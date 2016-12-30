
using SF.Core.QueryExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SF.Core.Extensions
{
    /// <summary>
    /// Linq, List, Dictionary, etc extensions
    /// </summary>
    public static partial class ExtensionMethods
    {

        #region Tree
        /// <summary>
        /// 树形查询条件
        /// </summary>
        /// <param name="entityList">数据源</param>
        /// <param name="condition">查询条件</param>
        /// <param name="primaryKey">主键名称</param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static List<T> TreeWhere<T>(this List<T> entityList, Predicate<T> condition, string primaryKey = "Id", string parentId = "ParentId") where T : class
        {

            List<T> locateList = entityList.FindAll(condition);
            var parameter = Expression.Parameter(typeof(T), "t");
            //模糊查询表达式
            List<T> treeList = new List<T>();
            foreach (T entity in locateList)
            {
                //先把自己加入进来
                treeList.Add(entity);
                //向上查询
                string pId = entity.GetType().GetProperty(parentId).GetValue(entity, null).ToString();
                while (true)
                {
                    if (string.IsNullOrEmpty(pId) && pId == "0")
                    {
                        break;
                    }

                    // Predicate<T> upLambda = (Expression.Equal(parameter.Property(primaryKey), Expression.Constant(pId))).ToLambda<Predicate<T>>(parameter).Compile();
                    T upRecord = entityList.AsQueryable().Where(primaryKey, DynamicCompare.Equal, pId).FirstOrDefault();
                    if (upRecord != null)
                    {
                        treeList.Add(upRecord);
                        pId = upRecord.GetType().GetProperty(parentId).GetValue(upRecord, null).ToString();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return treeList.Distinct().ToList();
        }

        /// <summary>
        /// 树形查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">实体数据</param>
        /// <param name="entityList">数据源</param>
        /// <param name="condition">查询条件</param>
        /// <param name="primaryKey">主键名称</param>
        /// <param name="parentId">父级ID名称</param>
        /// <returns></returns>
        public static List<T> FindParentWhere<T>(this T entity, List<T> entityList, Predicate<T> condition, string primaryKey = "Id", string parentId = "ParentId") where T : class
        {
            if (condition != null)
                entityList = entityList.FindAll(condition);
            var parameter = Expression.Parameter(typeof(T), "t");
            //模糊查询表达式
            List<T> treeList = new List<T>();
            //先把自己加入进来
            // treeList.Add(entity);
            //获取本身父级ID
            string pId = entity.GetType().GetProperty(parentId).GetValue(entity, null).ToString();
            while (true)
            {
                //ID为空或为0退出
                if (string.IsNullOrEmpty(pId) && pId == "0")
                {
                    break;
                }
                //获取父级信息
                T upRecord = entityList.AsQueryable().Where(primaryKey, DynamicCompare.Equal, pId).FirstOrDefault();
                if (upRecord != null)
                {
                    treeList.Add(upRecord);
                    //获取父级的父级ID
                    pId = upRecord.GetType().GetProperty(parentId).GetValue(upRecord, null).ToString();
                }
                else
                {
                    break;
                }
            }
            //去重并发转
            return treeList.Distinct().Reverse().ToList();
        }
        #endregion

    }
}
