using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace SimpleFramework.Core.BreadCrumb
{
    /// <summary>
    /// BreadCrumb TagHelper
    /// </summary>
    [HtmlTargetElement("breadcrumb")]
    public class BreadCrumbTagHelper : TagHelper
    {
        /// <summary>
        /// Such as 'Home' or 'خانه'
        /// </summary>
        [HtmlAttributeName("asp-homepage-title")]
        public string HomePageTitle { set; get; }

        /// <summary>
        /// Such as @Url.Action("Index", "Home")
        /// </summary>
        [HtmlAttributeName("asp-homepage-url")]
        public string HomePageUrl { set; get; }

        /// <summary>
        /// such as `glyphicon glyphicon-home`
        /// </summary>
        [HtmlAttributeName("asp-homepage-glyphicon")]
        public string HomePageGlyphIcon { set; get; }

        /// <summary>
        ///
        /// </summary>
        protected HttpRequest Request => ViewContext.HttpContext.Request;

        /// <summary>
        ///
        /// </summary>
        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var breadCrumbs = ViewContext.HttpContext.Items[BreadCrumbExtentions.CurrentBreadCrumbKey] as List<BreadCrumb>;
            if (breadCrumbs == null || !breadCrumbs.Any())
            {
                return;
            }

            var currentFullUrl = Request.GetEncodedUrl();
            var currentRouteUrl = new UrlHelper(ViewContext).Action(ViewContext.ActionDescriptor.RouteValues["action"]);
            var isCurrentPageHomeUrl = HomePageUrl.Equals(currentFullUrl, StringComparison.OrdinalIgnoreCase) ||
                                       HomePageUrl.Equals(currentRouteUrl, StringComparison.OrdinalIgnoreCase);


            output.TagName = "ol";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "breadcrumb");

            if (isCurrentPageHomeUrl)
            {
                var itemBuilder = new TagBuilder("li");
                itemBuilder.AddCssClass("active");
                itemBuilder.InnerHtml.AppendHtml(
                    $"<span class='{HomePageGlyphIcon}' aria-hidden='true'></span> {HomePageTitle}");
                output.Content.AppendHtml(itemBuilder);
            }
            else
            {
                var itemBuilder = new TagBuilder("li");
                itemBuilder.InnerHtml.AppendHtml(
                    $"<a href='{HomePageUrl}'><span class='{HomePageGlyphIcon}' aria-hidden='true'></span> {HomePageTitle}</a>");
                output.Content.AppendHtml(itemBuilder);
            }

            foreach (var node in breadCrumbs.OrderBy(x => x.Order))
            {
                if (node.Url.Equals(HomePageUrl, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (node.Url.Equals(currentFullUrl, StringComparison.OrdinalIgnoreCase) ||
                    node.Url.Equals(currentRouteUrl, StringComparison.OrdinalIgnoreCase))
                {
                    var itemBuilder = new TagBuilder("li");
                    itemBuilder.AddCssClass("active");

                    if (!string.IsNullOrWhiteSpace(node.GlyphIcon))
                    {
                        itemBuilder.InnerHtml.AppendHtml(
                            $"<span class='{node.GlyphIcon}' aria-hidden='true'></span> ");
                    }
                    itemBuilder.InnerHtml.AppendHtml($"{node.Title}");
                    output.Content.AppendHtml(itemBuilder);
                }
                else
                {
                    var itemBuilder = new TagBuilder("li");
                    itemBuilder.InnerHtml.AppendHtml($"<a href='{node.Url}'>");
                    if (!string.IsNullOrWhiteSpace(node.GlyphIcon))
                    {
                        itemBuilder.InnerHtml.AppendHtml(
                            $"<span class='{node.GlyphIcon}' aria-hidden='true'></span> ");
                    }
                    itemBuilder.InnerHtml.AppendHtml($"{node.Title}");
                    itemBuilder.InnerHtml.AppendHtml("</a>");
                    output.Content.AppendHtml(itemBuilder);
                }
            }
        }
    }
}