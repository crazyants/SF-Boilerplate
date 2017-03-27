 
using System.Collections.Generic;

namespace SF.Core.Abstraction.UI.Backends
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