using AutoMapper;
using System.Collections.Generic;
using TaskSystem.Application.Abstractions.Movie.Contracts;
using TaskSystem.WebApi.Model.Movie;

namespace TaskSystem.WebApi.Mappings
{
    /// <summary>
    /// Defines mapping configurations for movie-related models and DTOs.
    /// </summary>
    public class MovieProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovieProfile"/> class.
        /// Configures mappings between Web API models and application DTOs.
        /// </summary>
        public MovieProfile()
        {
            /// <summary>
            /// Maps between <see cref="MovieRequest"/> and <see cref="MovieRequestDto"/>.
            /// </summary>
            _ = CreateMap<MovieRequest, MovieRequestDto>().ReverseMap();

            /// <summary>
            /// Maps between <see cref="MovieCreateRequest"/> and <see cref="MovieCreateRequestDto"/>.
            /// </summary>
            _ = CreateMap<MovieCreateRequest, MovieCreateRequestDto>().ReverseMap();

            /// <summary>
            /// Maps between <see cref="DirectorRequest"/> and <see cref="DirectorRequestDto"/>.
            /// </summary>
            _ = CreateMap<DirectorRequest, DirectorRequestDto>().ReverseMap();

            /// <summary>
            /// Maps between <see cref="DirectorCreateRequest"/> and <see cref="DirectorCreateRequestDto"/>.
            /// </summary>
            _ = CreateMap<DirectorCreateRequest, DirectorCreateRequestDto>().ReverseMap();

            /// <summary>
            /// Maps between <see cref="ListResponseDto"/> and <see cref="ListResponse"/>.
            /// </summary>
            _ = CreateMap<ListResponseDto, ListResponse>().ReverseMap();
        }
    }
}