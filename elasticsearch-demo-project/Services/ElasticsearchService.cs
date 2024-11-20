using Elastic.Clients.Elasticsearch;
using elasticsearch_demo_project.Settings;
using Microsoft.Extensions.Options;

namespace elasticsearch_demo_project.Services
{
    public class ElasticsearchService
    {
        private readonly ElasticsearchClient _client;
        private readonly ElasticsearchSettings _settings;

        public ElasticsearchService(IOptions<ElasticsearchSettings> settings)
        {
            _settings = settings.Value;
            var clientSettings = new ElasticsearchClientSettings(new Uri(_settings.Uri))
                .DefaultIndex(_settings.IndexName);
            _client = new ElasticsearchClient(clientSettings);

            CreateIndexAsync()
                .GetAwaiter()
                .GetResult();
        }

        public ElasticsearchClient Client => _client;

        public async Task CreateIndexAsync()
        {
            var response = await _client.Indices.CreateAsync(_settings.IndexName, c => c
                .Settings(s => s
                    .NumberOfShards(_settings.Shards)
                    .NumberOfReplicas(_settings.Replicas)
                )
            );

            if (!response.IsValidResponse)
            {
                throw new Exception($"Failed to create index '{_settings.IndexName}': {response.DebugInformation}");
            }
        }
    }
}
