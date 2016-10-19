using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleFramework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SimpleFramework.Core.Interceptors
{
	public class InterceptionContext
	{
		public  DbContext DatabaseContext { get; internal set; }
		public ChangeTracker ChangeTracker { get; internal set; }
		public IEnumerable<EntityEntry> Entries { get; internal set; }
		public ILookup<EntityState, EntityEntry> EntriesByState { get; internal set; }

		private readonly List<IInterceptor> _interceptors = new List<IInterceptor>();

		public InterceptionContext(IInterceptor[] interceptors)
		{
            //if (interceptors != null)
            {
                _interceptors = new List<IInterceptor>(interceptors);
            }
		}

		public void Before()
		{
			var interceptors = _interceptors;
			interceptors.ForEach(intercept => intercept.Before(this));
		}

		public void After()
		{
			var interceptors = _interceptors;
			interceptors.ForEach(intercept => intercept.After(this));
		}

	}
}
