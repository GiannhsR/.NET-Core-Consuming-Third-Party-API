using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestCore.Services.BackgroundServices
{
    public class IBackgroundServices : BackgroundService
    {
        private const string DataDragonChampionsURI = "http://ddragon.leagueoflegends.com/cdn/11.4.1/data/en_US/champion.json";
        private readonly IHttpClientFactory _httpClientFactory;
        public IBackgroundServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, DataDragonChampionsURI);
                var client = _httpClientFactory.CreateClient();
                var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                var JsonResponse = JsonConvert.DeserializeObject(content);
                await Task.Delay(TimeSpan.FromDays(30),stoppingToken);
            }
        }
    }
}
