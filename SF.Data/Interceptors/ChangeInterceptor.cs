using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace SF.Core.Interceptors
{
    public class ChangeInterceptor<T> : TypeInterceptor
    {
        protected override void OnBefore(EntityEntry item)
        {
            T tItem = (T)item.Entity;
            switch (item.State)
            {
                case EntityState.Added:
                    this.OnBeforeInsert(item, tItem);
                    break;
                case EntityState.Deleted:
                    this.OnBeforeDelete(item, tItem);
                    break;
                case EntityState.Modified:
                    this.OnBeforeUpdate(item, tItem);
                    break;
            }
        }

        protected override void OnAfter(EntityEntry item, EntityState state)
        {
            T tItem = (T)item.Entity;
            switch (state)
            {
                case EntityState.Added:
                    this.OnAfterInsert(item, tItem);
                    break;
                case EntityState.Deleted:
                    this.OnAfterDelete(item, tItem);
                    break;
                case EntityState.Modified:
                    this.OnAfterUpdate(item, tItem);
                    break;
            }
        }

        public virtual void OnBeforeInsert(EntityEntry entry, T item)
        {
            return;
        }

        public virtual void OnAfterInsert(EntityEntry entry, T item)
        {
            return;
        }

        public virtual void OnBeforeUpdate(EntityEntry entry, T item)
        {
            return;
        }

        public virtual void OnAfterUpdate(EntityEntry entry, T item)
        {
            return;
        }

        public virtual void OnBeforeDelete(EntityEntry entry, T item)
        {
            return;
        }

        public virtual void OnAfterDelete(EntityEntry entry, T item)
        {
            return;
        }

        public ChangeInterceptor()
            : base(typeof(T))
        {

        }
    }

}
