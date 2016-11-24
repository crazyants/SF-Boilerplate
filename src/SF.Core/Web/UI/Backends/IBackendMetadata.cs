 
using System.Collections.Generic;

namespace SF.Core.Web.UI.Backends
{
  public interface IBackendMetadata
  {
    IEnumerable<BackendStyleSheet> BackendStyleSheets { get; }
    IEnumerable<BackendScript> BackendScripts { get; }
    IEnumerable<BackendMenuGroup> BackendMenuGroups { get; }
  }
}