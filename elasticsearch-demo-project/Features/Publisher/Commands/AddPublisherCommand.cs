using elasticsearch_demo_project.Dtos;
using elasticsearch_demo_project.Interfaces;
using MediatR;

namespace elasticsearch_demo_project.Features.Publisher.Commands
{
    public class AddPublisherCommand : IRequest<PublisherDto>
    {
        public PublisherDto? Publisher { get; set; }
    }

    public class AddPublisherCommandHandler : IRequestHandler<AddPublisherCommand, PublisherDto>
    {
        private readonly IPublisherRepository _publisherRepository;

        public AddPublisherCommandHandler(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task<PublisherDto> Handle(AddPublisherCommand request, CancellationToken cancellationToken)
        {
            await _publisherRepository.AddAsync(request.Publisher);
            return new PublisherDto()
            {
                PublisherCode = request.Publisher.PublisherCode,
                PublisherName = request.Publisher.PublisherCode,
            };
        }
    }
}
