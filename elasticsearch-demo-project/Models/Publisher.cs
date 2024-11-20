using elasticsearch_demo_project.Common;

namespace elasticsearch_demo_project.Models
{
    public class Publisher : BaseEntity
    {
        public string? PublisherCode { get; set; }
        public string? PublisherName { get; set; }
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
