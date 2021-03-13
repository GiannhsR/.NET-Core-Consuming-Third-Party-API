using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCore.DAL.Models;
using TestCore.DTO;

namespace TestCore.DAL.RepositoryServices
{
    public class IMyDatabaseService
    {
        private readonly AppDbContext _appDbContext;
        public IMyDatabaseService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ICollection<SearchViewModel>> GetSearchesAsync()
        {
            return await _appDbContext.Searches
                .Where(s => s.SearchedTimes > 0)
                .Select(s => new SearchViewModel
                {
                    Date = s.Date,
                    SearchedTimes = s.SearchedTimes,
                    SearchedSummonerName = s.SearchedSummonerName
                })
                .ToListAsync();
        }

        public async Task<SearchViewModel> GetSingleSearchAsync(int id)
        {
            return await _appDbContext.Searches
                .Where(s => s.SearchesId == id)
                .Select(s => new SearchViewModel
                {
                    Date = s.Date,
                    SearchedTimes = s.SearchedTimes,
                    SearchedSummonerName = s.SearchedSummonerName
                })
                .SingleOrDefaultAsync();

        }

        public async Task UpdateSearch(SummonerDTO summonerDTO)
        {
            var search = await _appDbContext.Searches
                .Where(s => s.SearchedSummonerName == summonerDTO.Name)
                .FirstOrDefaultAsync();
            if(search == null)
            {
                throw new Exception("Unable to find the 'search'.");
            }
            UpdateSearch(search, summonerDTO);
            await _appDbContext.SaveChangesAsync();
        }

        public void UpdateSearch(Searches search, SummonerDTO summonerDTO)
        {
            search.Date = System.DateTime.Now;
            search.SearchedTimes += 1;
        }
        public async Task<int> StoreSearchQuery(SummonerDTO summonerDTO)
        {
            var search = new Searches
            {
                Date = System.DateTime.Now,
                SearchedTimes = 1,
                SearchedSummonerName = summonerDTO.Name
            };
            _appDbContext.Add(search);
            await _appDbContext.SaveChangesAsync();
            return search.SearchesId;
        }
    }
}
