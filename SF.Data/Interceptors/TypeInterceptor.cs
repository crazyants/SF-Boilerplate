using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using SF.Core.Abstraction.Interceptors;
using System.Reflection;

namespace SF.Core.Interceptors
{
    public abstract class TypeInterceptor : IInterceptor
    {
        private readonly System.Type _targetType;
        public Type TargetType { get { return _targetType; } }

        protected TypeInterceptor(System.Type targetType)
        {
            this._targetType = targetType;
        }

        public virtual bool IsTargetEntity(EntityEntry item)
        {
            return item.State != EntityState.Detached &&
                   this.TargetType.IsInstanceOfType(item.Entity);
        }

        public void Before(InterceptionContext context)
        {
            foreach (var entry in context.Entries)
                Before(entry);
        }

        public void After(InterceptionContext context)
        {
            foreach (var entryWithState in context.EntriesByState)
            {
                foreach (var entry in entryWithState)
                {
                    After(entry, entryWithState.Key);
                }
            }
        }

        private void Before(EntityEntry item)
        {
            if (this.IsTargetEntity(item))
                this.OnBefore(item);
        }

        protected abstract void OnBefore(EntityEntry item);

        private void After(EntityEntry item, EntityState state)
        {
            if (this.IsTargetEntity(item))
                this.OnAfter(item, state);
        }

        protected abstract void OnAfter(EntityEntry item, EntityState state);
    }

}
