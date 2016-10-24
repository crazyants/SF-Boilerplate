using System.Threading.Tasks;

namespace SimpleFramework.Core.Scheduler.Contracts
{
    /// <summary>
    /// DNTScheduler needs a ping service to keep it alive.
    /// This class provides the SiteRootUrl for the PingTask.
    /// </summary>
    public interface IThisApplication
    {
        /// <summary>
        /// The site's root url.
        /// </summary>
        string SiteRootUrl { set; get; }

        /// <summary>
        /// Pings the site's root url.
        /// </summary>
        /// <returns></returns>
        Task WakeUp();
    }
}