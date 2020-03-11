using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SearchPositionExtractor.Data;

namespace SearchPositionExtractor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchPositionExtractorController : ControllerBase
    {
        private readonly IPostionExtractor _postionExtractor;

        public SearchPositionExtractorController(IPostionExtractor postionExtractor)
        {
            _postionExtractor = postionExtractor ?? throw new ArgumentNullException(nameof(postionExtractor));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SearchPosition>>> Get(string searchTerm, string positionTerm, int pages = 10)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term cannot be blank");
            }
            if (string.IsNullOrEmpty(positionTerm))
            {
                return BadRequest("Postion term cannot be blank");
            }

            var searchPositions = new List<SearchPosition>();
            var url = $"{Constants.GOOGLE_SEARCH_URL}?q={searchTerm.Trim().Replace(" ", "+")}";
            for (int i = 0; i < pages; i++)
            {
                string pageString = (i == 0) ? string.Empty : $"&start={i * 10}";

                searchPositions.Add(new SearchPosition { PageNumber = i + 1, Positions = await _postionExtractor.GetPositions($"{url}{pageString}", positionTerm) });
            }

            return Ok(searchPositions);
        }
    }
}
