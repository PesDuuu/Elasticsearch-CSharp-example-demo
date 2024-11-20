using elasticsearch_demo_project.Dtos;
using elasticsearch_demo_project.Interfaces;
using MediatR;

namespace elasticsearch_demo_project.Features.Genre.Commands
{
    public class DeleteGenreCommand : IRequest<GenreDto>
    {
        public string? GenreCode { get; set; }
    }

    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, GenreDto>
    {
        private readonly IGenreRepository _genreRepository;

        public DeleteGenreCommandHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<GenreDto> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            await _genreRepository.DeleteAsync(request.GenreCode);
            return new GenreDto();
        }
    }
}
