using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.DTO
{
    public class MatchHistoryDTO
    {
        public int StartIndex { get; set; }
        public int TotalGames { get; set; }
        public int EndIndex { get; set; }
        public List<MatchReferenceDTO> Matches { get; set; }
    }
}
