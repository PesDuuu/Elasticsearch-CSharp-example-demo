using AutoMapper;
using elasticsearch_demo_project.Dtos;
using elasticsearch_demo_project.Models;

namespace elasticsearch_demo_project.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<Publisher, PublisherDto>().ReverseMap();
        }
    }
}
