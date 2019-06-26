using Microsoft.Extensions.Configuration;
using Nest;
using System;

namespace WebAdvert.SearchWorker
{
    public static class ElasticSearchHelper
    {
        private static IElasticClient _client;
        public static IElasticClient GetInstance(IConfiguration configuration)
        {
            if(_client == null)
            {
                var url = configuration.GetSection("ElasticSearch").GetValue<string>("Url");
                var settings = new ConnectionSettings(new Uri(url))
                    .DefaultIndex("adverts")
                    .DefaultTypeName("advert")
                    .DefaultMappingFor<AdvertMessageModel>(m => m.IdProperty(x => x.Id));
                _client = new ElasticClient(settings);
            }
            return _client;
        }
    }
}
