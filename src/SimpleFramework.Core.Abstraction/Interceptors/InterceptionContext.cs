using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SimpleFramework.Core.Abstraction.Interceptors
{
	public class InterceptionContext
	{
		public  DbContext DatabaseContext { get;  set; }
		public ChangeTracker ChangeTracker { get;  set; }
		public IEnumerable<EntityEntry> Entries { get;  set; }
		public ILookup<EntityState, EntityEntry> EntriesByState { get;  set; }

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
