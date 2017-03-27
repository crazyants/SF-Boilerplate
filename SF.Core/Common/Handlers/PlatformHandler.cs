/*******************************************************************************
* 命名空间: SF.Core.Common.Handlers
*
* 功 能： N/A
* 类 名： PlatformHandler
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/1 10:12:00 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System.Runtime.InteropServices;

namespace SF.Core.Common.Handlers
{
    public class PlatformHandler
    {
        static PlatformHandler()
        {

            var currentPlatform = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? OSPlatform.Windows
                : RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                ? OSPlatform.OSX
                : OSPlatform.Linux;
            Platform = new Platform(currentPlatform);
        }

        public static Platform Platform { get; set; }
    }

    public class Platform
    {
        public Platform(OSPlatform platform)
        {
            OSPlatform = platform;
        }

        public OSPlatform OSPlatform { get; set; }

        public bool IsLinux { get { return OSPlatform == OSPlatform.Linux; } }

        public bool IsMac { get { return OSPlatform == OSPlatform.OSX; } }

        public bool IsWindows { get { return OSPlatform == OSPlatform.Windows; } }
    }
}
