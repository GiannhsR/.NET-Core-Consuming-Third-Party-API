using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCore.DAL.Models;

namespace TestCore.DAL.RepositoryServices
{
    public class IChampionsService
    {
        private readonly IMongoCollection<Champion> _champions;

        public IChampionsService(IDataDragonChampionDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _champions = database.GetCollection<Champion>(settings.ChampionsCollectionName);
        }

        public async Task<List<Champion>> GetAsync()
        {
            return await _champions.Find(champion => true).ToListAsync();
        }

        public async Task<Champion> GetAsync(string key)
        {
            return await _champions.Find<Champion>(champion => champion.ChampionKey == key).FirstOrDefaultAsync();
        }
    }
}
