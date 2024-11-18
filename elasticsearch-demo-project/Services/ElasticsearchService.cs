using Elastic.Clients.Elasticsearch;
using elasticsearch_demo_project.Models;
using elasticsearch_demo_project.Settings;
using Microsoft.Extensions.Options;

namespace elasticsearch_demo_project.Services
{
    public class ElasticsearchService
    {
        private readonly ElasticsearchClient _client;
        private readonly ElasticsearchSettings _settings;

        public ElasticsearchService(ElasticsearchClient client, IOptions<ElasticsearchSettings> settings)
        {
            _client = client;
            _settings = settings.Value;
        }

        public async Task IndexProduct(Product product)
        {
            try
            {
                await _client.IndexAsync(product, idx => idx.Index(_settings.IndexName));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to index product with ID {product.Id}: {ex.Message}", ex);
            }
        }

        public async Task<Product?> GetProduct(int id)
        {
            try
            {
                var response = await _client.GetAsync<Product?>(id, idx => idx.Index(_settings.IndexName));
                return response.Found ? response.Source : null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get product with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                await _client.DeleteAsync<Product>(id, idx => idx.Index(_settings.IndexName));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete product with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Product>> SearchProducts(string query)
        {
            try
            {
                var response = await _client.SearchAsync<Product>(s => s
                    .Index(_settings.IndexName)
                    .Query(q => q
                        .Match(m => m
                            .Field(f => f.Name)
                            .Query(query)
                        )
                    )
                );
                return response.Documents;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to search products with query '{query}': {ex.Message}", ex);
            }
        }

        public async Task UpdateProduct(Product product)
        {
            try
            {
                var response = await _client.UpdateAsync<Product, Product>(product.Id, u => u
                    .Index(_settings.IndexName)
                    .Doc(product) // Only the fields that have changed will be updated
                );

                if (!response.IsValidResponse)
                {
                    throw new Exception($"Failed to update product with ID {product.Id}");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"Failed to update product with ID {product.Id}: {ex.Message}", ex);
            }
        }
    }
}
