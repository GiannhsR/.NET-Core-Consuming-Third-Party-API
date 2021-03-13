using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.DAL.Models
{
    public class SearchViewModel
    {
        public DateTime Date { get; set; }
        public long SearchedTimes { get; set; }
        public string SearchedSummonerName { get; set; }
    }
}
