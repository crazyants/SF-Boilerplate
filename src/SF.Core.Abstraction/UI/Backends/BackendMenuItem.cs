
namespace SF.Core.Abstraction.UI.Backends
{
  public class BackendMenuItem
  {
    public string Url { get; set; }
    public string Name { get; }
    public int Position { get; set; }

    public BackendMenuItem(string url, string name, int position)
    {
      this.Url = url;
      this.Name = name;
      this.Position = position;
    }
  }
}