using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchPositionExtractor.Data
{
    public interface IPostionExtractor
    {
        Task<IList<int>> GetPositions(string url, string term);
    }

    public class PostionExtractor : IPostionExtractor
    {
        private readonly IHtmlParser _htmlParser;

        public PostionExtractor(IHtmlParser htmlParser)
        {
            _htmlParser = htmlParser ?? throw new ArgumentNullException(nameof(htmlParser));
        }

        public async Task<IList<int>> GetPositions(string url, string term)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            if (string.IsNullOrEmpty(term))
            {
                throw new ArgumentNullException(nameof(term));
            }

            var postions = new List<int>();
            term = term.ToLower();
            var elements = await _htmlParser.GetMatchingElements(url, Constants.GOOGLE_SEARCHRESULTS_PARSER_EXPRESSION);
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].ToLower().Contains(term))
                {
                    postions.Add(i+1);
                }
            }

            return postions;
        }
    }
}
