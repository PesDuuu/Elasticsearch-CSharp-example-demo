using elasticsearch_demo_project.Dtos;
using elasticsearch_demo_project.Interfaces;
using MediatR;

namespace elasticsearch_demo_project.Features.Genre.Commands
{
    public class AddGenreCommand : IRequest<GenreDto>
    {
        public GenreDto? Genre { get; set; }
    }

    public class AddGenreCommandHandler : IRequestHandler<AddGenreCommand, GenreDto>
    {
        private readonly IGenreRepository _genreRepository;

        public AddGenreCommandHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<GenreDto> Handle(AddGenreCommand request, CancellationToken cancellationToken)
        {
            await _genreRepository.AddAsync(request.Genre);
            return new GenreDto()
            {
                GenreCode = request.Genre.GenreCode,
                GenreName = request.Genre.GenreName,
            };
        }
    }
}
