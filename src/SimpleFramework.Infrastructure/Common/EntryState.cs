using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleFramework.Infrastructure.Common
{
	public enum EntryState
	{
		Detached = 1,
		Unchanged = 2,
		Added = 4,
		Deleted = 8,
		Modified = 16,
	}
}
