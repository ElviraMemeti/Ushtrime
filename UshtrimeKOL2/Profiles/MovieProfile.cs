

using AutoMapper;
using SOA2024.MovieReview.API.Dtos;
using SOA2024.MovieReview.API.Models;

namespace SOA2024.MovieReview.API.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.DirectorName, opt => opt.MapFrom(x => x.Director.Name));

            CreateMap<CreateMovieDto, Movie>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => Guid.NewGuid()));

            CreateMap<UpdateMovieDto, Movie>();

        }
    }
}

