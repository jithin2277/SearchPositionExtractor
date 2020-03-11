using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace SearchPositionExtractor.Data
{
    public interface IHtmlParser
    {
        Task<IList<string>> GetMatchingElements(string url, string expression);
    }

    public class HtmlParser : IHtmlParser
    {
        private readonly IHtmlExtractor _htmlExtractor;

        public HtmlParser(IHtmlExtractor htmlExtractor)
        {
            _htmlExtractor = htmlExtractor ?? throw new ArgumentNullException(nameof(htmlExtractor));
        }

        public async Task<IList<string>> GetMatchingElements(string url, string expression)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(url);
            }
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentNullException(expression);
            }

            var elements = new List<string>();
            var htmlString = await _htmlExtractor.GetHtmlString(url).ConfigureAwait(false);
            var regExpression = new Regex(expression);
            var matches = regExpression.Matches(htmlString);
            var matchEnumerator = matches.GetEnumerator();
            while (matchEnumerator.MoveNext())
            {
                elements.Add(matchEnumerator.Current.ToString());
            }
            
            return elements;
        }
    }
}
