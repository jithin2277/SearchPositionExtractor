using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchPositionExtractor.Api
{
    public class SearchPosition
    {
        public int PageNumber { get; set; }
        public IList<int> Positions { get; set; }
    }
}
