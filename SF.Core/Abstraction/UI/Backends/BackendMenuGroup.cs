 
using System.Collections.Generic;

namespace SF.Core.Abstraction.UI.Backends
{
  public class BackendMenuGroup
  {
    public string Name { get; set; }
    public int Position { get; set; }
    public IEnumerable<BackendMenuItem> BackendMenuItems { get; set; }

    public BackendMenuGroup(string name, int position, IEnumerable<BackendMenuItem> backendMenuItems)
    {
      this.Name = name;
      this.Position = position;
      this.BackendMenuItems = backendMenuItems;
    }
  }
}