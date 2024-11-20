using elasticsearch_demo_project.Common;

namespace elasticsearch_demo_project.Models
{
    public class GameGenre : BaseEntity
    {
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public string? GenreCode { get; set; }
        public Genre? Genre { get; set; }
    }
}
