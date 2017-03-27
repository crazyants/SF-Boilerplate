using System.Linq;
using System.Reflection;

namespace SF.Core.Infrastructure.Modules
{
    public class ModuleInfo
    {

        public string Name { get; set; }

        public Assembly Assembly { get; set; }

        public string ShortName
        {
            get
            {
                return Name.Split('.').Last();
            }
        }

        public string Path { get; set; }

        public ModuleConfig Config { get; set; } = new ModuleConfig();
    }

    public class ModuleConfig
    {
        /// <summary>
        /// 模块标识
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 平台版本
        /// </summary>
        public string PlatformVersion { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Authors { get; set; }
        /// <summary>
        /// 拥有者
        /// </summary>
        public string Owners { get; set; }
        /// <summary>
        /// 图标URL
        /// </summary>
        public string IconUrl { get; set; }
        /// <summary>
        /// 发布说明
        /// </summary>
        public string ReleaseNotes { get; set; }
        /// <summary>
        /// 版权
        /// </summary>
        public string Copyright { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 是否系统模块，如果是系统模块即不能卸载
        /// </summary>
        public bool SystemModule { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
