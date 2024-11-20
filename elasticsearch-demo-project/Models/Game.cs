using elasticsearch_demo_project.Common;

namespace elasticsearch_demo_project.Models
{
    public class Game : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? PublisherCode { get; set; }
        public ICollection<GameGenre> GameGenres { get; set; } = new List<GameGenre>();
        public Publisher? Publisher { get; set; }
    }
}
