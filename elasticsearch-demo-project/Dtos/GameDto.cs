namespace elasticsearch_demo_project.Dtos
{
    public class GameDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? PublisherCode { get; set; }
        public List<GenreDto> Genres { get; set; } = new List<GenreDto>();
    }
}
