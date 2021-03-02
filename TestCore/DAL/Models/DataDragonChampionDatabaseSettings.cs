using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.DAL.Models
{
    public class DataDragonChampionDatabaseSettings : IDataDragonChampionDatabaseSettings
    {
        public string ChampionsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDataDragonChampionDatabaseSettings
    {
        string ChampionsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
