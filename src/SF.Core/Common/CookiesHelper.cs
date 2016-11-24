/*******************************************************************************
* 命名空间: SF.Core.Common
*
* 功 能： N/A
* 类 名： CookiesHelper
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/23 9:32:51 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Common
{
    public static class CookiesHelper
    {
        public static string CookieName { get; set; } = "";
        public static string LoginCookieName { get; set; } = "ll";

        private static string CookieKey(string cookieName)
        {
            return cookieName + CookieName;
        }

        public static void DeleteCookie(HttpContext context, string cookieName)
        {
            string key = CookieKey(cookieName);

            context.Response.Cookies.Delete(key);
        }

        public static void WriteCookie(HttpContext context, string cookieName, string value, int months)
        {
            WriteCookie(context, cookieName, value, months, 0);
        }

        public static void WriteCookie(HttpContext context, string cookieName, string value, int months, int days)
        {
            string key = CookieKey(cookieName);

            if (!context.Request.Cookies.ContainsKey(key))
            {
                DateTime expires = DateTime.Today.AddDays(30 * months + days);
                DateTimeOffset dateAndOffset = new DateTimeOffset(expires,
                                            TimeZoneInfo.Local.GetUtcOffset(expires));

                context.Response.Cookies.Append(key, value,
                    new CookieOptions
                    {
                        Expires = expires
                    });
            }
        }

        public static string ReadCookie(HttpContext context, string cookieName)
        {
            string key = CookieKey(cookieName);

            try
            {
                return System.Net.WebUtility.UrlDecode(context.Request.Cookies[key]);
            }
            catch
            {
                return "";
            }
        }
    }
}
