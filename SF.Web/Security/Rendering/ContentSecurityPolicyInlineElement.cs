using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SF.Web.Security.Filters;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Text.Encodings.Web;

namespace SF.Web.Security.Rendering
{
    internal class ContentSecurityPolicyInlineElement : IDisposable
    {
        #region Constants
        internal const string ScriptTagName = "script";
        internal const string StyleTagName = "style";

        private const string _nonceAttribute = "nonce";
        private const string _sha256SourceFormat = " 'sha256-{0}'";
        #endregion

        #region Fields
        private static IDictionary<string, string> _inlineExecutionContextKeys = new Dictionary<string, string>
        {
            { ScriptTagName, ContentSecurityPolicyAttribute.InlineExecutionContextKeys[ContentSecurityPolicyAttribute.ScriptDirective] },
            { StyleTagName, ContentSecurityPolicyAttribute.InlineExecutionContextKeys[ContentSecurityPolicyAttribute.StyleDirective] }
        };

        private static IDictionary<string, string> _hashListBuilderContextKeys = new Dictionary<string, string>
        {
            { ScriptTagName, ContentSecurityPolicyAttribute.HashListBuilderContextKeys[ContentSecurityPolicyAttribute.ScriptDirective] },
            { StyleTagName, ContentSecurityPolicyAttribute.HashListBuilderContextKeys[ContentSecurityPolicyAttribute.StyleDirective] }
        };

        private readonly ViewContext _viewContext;
        private readonly TextWriter _viewContextWriter;
        private readonly ContentSecurityPolicyInlineExecution _inlineExecution;
        private readonly TagBuilder _elementTag;
        #endregion

        #region Constructor
        internal ContentSecurityPolicyInlineElement(ViewContext context, string elementTagName, IDictionary<string, object> htmlAttributes)
        {
            _viewContext = context;

            _inlineExecution = (ContentSecurityPolicyInlineExecution)_viewContext.HttpContext.Items[_inlineExecutionContextKeys[elementTagName]];

            _elementTag = new TagBuilder(elementTagName);
            _elementTag.MergeAttributes(htmlAttributes);
            if (_inlineExecution == ContentSecurityPolicyInlineExecution.Nonce)
            {
                _elementTag.MergeAttribute(_nonceAttribute, (string)_viewContext.HttpContext.Items[ContentSecurityPolicyAttribute.NonceRandomContextKey]);
            }

            _elementTag.TagRenderMode = TagRenderMode.StartTag;
            _elementTag.WriteTo(_viewContext.Writer, HtmlEncoder.Default);

            if (_inlineExecution == ContentSecurityPolicyInlineExecution.Hash)
            {
                _viewContextWriter = _viewContext.Writer;
                _viewContext.Writer = new StringWriter();
            }
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            if (_inlineExecution == ContentSecurityPolicyInlineExecution.Hash)
            {
                StringBuilder elementInnerHtmlBuilder = ((StringWriter)_viewContext.Writer).GetStringBuilder();
                string elementInnerHtml = elementInnerHtmlBuilder.ToString().Replace("\r\n", "\n");
                byte[] elementHashBytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(elementInnerHtml));
                string elementHash = Convert.ToBase64String(elementHashBytes);
                ((StringBuilder)_viewContext.HttpContext.Items[_hashListBuilderContextKeys[_elementTag.TagName]]).AppendFormat(_sha256SourceFormat, elementHash);

                _viewContext.Writer.Dispose();
                _viewContext.Writer = _viewContextWriter;
                _viewContext.Writer.Write(elementInnerHtml);
            }

            _elementTag.TagRenderMode = TagRenderMode.EndTag;
            _elementTag.WriteTo(_viewContext.Writer, HtmlEncoder.Default);
        }
        #endregion
    }
}
