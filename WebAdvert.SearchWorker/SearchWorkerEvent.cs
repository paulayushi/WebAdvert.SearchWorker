using AdvertApi.Models.Messages;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Nest;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WedbAdvert.SearchWorker;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace WebAdvert.SearchWorker
{
    public class SearchWorkerEvent
    {
        private readonly IElasticClient _client;

        public SearchWorkerEvent(): this(ElasticSearchHelper.GetInstance(ConfigurationHelper.Instance))
        {
        }

        public SearchWorkerEvent(IElasticClient client)
        {
            _client = client;
        }
        public async Task Function(SNSEvent snsEvent, ILambdaContext context)
        {
            foreach (var record in snsEvent.Records)
            {
                context.Logger.LogLine(record.Sns.Message);

                var messages = JsonConvert.DeserializeObject<AdvertConfirmedMessage>(record.Sns.Message);
                var advertDoc = MapHelper.Map(messages);

                await _client.IndexDocumentAsync(advertDoc);
            }
        }
    }
}
