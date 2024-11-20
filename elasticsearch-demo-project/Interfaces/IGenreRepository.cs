using elasticsearch_demo_project.Dtos;

namespace elasticsearch_demo_project.Interfaces
{
    public interface IGenreRepository
    {
        Task<IEnumerable<GenreDto>> GetGenresAsync(IEnumerable<string>? genreCodes = null);

        Task AddAsync(GenreDto genreDto);

        Task UpdateAsync(string genreCode, GenreDto genreDto);

        Task DeleteAsync(string genreCode);

    }
}
