using AutoMapper;
using elasticsearch_demo_project.Contexts;
using elasticsearch_demo_project.Dtos;
using elasticsearch_demo_project.Interfaces;
using elasticsearch_demo_project.Models;
using Microsoft.EntityFrameworkCore;

namespace elasticsearch_demo_project.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GenreRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreDto>> GetGenresAsync(IEnumerable<string>? genreCodes = null)
        {
            if (genreCodes == null || !genreCodes.Any())
            {
                var allGenres = await _context.Genres.ToListAsync();
                return _mapper.Map<IEnumerable<GenreDto>>(allGenres);
            }

            var genres = await _context.Genres
                .Where(g => genreCodes.Contains(g.GenreCode))
                .ToListAsync();

            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }

        public async Task AddAsync(GenreDto genreDto)
        {
            var genre = _mapper.Map<Genre>(genreDto);

            if (await _context.Genres.AnyAsync(g => g.GenreCode == genre.GenreCode))
            {
                throw new ArgumentException($"Genre with code '{genre.GenreCode}' already exists.");
            }

            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(string genreCode, GenreDto genreDto)
        {
            var existingGenre = await _context.Genres.FirstOrDefaultAsync(g => g.GenreCode == genreCode);

            if (existingGenre == null)
            {
                throw new KeyNotFoundException($"Genre with code '{genreCode}' not found.");
            }

            existingGenre.GenreName = genreDto.GenreName;

            _context.Genres.Update(existingGenre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string genreCode)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.GenreCode == genreCode);

            if (genre == null)
            {
                throw new KeyNotFoundException($"Genre with code '{genreCode}' not found.");
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
    }
}
