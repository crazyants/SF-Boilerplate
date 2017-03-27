 
using System.Collections.Generic;

namespace SF.Core.Abstraction.UI.Backends
{
  public interface IBackendMetadata
  {
    IEnumerable<BackendStyleSheet> BackendStyleSheets { get; }
    IEnumerable<BackendScript> BackendScripts { get; }
    IEnumerable<BackendMenuGroup> BackendMenuGroups { get; }
  }
}