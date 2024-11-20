using elasticsearch_demo_project.Dtos;
using elasticsearch_demo_project.Interfaces;
using MediatR;

namespace elasticsearch_demo_project.Features.Publisher.Queries
{
    public class GetPublisherQuery : IRequest<IEnumerable<PublisherDto>>
    {
        public IEnumerable<string>? PublisherCodes { get; set; }
    }

    public class GetPublisherQueryHandler : IRequestHandler<GetPublisherQuery, IEnumerable<PublisherDto>>
    {
        private readonly IPublisherRepository _publisherRepository;

        public GetPublisherQueryHandler(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task<IEnumerable<PublisherDto>> Handle(GetPublisherQuery request, CancellationToken cancellationToken)
        {
            var data = await _publisherRepository.GetPublishersAsync(request.PublisherCodes);
            return data;
        }
    }
}
