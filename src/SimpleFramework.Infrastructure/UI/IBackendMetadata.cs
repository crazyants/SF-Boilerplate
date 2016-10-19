 
using System.Collections.Generic;

namespace SimpleFramework.Infrastructure.UI
{
  public interface IBackendMetadata
  {
    IEnumerable<BackendStyleSheet> BackendStyleSheets { get; }
    IEnumerable<BackendScript> BackendScripts { get; }
    IEnumerable<BackendMenuGroup> BackendMenuGroups { get; }
  }
}