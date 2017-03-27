using System;
using System.ComponentModel.DataAnnotations;

namespace SF.Entitys.Abstraction
{
    /// <summary>
    /// 实体增删改扩展
    /// </summary>
    public static class EntityExtension
    {
        public static void DefaultCreate(this IHaveCreatedMeta haveCreatedMeta, string currentUser)
        {
            var currentTime = DateTimeOffset.Now;
            haveCreatedMeta.CreatedOn = haveCreatedMeta.CreatedOn == default(DateTimeOffset) ? currentTime : haveCreatedMeta.CreatedOn;
            haveCreatedMeta.CreatedBy = haveCreatedMeta.CreatedBy ?? currentUser;
        }
        public static void DefaultUpdate(this IHaveUpdatedMeta haveUpdatedMeta, string currentUser)
        {
            var currentTime = DateTimeOffset.Now;
            haveUpdatedMeta.UpdatedOn = haveUpdatedMeta.UpdatedOn == default(DateTimeOffset) ? currentTime : haveUpdatedMeta.UpdatedOn;
            haveUpdatedMeta.UpdatedBy = haveUpdatedMeta.UpdatedBy ?? currentUser;
        }
        public static void DefaultDelete(this IHaveDeletedMeta haveDeletedMeta, string currentUser)
        {
            var currentTime = DateTimeOffset.Now;
            haveDeletedMeta.DeletedOn = haveDeletedMeta.DeletedOn == default(DateTimeOffset) ? currentTime : haveDeletedMeta.DeletedOn;
            haveDeletedMeta.DeletedBy = haveDeletedMeta.DeletedBy ?? currentUser;
        }
    }


}
