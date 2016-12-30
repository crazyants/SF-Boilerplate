
namespace SF.Core.Abstraction.UI.Backends
{
  public class BackendScript
  {
    public string Url { get; set; }
    public int Position { get; set; }

    public BackendScript(string url, int position)
    {
      this.Url = url;
      this.Position = position;
    }
  }
}