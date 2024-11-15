namespace elasticsearch_demo_project.Settings
{
    public class ElasticsearchSettings
    {
        public string? Uri { get; set; }
        public string? IndexName { get; set; }
        public int Shards { get; set; }
        public int Replicas { get; set; }
    }
}
