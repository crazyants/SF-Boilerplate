 
using System.Collections.Generic;

namespace SimpleFramework.Infrastructure.UI
{
  public abstract class BackendMetadataBase : IBackendMetadata
  {
    public virtual IEnumerable<BackendStyleSheet> BackendStyleSheets
    {
      get
      {
        return null;
      }
    }

    public virtual IEnumerable<BackendScript> BackendScripts
    {
      get
      {
        return null;
      }
    }

    public virtual IEnumerable<BackendMenuGroup> BackendMenuGroups
    {
      get
      {
        return null;
      }
    }
  }
}