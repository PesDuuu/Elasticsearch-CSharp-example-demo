using elasticsearch_demo_project.Dtos;

namespace elasticsearch_demo_project.Interfaces
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<PublisherDto>> GetPublishersAsync(IEnumerable<string>? publisherCodes = null);

        Task AddAsync(PublisherDto publisherDto);

        Task UpdateAsync(string publisherCode, PublisherDto publisherDto);

        Task DeleteAsync(string publisherCode);
    }
}
