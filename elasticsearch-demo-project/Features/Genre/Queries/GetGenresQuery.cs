using elasticsearch_demo_project.Dtos;
using elasticsearch_demo_project.Interfaces;
using MediatR;

namespace elasticsearch_demo_project.Features.Genre.Queries
{
    public class GetGenresQuery : IRequest<IEnumerable<GenreDto>>
    {
        public IEnumerable<string>? GenreCodes { get; set; }
    }

    public class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, IEnumerable<GenreDto>>
    {
        private readonly IGenreRepository _genreRepository;

        public GetGenresQueryHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<GenreDto>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            var data = await _genreRepository.GetGenresAsync(request.GenreCodes);
            return data;
        }
    }
}
