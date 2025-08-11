using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskSystem.Common.Model;
using TaskSystem.WebApi.Model.Movie;

namespace TaskSystem.WebApi.Controllers
{
    /// <summary>
    /// API controller for managing movies and directors.
    /// </summary>
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieController"/> class.
        /// </summary>
        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new movie.
        /// </summary>
        /// <param name="request">The movie creation request.</param>
        /// <returns>Returns the result of the creation operation.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMovie([FromBody] MovieCreateRequest request)
        {
            var mappedRequest = _mapper.Map<MovieCreateRequestDto>(request);

            var result = await _movieService.CreateMovieAsync(mappedRequest);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Gets all movies.
        /// </summary>
        /// <returns>Returns a list of all movies.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<List<MovieRequestDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMovies()
        {
            var result = await _movieService.GetAllMoviesAsync();

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            var mappedResult = _mapper.Map<List<ListResponse>>(result.Result);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Updates an existing movie.
        /// </summary>
        /// <param name="request">The movie update request.</param>
        /// <returns>Returns the result of the update operation.</returns>

        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMovie([FromBody] MovieRequest request)
        {
            var mappedRequest = _mapper.Map<MovieRequestDto>(request);

            var result = await _movieService.UpdateMovieAsync(mappedRequest);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Deletes a movie by its ID.
        /// </summary>
        /// <param name="id">The ID of the movie to delete.</param>
        /// <returns>Returns the result of the deletion operation.</returns>

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMovie(string id)
        {
            var result = await _movieService.DeleteMovieAsync(id);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Creates a new director.
        /// </summary>
        /// <param name="request">The director creation request.</param>
        /// <returns>Returns the result of the creation operation.</returns>

        [HttpPost("directors")]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDirector([FromBody] DirectorCreateRequest request)
        {
            var mappedRequest = _mapper.Map<DirectorCreateRequestDto>(request);

            var result = await _movieService.CreateDirectorAsync(mappedRequest);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Deletes a director by its ID.
        /// </summary>
        /// <param name="id">The ID of the director to delete.</param>
        /// <returns>Returns the result of the deletion operation.</returns>

        [HttpDelete("directors/{id}")]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteDirector(string id)
        {
            var result = await _movieService.DeleteDirectorAsync(id);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}