using Microsoft.EntityFrameworkCore.ChangeTracking;
using SF.Entitys.Abstraction;
using System;

namespace SF.Core.Interceptors
{
    public class EntityPrimaryKeyGeneratorInterceptor : ChangeInterceptor<EntityWithTypedId<string>>
    {
        public override void OnBeforeInsert(EntityEntry entry, EntityWithTypedId<string> entity)
        {
            base.OnBeforeInsert(entry, entity);

            entity.Id = Guid.NewGuid().ToString("N");

        }

    }
}
