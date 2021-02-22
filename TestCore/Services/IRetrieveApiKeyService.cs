using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.Services
{
    public class IRetrieveApiKeyService
    {
        private readonly IConfiguration _config;

        public IRetrieveApiKeyService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string RetrieveApiKey()
        {
            return _config.GetValue<string>("API_KEY");
        }
    }
}
