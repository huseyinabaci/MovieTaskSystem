using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskSystem.Application.Abstractions.Movie.Contracts;
using TaskSystem.Common.Model;

/// <summary>
/// Defines the contract for movie-related operations.
/// </summary>
public interface IMovieService
{
    /// <summary>
    /// Creates a new movie record.
    /// </summary>
    /// <param name="requestDto">Data transfer object containing movie details.</param>
    /// <returns>A service response indicating success or failure.</returns>
    Task<ServiceResponse> CreateMovieAsync(MovieCreateRequestDto requestDto);

    /// <summary>
    /// Retrieves all movies.
    /// </summary>
    /// <returns>A service response containing a list of movies or an error message.</returns>
    Task<ServiceResponse<List<ListResponseDto>>> GetAllMoviesAsync();

    /// <summary>
    /// Updates an existing movie record.
    /// </summary>
    /// <param name="requestDto">Data transfer object containing updated movie details.</param>
    /// <returns>A service response indicating success or failure.</returns>
    Task<ServiceResponse> UpdateMovieAsync(MovieRequestDto requestDto);

    /// <summary>
    /// Deletes a movie record by its ID.
    /// </summary>
    /// <param name="id">The ID of the movie to delete.</param>
    /// <returns>A service response indicating success or failure.</returns>
    Task<ServiceResponse> DeleteMovieAsync(string id);

    /// <summary>
    /// Creates a new director record.
    /// </summary>
    /// <param name="requestDto">Data transfer object containing director details.</param>
    /// <returns>A service response indicating success or failure.</returns>
    Task<ServiceResponse> CreateDirectorAsync(DirectorCreateRequestDto requestDto);

    /// <summary>
    /// Deletes a director record by its ID.
    /// </summary>
    /// <param name="id">The ID of the director to delete.</param>
    /// <returns>A service response indicating success or failure.</returns>
    Task<ServiceResponse> DeleteDirectorAsync(string id);
}