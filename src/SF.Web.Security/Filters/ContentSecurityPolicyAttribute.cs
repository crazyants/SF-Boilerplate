using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SF.Web.Security.Filters
{
    /// <summary>
    /// Content Security Policy Level 2 inline execution modes
    /// </summary>
    public enum ContentSecurityPolicyInlineExecution
    {
        /// <summary>
        /// Refuse any inline execution
        /// </summary>
        Refuse,
        /// <summary>
        /// Allow all inline execution
        /// </summary>
        Unsafe,
        /// <summary>
        /// Use nonce mechanism
        /// </summary>
        Nonce,
        /// <summary>
        /// Use hash mechanism
        /// </summary>
        Hash
    }

    /// <summary>
    /// Content Security Policy Level 2 sandbox flags
    /// </summary>
    [Flags]
    public enum ContentSecurityPolicySandboxFlags
    {
        /// <summary>
        /// Set no sandbox flags
        /// </summary>
        None = 0,
        /// <summary>
        /// Set allow-forms sandbox flag
        /// </summary>
        AllowForms = 1,
        /// <summary>
        /// Set allow-pointer-lock sandbox flag
        /// </summary>
        AllowPointerLock = 2,
        /// <summary>
        /// Set allow-popups sandbox flag
        /// </summary>
        AllowPopups = 4,
        /// <summary>
        /// Set allow-same-origin sandbox flag
        /// </summary>
        AllowSameOrigin = 8,
        /// <summary>
        /// Set allow-scripts sandbox flag
        /// </summary>
        AllowScripts = 16,
        /// <summary>
        /// Set allow-top-navigation sandbox flag
        /// </summary>
        AllowTopNavigation = 32
    }

    /// <summary>
    /// Content Security Policy Level 2 referrer behaviours
    /// </summary>
    public enum ContentSecurityPolicyReferrer
    {
        /// <summary>
        /// Prevents the UA sending a referrer header.
        /// </summary>
        NoReferrer,
        /// <summary>
        /// Prevents the UA sending a referrer header when navigating from https to http.
        /// </summary>
        NoReferrerWhenDowngrade,
        /// <summary>
        /// Allows the UA to only send the origin in the referrer header.
        /// </summary>
        Origin,
        /// <summary>
        /// Allows the UA to only send the origin in the referrer header when making cross-origin requests.
        /// </summary>
        OriginWhenCrossOrigin,
        /// <summary>
        /// Allows the UA to send the full URL in the referrer header with same-origin and cross-origin reqiests.
        /// </summary>
        UnsafeUrl
    }

    /// <summary>
    /// Content Security Policy Level 2 user agent XSS heuristics behaviours
    /// </summary>
    public enum ContentSecurityPolicyReflectedXss
    {
        /// <summary>
        /// Allows reflected XSS attacks.
        /// </summary>
        Allow,
        /// <summary>
        /// Block reflected XSS attacks.
        /// </summary>
        Block,
        /// <summary>
        /// Filter the reflected XSS attack.
        /// </summary>
        Filter
    }

    /// <summary>
    /// Action filter for defining Content Security Policy Level 2 policies
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ContentSecurityPolicyAttribute : ActionFilterAttribute
    {
        #region Constants
        internal const string ScriptDirective = "script-src";
        internal const string StyleDirective = "style-src";

        internal const string NonceRandomContextKey = "Lib.AspNetCore.Mvc.Security.Csp.NonceRandom";

        private const string _contentSecurityPolicyHeader = "Content-Security-Policy";
        private const string _contentSecurityPolicyReportOnlyHeader = "Content-Security-Policy-Report-Only";

        private const string _baseDirectiveFormat = "base-uri {0};";
        private const string _childDirectiveFormat = "child-src {0};";
        private const string _connectDirectiveFormat = "connect-src {0};";
        private const string _defaultDirectiveFormat = "default-src {0};";
        private const string _fontDirectiveFormat = "font-src {0};";
        private const string _formDirectiveFormat = "form-action {0};";
        private const string _frameAncestorsDirectiveFormat = "frame-ancestors {0};";
        private const string _imageDirectiveFormat = "img-src {0};";
        private const string _manifestDirectiveFormat = "manifest-src {0};";
        private const string _mediaDirectiveFormat = "media-src {0};";
        private const string _objectDirectiveFormat = "object-src {0};";
        private const string _referrerDirectiveFormat = "referrer {0};";
        private const string _reflectedXssDirectiveFormat = "reflected-xss {0};";
        private const string _reportDirectiveFormat = "report-uri {0};";
        private const string _sandboxDirective = "sandbox";

        private const string _directivesDelimiter = ";";

        private const string _unsafeInlineSource = " 'unsafe-inline'";
        private const string _nonceSourceFormat = " 'nonce-{0}'";

        private const string _allowFormsSandboxFlag = " allow-forms";
        private const string _allowPointerLockSandboxFlag = " allow-pointer-lock";
        private const string _allowPopupsSandboxFlag = " allow-popups";
        private const string _allowSameOriginSandboxFlag = " allow-same-origin";
        private const string _allowScriptsSandboxFlag = " allow-scripts";
        private const string _allowTopNavigationSandboxFlag = " allow-top-navigation";

        private const string _noReferrer = "no-referrer";
        private const string _noReferrerWhenDowngrade = "no-referrer-when-downgrade";
        private const string _origin = "origin";
        private const string _originWhenCrossOrigin = "origin-when-cross-origin";
        private const string _unsafeUrl = "unsafe-url";
        #endregion

        #region Fields
        internal static IDictionary<string, string> InlineExecutionContextKeys = new Dictionary<string, string>
        {
            { ScriptDirective, "Lib.AspNetCore.Mvc.Security.Csp.ScriptInlineExecution" },
            { StyleDirective, "Lib.AspNetCore.Mvc.Security.Csp.StyleInlineExecution" }
        };

        internal static IDictionary<string, string> HashListBuilderContextKeys = new Dictionary<string, string>
        {
            { ScriptDirective, "Lib.AspNetCore.Mvc.Security.Csp.ScriptHashListBuilder" },
            { StyleDirective, "Lib.AspNetCore.Mvc.Security.Csp.StyleHashListBuilder" }
        };

        private static IDictionary<string, string> _hashListPlaceholders = new Dictionary<string, string>
        {
            { ScriptDirective, "<ScriptHashListPlaceholder>" },
            { StyleDirective, "<StyleHashListPlaceholder>" }
        };
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the list of URLs that can be used to specify the document base URL.
        /// </summary>
        public string BaseUri { get; set; }

        /// <summary>
        /// Gets or sets the source list  for web workers and nested browsing contexts.
        /// </summary>
        public string ChildSources { get; set; }

        /// <summary>
        /// Gets or sets the source list for fetch, XMLHttpRequest, WebSocket, and EventSource connections.
        /// </summary>
        public string ConnectSources { get; set; }

        /// <summary>
        /// Gets or sets the default source list for directives which can fall back to the default sources.
        /// </summary>
        public string DefaultSources { get; set; }

        /// <summary>
        /// Gets or sets the source list for fonts loaded using @font-face.
        /// </summary>
        public string FontSources { get; set; }

        /// <summary>
        /// Gets or sets the valid endpoints for form submissions.
        /// </summary>
        public string FormAction { get; set; }

        /// <summary>
        /// Gets or sets the valid parents that may embed a page using the frame and iframe elements.
        /// </summary>
        public string FrameAncestorsSources { get; set; }

        /// <summary>
        /// Gets or sets the source list for of images and favicons.
        /// </summary>
        public string ImageSources { get; set; }

        /// <summary>
        /// Gets or sets the source list for manifest which can be applied to the resource.
        /// </summary>
        public string ManifestSources { get; set; }

        /// <summary>
        /// Gets or sets the source list for loading media using the audio and video elements.
        /// </summary>
        public string MediaSources { get; set; }

        /// <summary>
        /// Gets or sets the source list for the object, embed, and applet elements.
        /// </summary>
        public string ObjectSources { get; set; }

        /// <summary>
        /// Gets or sets the value for referrer directive.
        /// </summary>
        public ContentSecurityPolicyReferrer? Referrer { get; set; }

        /// <summary>
        /// Gets or sets the value for reflected-xss directive.
        /// </summary>
        public ContentSecurityPolicyReflectedXss? ReflectedXss { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if this is report only policy.
        /// </summary>
        public bool ReportOnly { get; set; }

        /// <summary>
        /// Gets or sets the URL to which the user agent should send reports about policy violations
        /// </summary>
        public string ReportUri { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if sandbox policy should be applied.
        /// </summary>
        public bool Sandbox { get; set; }

        /// <summary>
        /// Gets or sets the sandboxing flags (only used when Sandbox is true)
        /// </summary>
        public ContentSecurityPolicySandboxFlags SandboxFlags { get; set; }

        /// <summary>
        /// Gets or sets the source list for scripts.
        /// </summary>
        public string ScriptSources { get; set; }

        /// <summary>
        /// Gets or sets the inline execution mode for scripts
        /// </summary>
        public ContentSecurityPolicyInlineExecution ScriptInlineExecution { get; set; }

        /// <summary>
        /// Gets or sets the source list for stylesheets.
        /// </summary>
        public string StyleSources { get; set; }

        /// <summary>
        /// Gets or sets the inline execution mode for stylesheets.
        /// </summary>
        public ContentSecurityPolicyInlineExecution StyleInlineExecution { get; set; }

        private string ContentSecurityPolicyHeader
        {
            get { return ReportOnly ? _contentSecurityPolicyReportOnlyHeader : _contentSecurityPolicyHeader; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes new instance of ContentSecurityPolicyAttribute
        /// </summary>
        public ContentSecurityPolicyAttribute()
        {
            DefaultSources = "'none'";
            ScriptInlineExecution = ContentSecurityPolicyInlineExecution.Refuse;
            StyleInlineExecution = ContentSecurityPolicyInlineExecution.Refuse;
            Sandbox = false;
            SandboxFlags = ContentSecurityPolicySandboxFlags.None;
            ReportOnly = false;
        }
        #endregion

        #region IActionFilter Members
        /// <summary>
        /// Called after the action method executes.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            StringBuilder policyBuilder = new StringBuilder();

            AppendDirective(policyBuilder, _baseDirectiveFormat, BaseUri);
            AppendDirective(policyBuilder, _childDirectiveFormat, ChildSources);
            AppendDirective(policyBuilder, _connectDirectiveFormat, ConnectSources);
            AppendDirective(policyBuilder, _defaultDirectiveFormat, DefaultSources);
            AppendDirective(policyBuilder, _fontDirectiveFormat, FontSources);
            AppendDirective(policyBuilder, _formDirectiveFormat, FormAction);
            AppendDirective(policyBuilder, _frameAncestorsDirectiveFormat, FrameAncestorsSources);
            AppendDirective(policyBuilder, _imageDirectiveFormat, ImageSources);
            AppendDirective(policyBuilder, _manifestDirectiveFormat, ManifestSources);
            AppendDirective(policyBuilder, _mediaDirectiveFormat, MediaSources);
            AppendDirective(policyBuilder, _objectDirectiveFormat, ObjectSources);
            AppendReferrerDirective(policyBuilder);
            AppendDirective(policyBuilder, _reflectedXssDirectiveFormat, ReflectedXss.HasValue ? ReflectedXss.Value.ToString().ToLowerInvariant() : null);
            AppendDirective(policyBuilder, _reportDirectiveFormat, ReportUri);
            AppendSandboxDirective(policyBuilder);
            AppendDirectiveWithInlineExecution(context, policyBuilder, ScriptDirective, ScriptSources, ScriptInlineExecution);
            AppendDirectiveWithInlineExecution(context, policyBuilder, StyleDirective, StyleSources, StyleInlineExecution);

            if (policyBuilder.Length > 0)
            {
                context.HttpContext.Response.Headers.Add(ContentSecurityPolicyHeader, policyBuilder.ToString());
                context.HttpContext.Response.OnStarting(ResponseOnStarting, context.HttpContext);
            }
        }
        #endregion

        #region Private Methods
        private void AppendDirective(StringBuilder policyBuilder, string directiveFormat, string source)
        {
            if (!String.IsNullOrWhiteSpace(source))
            {
                policyBuilder.AppendFormat(directiveFormat, source);
            }
        }

        private void AppendReferrerDirective(StringBuilder policyBuilder)
        {
            if (Referrer.HasValue)
            {
                switch (Referrer.Value)
                {
                    case ContentSecurityPolicyReferrer.NoReferrer:
                        AppendDirective(policyBuilder, _referrerDirectiveFormat, _noReferrer);
                        break;
                    case ContentSecurityPolicyReferrer.NoReferrerWhenDowngrade:
                        AppendDirective(policyBuilder, _referrerDirectiveFormat, _noReferrerWhenDowngrade);
                        break;
                    case ContentSecurityPolicyReferrer.Origin:
                        AppendDirective(policyBuilder, _referrerDirectiveFormat, _origin);
                        break;
                    case ContentSecurityPolicyReferrer.OriginWhenCrossOrigin:
                        AppendDirective(policyBuilder, _referrerDirectiveFormat, _originWhenCrossOrigin);
                        break;
                    case ContentSecurityPolicyReferrer.UnsafeUrl:
                        AppendDirective(policyBuilder, _referrerDirectiveFormat, _unsafeUrl);
                        break;
                }
            }
        }

        private void AppendSandboxDirective(StringBuilder policyBuilder)
        {
            if (Sandbox)
            {
                policyBuilder.Append(_sandboxDirective);

                if (SandboxFlags != ContentSecurityPolicySandboxFlags.None)
                {
                    AppendSandboxFlag(policyBuilder, ContentSecurityPolicySandboxFlags.AllowForms, _allowFormsSandboxFlag);
                    AppendSandboxFlag(policyBuilder, ContentSecurityPolicySandboxFlags.AllowPointerLock, _allowPointerLockSandboxFlag);
                    AppendSandboxFlag(policyBuilder, ContentSecurityPolicySandboxFlags.AllowPopups, _allowPopupsSandboxFlag);
                    AppendSandboxFlag(policyBuilder, ContentSecurityPolicySandboxFlags.AllowSameOrigin, _allowSameOriginSandboxFlag);
                    AppendSandboxFlag(policyBuilder, ContentSecurityPolicySandboxFlags.AllowScripts, _allowScriptsSandboxFlag);
                    AppendSandboxFlag(policyBuilder, ContentSecurityPolicySandboxFlags.AllowTopNavigation, _allowTopNavigationSandboxFlag);
                }

                policyBuilder.Append(_directivesDelimiter);
            }
        }

        private void AppendDirectiveWithInlineExecution(ActionExecutedContext context, StringBuilder policyBuilder, string directive, string source, ContentSecurityPolicyInlineExecution inlineExecution)
        {
            if (!String.IsNullOrWhiteSpace(source) || (inlineExecution != ContentSecurityPolicyInlineExecution.Refuse))
            {
                policyBuilder.Append(directive);

                if (!String.IsNullOrWhiteSpace(source))
                {
                    policyBuilder.AppendFormat(" {0}", source);
                }

                context.HttpContext.Items[InlineExecutionContextKeys[directive]] = inlineExecution;
                switch (inlineExecution)
                {
                    case ContentSecurityPolicyInlineExecution.Unsafe:
                        policyBuilder.Append(_unsafeInlineSource);
                        break;
                    case ContentSecurityPolicyInlineExecution.Nonce:
                        string nonceRandom = GetNonceRandom(context);
                        policyBuilder.AppendFormat(_nonceSourceFormat, nonceRandom);
                        break;
                    case ContentSecurityPolicyInlineExecution.Hash:
                        context.HttpContext.Items[HashListBuilderContextKeys[directive]] = new StringBuilder();
                        policyBuilder.Append(_hashListPlaceholders[directive]);
                        break;
                    default:
                        break;
                }

                policyBuilder.Append(_directivesDelimiter);
            }
        }

        private string GetNonceRandom(ActionExecutedContext context)
        {
            string nonceRandom;
            if (context.HttpContext.Items.ContainsKey(NonceRandomContextKey))
            {
                nonceRandom = (string)context.HttpContext.Items[NonceRandomContextKey];
            }
            else
            {
                nonceRandom = Guid.NewGuid().ToString("N");
                context.HttpContext.Items[NonceRandomContextKey] = nonceRandom;
            }

            return nonceRandom;
        }

        private void AppendSandboxFlag(StringBuilder policyBuilder, ContentSecurityPolicySandboxFlags flag, string flagValue)
        {
            if (SandboxFlags.HasFlag(flag))
            {
                policyBuilder.Append(flagValue);
            }
        }

        private Task ResponseOnStarting(object state)
        {
            HttpContext httpContext = (HttpContext)state;

            string contentSecurityPolicyHeaderValue = httpContext.Response.Headers[ContentSecurityPolicyHeader];

            if (!String.IsNullOrWhiteSpace(contentSecurityPolicyHeaderValue))
            {
                if (ScriptInlineExecution == ContentSecurityPolicyInlineExecution.Hash)
                {
                    contentSecurityPolicyHeaderValue = contentSecurityPolicyHeaderValue.Replace(_hashListPlaceholders[ScriptDirective], ((StringBuilder)httpContext.Items[HashListBuilderContextKeys[ScriptDirective]]).ToString());
                }

                if (StyleInlineExecution == ContentSecurityPolicyInlineExecution.Hash)
                {
                    contentSecurityPolicyHeaderValue = contentSecurityPolicyHeaderValue.Replace(_hashListPlaceholders[StyleDirective], ((StringBuilder)httpContext.Items[HashListBuilderContextKeys[StyleDirective]]).ToString());
                }

                httpContext.Response.Headers[ContentSecurityPolicyHeader] = contentSecurityPolicyHeaderValue;
            }

            return Task.FromResult<object>(null);
        }
        #endregion
    }
}
