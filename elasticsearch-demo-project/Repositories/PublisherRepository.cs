using AutoMapper;
using elasticsearch_demo_project.Contexts;
using elasticsearch_demo_project.Dtos;
using elasticsearch_demo_project.Interfaces;
using elasticsearch_demo_project.Models;
using Microsoft.EntityFrameworkCore;

namespace elasticsearch_demo_project.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PublisherRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PublisherDto>> GetPublishersAsync(IEnumerable<string>? publisherCodes = null)
        {
            if (publisherCodes == null || !publisherCodes.Any())
            {
                var publishers = await _context.Publishers.ToListAsync();
                return _mapper.Map<IEnumerable<PublisherDto>>(publishers);
            }

            var filteredPublishers = await _context.Publishers
                .Where(p => publisherCodes.Contains(p.PublisherCode))
                .ToListAsync();

            return _mapper.Map<IEnumerable<PublisherDto>>(filteredPublishers);
        }

        public async Task AddAsync(PublisherDto publisherDto)
        {
            var publisher = _mapper.Map<Publisher>(publisherDto);
            await _context.Publishers.AddAsync(publisher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(string publisherCode, PublisherDto publisherDto)
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.PublisherCode == publisherCode);

            if (publisher == null)
            {
                throw new KeyNotFoundException($"Publisher with code '{publisherCode}' not found.");
            }

            _mapper.Map(publisherDto, publisher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string publisherCode)
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(g => g.PublisherCode == publisherCode);

            if (publisher == null)
            {
                throw new KeyNotFoundException($"Genre with code '{publisherCode}' not found.");
            }

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
        }
    }
}
