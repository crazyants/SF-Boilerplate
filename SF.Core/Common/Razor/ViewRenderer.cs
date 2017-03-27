using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.IO;
using System.Threading.Tasks;

namespace SF.Core.Common.Razor
{
    public interface IViewRenderService
    {
        Task<string> RenderViewAsString<TModel>(string viewName, TModel model);
    }
    /// <summary>
    /// 此类是提供一种容易的方法来生成html字符串
    ///  使用 Razor templates and models,用于生成电子邮件的html字符串。
    /// </summary>
    /// <example>
    ///    await new ViewRenderer.RenderViewAsString<string>("EmailTemplates/AccountApprovedTextEmail", loginUrl);
    /// </example>
    public class ViewRenderService : IViewRenderService
    {
        public ViewRenderService(
            ICompositeViewEngine viewEngine,
            ITempDataProvider tempDataProvider,
            IActionContextAccessor actionAccessor
            )
        {
            this.viewEngine = viewEngine;
            this.tempDataProvider = tempDataProvider;
            this.actionAccessor = actionAccessor;

        }

        private ICompositeViewEngine viewEngine;
        private ITempDataProvider tempDataProvider;
        private IActionContextAccessor actionAccessor;

        public async Task<string> RenderViewAsString<TModel>(string viewName, TModel model)
        {

            var viewData = new ViewDataDictionary<TModel>(
                        metadataProvider: new EmptyModelMetadataProvider(),
                        modelState: new ModelStateDictionary())
            {
                Model = model
            };

            var actionContext = actionAccessor.ActionContext;

            var tempData = new TempDataDictionary(actionContext.HttpContext, tempDataProvider);

            using (StringWriter output = new StringWriter())
            {

                ViewEngineResult viewResult = viewEngine.FindView(actionContext, viewName, true);

                ViewContext viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewData,
                    tempData,
                    output,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return output.GetStringBuilder().ToString();
            }
        }
    }
}
