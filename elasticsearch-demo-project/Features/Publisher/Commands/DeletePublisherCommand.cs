using elasticsearch_demo_project.Dtos;
using elasticsearch_demo_project.Interfaces;
using MediatR;

namespace elasticsearch_demo_project.Features.Publisher.Commands
{
    public class DeletePublisherCommand : IRequest<PublisherDto>
    {
        public string? PublisherCode { get; set; }
    }

    public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand, PublisherDto>
    {
        private readonly IPublisherRepository _publisherRepository;

        public DeletePublisherCommandHandler(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task<PublisherDto> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
        {
            await _publisherRepository.DeleteAsync(request.PublisherCode);
            return new PublisherDto();
        }
    }
}
