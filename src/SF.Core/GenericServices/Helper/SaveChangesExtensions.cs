/*******************************************************************************
* 命名空间: SF.Core.GenericServices.Helper
*
* 功 能： N/A
* 类 名： SaveChangesExtensions
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/9 17:10:40 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using SF.Core.GenericServices.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.GenericServices.Helper
{
    /// <summary>
    /// This contains extention errors that change SaveChanges/SvaeChangesAsync into returning error messages
    /// rather than an exception on certain types of errors.
    /// </summary>
    public static class SaveChangesExtensions
    {
        /// <summary>
        /// This extension will undertake a SaveChanges but will catch any validation errors 
        /// or specific SqlException specified in ServicesConfiguration.SqlErrorDict and return
        /// them as errors rather than throw an exception
        /// </summary>
        /// <param name="db"></param>
        /// <returns>a status saying whether SaveChanges was successful or not. If not then holds errors</returns>
        public static ISuccessOrErrors SaveChangesWithChecking(this DbContext db)
        {
            var result = new SuccessOrErrors();
            var numChanges = 0;
            try
            {
                numChanges = db.SaveChanges(); //then update it
            }
            catch (ValidationException ex)
            {
                var validationResults = new List<ValidationResult>();
                validationResults.Add(ex.ValidationResult);
                return result.SetErrors(validationResults);
            }
            catch (DbUpdateException ex)
            {
                var decodedErrors = TryDecodeDbUpdateException(ex);
                if (decodedErrors == null)
                    throw; //it isn't something we understand

                return result.SetErrors(decodedErrors);
            }

            return result.SetSuccessMessage("Successfully added or updated {0} items", numChanges);
        }

        /// <summary>
        /// This extension will undertake a SaveChangesAsync but will catch any validation errors 
        /// or specific SqlException specified in ServicesConfiguration.SqlErrorDict and return
        /// them as errors
        /// </summary>
        /// <param name="db"></param>
        /// <returns>Task containing status saying whether SaveChanges was successful or not. If not then holds errors</returns>
        public static async Task<ISuccessOrErrors> SaveChangesWithCheckingAsync(this DbContext db)
        {
            var result = new SuccessOrErrors();
            var numChanges = 0;
            try
            {
                numChanges = await db.SaveChangesAsync().ConfigureAwait(false); //then update it
            }
            catch (ValidationException ex)
            {
                var validationResults = new List<ValidationResult>();
                validationResults.Add(ex.ValidationResult);
                return result.SetErrors(validationResults);
            }
            catch (DbUpdateException ex)
            {
                var decodedErrors = TryDecodeDbUpdateException(ex);
                if (decodedErrors == null)
                    throw; //it isn't something we understand

                return result.SetErrors(decodedErrors);
            }


            return result.SetSuccessMessage("Successfully added or updated {0} items", numChanges);
        }

        /// <summary>
        /// This decodes the DbUpdateException. If there are any errors it can
        /// handle then it returns a list of errors. Otherwise it returns null
        /// which means rethrow the error as it has not been handled
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>null if cannot handle errors, otherwise a list of errors</returns>
        private static IEnumerable<ValidationResult> TryDecodeDbUpdateException(DbUpdateException ex)
        {

            var result = new List<ValidationResult>();

            result.Add(new ValidationResult(ex.Message));

            return result.Any() ? result : null;
        }

    }
}
