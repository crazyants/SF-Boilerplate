using Microsoft.AspNetCore.Http;

namespace SF.Web.Control.Editor.Handlers
{
    public class ConfigHandler : Handler
{
    public ConfigHandler(HttpContext context) : base(context) { }

    public override void Process()
    {
        WriteJson(Config.Items);
    }
}
}