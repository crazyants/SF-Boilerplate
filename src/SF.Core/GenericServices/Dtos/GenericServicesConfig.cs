/*******************************************************************************
* 命名空间: SF.Core.GenericServices
*
* 功 能： N/A
* 类 名： GenericServicesConfig
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/7 17:42:25 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace SF.Core.GenericServices.Dtos
{
    /// <summary>
    /// This is the signiture of the method called on a SqlException happening in SaveChangesWithChecking (sync and async)
    /// </summary>
    /// <param name="exception">This is the Sql Exception that occured</param>
    /// <param name="entitiesThatErrored">DbEntityEntry objects that represents the entities that could not be saved to the database</param>
    /// <returns>return ValidationResult with error, or null if cannot handle this error</returns>
    public delegate ValidationResult HandleSqlException(
        Exception exception, IEnumerable<EntityEntry> entitiesThatErrored);

    /// <summary>
    /// This is the signiture of the method called if an exception is found in the RealiseSingleWithErrorChecking
    /// </summary>
    /// <param name="ex">the exception thrown</param>
    /// <param name="callingMethodName">the name of the calling method (can be used for logging)</param>
    /// <returns>error message, or null if no error</returns>
    public delegate string RealiseSingleException(Exception ex, string callingMethodName);

    /// <summary>
    /// This static class holds the GenericService configuration parts
    /// </summary>
    public static class GenericServicesConfig
    {

        private static readonly Dictionary<int, HandleSqlException> PrivateSqlHandlerDict = new Dictionary<int, HandleSqlException>();

        private static readonly Dictionary<int, string> PrivateSqlErrorDict = new Dictionary<int, string>
        {
            {547, "This operation failed because another data entry uses this entry."},         //constraint
            {2601, "One of the properties is marked as Unique index and there is already an entry with that value."} //cannot insert dup key in index
        };

        private static bool _useDelegateDecompilerWhereNeeded = true;

        //This holds all the AutoMapper configs
        internal static readonly ConcurrentDictionary<string, MapperConfiguration> AutoMapperConfigs =
            new ConcurrentDictionary<string, MapperConfiguration>();

        /// <summary>
        /// This contains the SqlErrorNumbers that will be caught by SaveChangesWithChecking (sync and Async)
        /// </summary>
        public static IReadOnlyDictionary<int, string> SqlErrorDict { get { return PrivateSqlErrorDict; } }

        /// <summary>
        /// This contains the HandleSqlException methods by Sql Error number that will be caught by SaveChangesWithChecking (sync and Async)
        /// </summary>
        public static IReadOnlyDictionary<int, HandleSqlException> SqlHandlerDict { get { return PrivateSqlHandlerDict; } }

        /// <summary>
        /// This can be set to a method that is called in RealiseSingleWithErrorChecking when an exception occurs.
        /// RealiseSingleWithErrorChecking is used when a single DTO/Enity is asked for inside DetailService and
        /// UpdateSetupService, plus RealiseSingleWithErrorChecking can be used as an extension. 
        /// The method you provide should return a error string if it can decode the error for the user, otherwise should return null
        /// </summary>
        public static RealiseSingleException RealiseSingleExceptionMethod { internal get; set; }

        /// <summary>
        /// Set this if you want Generic Services to use the DelegateDecompiler. See documentation for more information
        /// </summary>
        public static bool UseDelegateDecompilerWhereNeeded
        {
            get { return _useDelegateDecompilerWhereNeeded; }
            set { _useDelegateDecompilerWhereNeeded = value; }
        }

        //--------------------------------------------------
        //public methods

        /// <summary>
        /// This clears any AutoMapper mappings. Used when Unit Testing to ensure the mappings are newly set up.
        /// </summary>
        public static void ClearAutoMapperCache()
        {
            AutoMapperConfigs.Clear();
        }

        /// <summary>
        /// This clears the SqlErrorDict of all entries
        /// </summary>
        public static void ClearSqlErrorDict()
        {
            PrivateSqlErrorDict.Clear();
        }

        /// <summary>
        /// This clears the SqlHandlerDict of all entries
        /// </summary>
        public static void ClearSqlHandlerDict()
        {
            PrivateSqlHandlerDict.Clear();
        }

        /// <summary>
        /// This adds an entry to the SqlErrorDict
        /// </summary>
        /// <param name="sqlErrorNumber"></param>
        /// <param name="errorText"></param>
        public static void AddToSqlErrorDict(int sqlErrorNumber, string errorText)
        {
            if (PrivateSqlErrorDict.ContainsKey(sqlErrorNumber))
                PrivateSqlErrorDict[sqlErrorNumber] = errorText;
            else
                PrivateSqlErrorDict.Add(sqlErrorNumber, errorText);
        }

        /// <summary>
        /// This adds an ErrorHandler to the SqlHandlerDict
        /// The ErrorHandler will be called if the specified sql error happens.
        /// Note: will throw an exception if an error handler already exists for that sql error number unless 
        /// the checkNotAlreadySet is set to false
        /// </summary>
        /// <param name="sqlErrorNumber"></param>
        /// <param name="errorHandler">Called when given sql error number happens with sql error and entities. 
        /// Should return ValidationError or null if cannot handle the error</param>
        /// <param name="checkNotAlreadySet"></param>
        public static void AddToSqlHandlerDict(int sqlErrorNumber, HandleSqlException errorHandler, bool checkNotAlreadySet = true)
        {
            if (PrivateSqlHandlerDict.ContainsKey(sqlErrorNumber))
            {
                if (checkNotAlreadySet)
                    throw new InvalidOperationException(
                        string.Format("You tried to add an exception handler for sql error {0} but a handler called {1} was already there.",
                        sqlErrorNumber, PrivateSqlHandlerDict[sqlErrorNumber].GetType().GetTypeInfo().Name));
                PrivateSqlHandlerDict[sqlErrorNumber] = errorHandler;
            }
            else
                PrivateSqlHandlerDict.Add(sqlErrorNumber, errorHandler);
        }

    }
}
