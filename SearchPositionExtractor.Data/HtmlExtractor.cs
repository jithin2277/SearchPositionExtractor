using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchPositionExtractor.Data
{
    public interface IHtmlExtractor
    {
        Task<string> GetHtmlString(string url);
    }

    public class HtmlExtractor : IHtmlExtractor
    {
        public async Task<string> GetHtmlString(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            using (var client = new WebClient())
            {
                return await client.DownloadStringTaskAsync(url);
            }
        }
    }
}
