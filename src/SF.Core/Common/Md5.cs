
using System;
using System.Security.Cryptography;
using System.Text;

namespace SF.Core.Common
{
    /// <summary>
    /// MD5加密
    /// </summary>
    public class Md5
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns></returns>
        public static string md5(string str, int code)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                var strResult = BitConverter.ToString(result);
                var r = strResult.Replace("-", "");
                if (code == 16)
                {
                    return r.Substring(8, 16);
                }
                else
                {
                    return r;
                }
            }

            //string strEncrypt = string.Empty;
            //if (code == 16)
            //{
            //    strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
            //}

            //if (code == 32)
            //{
            //    strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            //}

            //return strEncrypt;

        }
    }
}
