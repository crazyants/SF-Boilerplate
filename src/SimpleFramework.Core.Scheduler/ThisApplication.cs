using System.Net.Http;
using System.Threading.Tasks;
using SimpleFramework.Core.Scheduler.Contracts;

namespace SimpleFramework.Core.Scheduler
{
    /// <summary>
    /// DNTScheduler needs a ping service to keep it alive.
    /// This class provides the SiteRootUrl for the PingTask.
    /// </summary>
    public class ThisApplication : IThisApplication
    {
        private readonly HttpClient _client = new HttpClient();

        /// <summary>
        /// The site's root url.
        /// </summary>
        public string SiteRootUrl { set; get; }

        /// <summary>
        /// Pings the site's root url.
        /// </summary>
        public async Task WakeUp()
        {
            if (string.IsNullOrWhiteSpace(SiteRootUrl))
            {
                return;
            }

            _client.DefaultRequestHeaders.ConnectionClose = true;
            _client.DefaultRequestHeaders.Add("User-Agent", "DNTScheduler 1.0");
            await _client.GetStringAsync(SiteRootUrl).ConfigureAwait(false);
        }
    }
}