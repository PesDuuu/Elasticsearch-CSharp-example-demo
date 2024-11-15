using Elastic.Clients.Elasticsearch;
using elasticsearch_demo_project.Models;

namespace elasticsearch_demo_project.Services
{
    public class ElasticsearchService
    {
        private readonly ElasticsearchClient _client;

        public ElasticsearchService(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task IndexProduct(Product product)
        {
            await _client.IndexAsync(product, idx => idx.Index("products"));
        }

        public async Task<Product?> GetProduct(int id)
        {
            var response = await _client.GetAsync<Product?>(id, idx => idx.Index("products"));
            return response.Found ? response.Source : null;
        }

        public async Task DeleteProduct(int id)
        {
            await _client.DeleteAsync<Product>(id, idx => idx.Index("products"));
        }

        public async Task<IEnumerable<Product>> SearchProducts(string query)
        {
            var response = await _client.SearchAsync<Product>(s => s
                .Index("products")
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Name)
                        .Query(query)
                    )
                )
            );
            return response.Documents;
        }
    }
}
