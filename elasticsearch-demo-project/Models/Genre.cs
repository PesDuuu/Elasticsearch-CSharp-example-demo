using elasticsearch_demo_project.Common;

namespace elasticsearch_demo_project.Models
{
    public class Genre : BaseEntity
    {
        public string? GenreCode { get; set; }
        public string? GenreName { get; set; }
        public ICollection<GameGenre> GameGenres { get; set; } = new List<GameGenre>();

    }
}
