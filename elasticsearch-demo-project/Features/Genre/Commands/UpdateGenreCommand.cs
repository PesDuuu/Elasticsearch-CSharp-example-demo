using elasticsearch_demo_project.Dtos;
using elasticsearch_demo_project.Interfaces;
using MediatR;

namespace elasticsearch_demo_project.Features.Genre.Commands
{
    public class UpdateGenreCommand : IRequest<GenreDto>
    {
        public string? GenreCode { get; set; }
        public GenreDto? Genre { get; set; }
    }

    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, GenreDto>
    {
        private readonly IGenreRepository _genreRepository;

        public UpdateGenreCommandHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<GenreDto> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            await _genreRepository.UpdateAsync(request.GenreCode, request.Genre);
            return new GenreDto()
            {
                GenreCode = request.Genre.GenreCode,
                GenreName = request.Genre.GenreName,
            };
        }
    }
}
