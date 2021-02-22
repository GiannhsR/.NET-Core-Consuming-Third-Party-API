using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.Services
{
    public class IRetrieveRegionService
    {
        public string RetrieveRegion(string selectedRegion)
        {
            return selectedRegion switch
            {
                "EUNE" => "eun1.api.riotgames.com",
                "EUW" => "euw1.api.riotgames.com",
                "NA" => "na1.api.riotgames.com",
                _ => "eun1.api.riotgames.com",
            };
        }
    }
}
