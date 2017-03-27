using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections;
using SF.Entitys.Abstraction;

namespace SF.Core.Common
{
	public static class ReflectionUtility
	{
		public static IEnumerable<string> GetPropertyNames<T>(params Expression<Func<T, object>>[] propertyExpressions)
		{
			var retVal = new List<string>();
			foreach(var propertyExpression in propertyExpressions)
			{
				retVal.Add(GetPropertyName(propertyExpression));
			}
			return retVal;
		}

		public static string GetPropertyName<T>(Expression<Func<T, object>> propertyExpression)
		{
			string retVal = null;
			if (propertyExpression != null)
			{
				var lambda = (LambdaExpression)propertyExpression;
				MemberExpression memberExpression;
				if (lambda.Body is UnaryExpression)
				{
					var unaryExpression = (UnaryExpression)lambda.Body;
					memberExpression = (MemberExpression)unaryExpression.Operand;
				}
				else
				{
					memberExpression = (MemberExpression)lambda.Body;
				}
				retVal = memberExpression.Member.Name;

			}
			return retVal;
		}

		public static PropertyInfo[] FindPropertiesWithAttribute(this Type type, Type attribute)
		{
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			return properties.Where(x => x.GetCustomAttributes(attribute, true).Any()).ToArray();
		}

		public static bool IsHaveAttribute(this PropertyInfo propertyInfo, Type attribute)
		{
			return propertyInfo.GetCustomAttributes(attribute, true).Any();
		}

        public static bool GetAttribute(this PropertyInfo propertyInfo, Type attribute)
        {
            return propertyInfo.GetCustomAttributes(attribute, true).Any();
        }

        public static List<object> GetAttributesOfDeclaringType(this Type type)
        {
            var attributeList = new List<object>();

            attributeList.AddRange(type.GetTypeInfo().GetCustomAttributes());

            return attributeList;
        }

        public static Type[] GetTypeInheritanceChain(this Type type)
		{
			var retVal = new List<Type>();

			retVal.Add(type);
			var baseType = type.GetTypeInfo().BaseType;
			while (baseType != typeof(BaseEntity) && baseType != typeof(object) )
			{
				retVal.Add(baseType);
				baseType = baseType.GetTypeInfo().BaseType;
			}
			return retVal.ToArray();
		}

        public static Type[] GetTypeInheritanceChainTo(this Type type, Type toBaseType)
        {
            var retVal = new List<Type>();

            retVal.Add(type);
            var baseType = type.GetTypeInfo().BaseType;
            while (baseType != toBaseType && baseType != typeof(object))
            {
                retVal.Add(baseType);
                baseType = baseType.GetTypeInfo().BaseType;
            }
            return retVal.ToArray();
        }

        public static bool IsDerivativeOf(this Type type, Type typeToCompare)
		{
			if (type == null)
			{
				throw new NullReferenceException();
			}

			var retVal = type.GetTypeInfo().BaseType != null;
			if (retVal)
			{
				retVal = type.GetTypeInfo().BaseType == typeToCompare;
			}
			if (!retVal && type.GetTypeInfo().BaseType != null)
			{
				retVal = type.GetTypeInfo().BaseType.IsDerivativeOf(typeToCompare);
			}
			return retVal;
		}

        public static T[] GetFlatObjectsListWithInterface<T>(this object obj, List<T> resultList = null)
        {
            var retVal = new List<T>();

            if (resultList == null)
            {
                resultList = new List<T>();
            }
            //Ignore cycling references
            if (!resultList.Any(x => Object.ReferenceEquals(x, obj)))
            {
                var objectType = obj.GetType();

                if (objectType.GetTypeInfo().GetInterface(typeof(T).Name) != null)
                {
                    retVal.Add((T)obj);
                    resultList.Add((T)obj);
                }

                var properties = objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                var objects = properties.Where(x => x.PropertyType.GetTypeInfo().GetInterface(typeof(T).Name) != null)
                                        .Select(x => (T)x.GetValue(obj)).ToList();

                //Recursive call for single properties
                retVal.AddRange(objects.Where(x => x != null).SelectMany(x => x.GetFlatObjectsListWithInterface<T>(resultList)));

                //Handle collection and arrays
                var collections = properties.Select(x => x.GetValue(obj, null))
                                            .Where(x => x is IEnumerable && !(x is String))
                                            .Cast<IEnumerable>();

                foreach (var collection in collections)
                {
                    foreach (var collectionObject in collection)
                    {
                        if (collectionObject is T)
                        {
                            retVal.AddRange(collectionObject.GetFlatObjectsListWithInterface<T>(resultList));
                        }
                    }
                }
            }
            return retVal.ToArray();
        }


        /// <summary>
        /// Gets a list of attributes defined for a class member and it's declaring type including inherited attributes.
        /// </summary>
        /// <param name="inherit">Inherit attribute from base classes</param>
        /// <param name="memberInfo">MemberInfo</param>
        public static List<object> GetAttributesOfMemberAndDeclaringType(MemberInfo memberInfo, bool inherit = true)
        {
            var attributeList = new List<object>();

            attributeList.AddRange(memberInfo.GetCustomAttributes(inherit));

            //Add attributes on the class
            if (memberInfo.DeclaringType != null)
            {
                attributeList.AddRange(memberInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(inherit));
            }

            return attributeList;
        }
    }
}
