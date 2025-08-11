using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskSystem.Application.Abstractions.Movie.Contracts;
using TaskSystem.Common.Helper;
using TaskSystem.Common.Model;
using TaskSystem.Domain;
using TaskSystem.Infrastructure.Repository.Interfaces;

/// <summary>
/// Provides business logic for managing movies and directors.
/// Handles creation, retrieval, updating, and deletion of movie and director records.
/// Utilizes generic repository interfaces for data access abstraction.
/// </summary>
public class MovieService : IMovieService
{
    private readonly IRepository<Movie> _movieRepository;
    private readonly IRepository<Director> _directorRepository;
    private readonly IServiceResponseHelper _serviceResponseHelper;

    /// <summary>
    /// Initializes a new instance of the MovieService class.
    /// </summary>
    /// <param name="movieRepository">Repository for managing movie entities.</param>
    /// <param name="directorRepository">Repository for managing director entities.</param>
    /// <param name="serviceResponseHelper">Helper for creating standardized service responses.</param>

    public MovieService(IRepository<Movie> movieRepository, IRepository<Director> directorRepository, IServiceResponseHelper serviceResponseHelper)
    {
        _movieRepository = movieRepository;
        _directorRepository = directorRepository;
        _serviceResponseHelper = serviceResponseHelper;
    }

    /// <summary>
    /// Creates a new movie record.
    /// </summary>
    /// <param name="requestDto">Data transfer object containing movie details.</param>
    /// <returns>A service response indicating success or failure.</returns>

    public async Task<ServiceResponse> CreateMovieAsync(MovieCreateRequestDto requestDto)
    {
        if (requestDto == null)
            return _serviceResponseHelper.SetError("Request cannot be null");

        var director = await _directorRepository.GetByIdAsync(requestDto.DirectorId.ToString());
        if (director == null)
            return _serviceResponseHelper.SetError("Invalid DirectorId. Director not found.");

        var entity = new Movie
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Title = requestDto.Title,
            Description = requestDto.Description,
            ReleaseDate = requestDto.ReleaseDate,
            Genre = requestDto.Genre,
            Rating = requestDto.Rating,
            ImdbId = requestDto.ImdbId,
            DirectorId = requestDto.DirectorId.ToString()
        };

        await _movieRepository.AddAsync(entity);

        return _serviceResponseHelper.SetSuccess();
    }

    /// <summary>
    /// Retrieves all movies.
    /// </summary>
    /// <returns>A service response containing a list of movies or an error message.</returns>

    public async Task<ServiceResponse<List<ListResponseDto>>> GetAllMoviesAsync()
    {
        var movies = await _movieRepository.GetAllAsync();

        if (movies == null)
            return _serviceResponseHelper.SetError<List<ListResponseDto>>(new List<ListResponseDto>(), "No movies found");

        if (movies.Count == 0)
            return _serviceResponseHelper.SetSuccess(new List<ListResponseDto>());

        var result = new List<ListResponseDto>();

        foreach (var movie in movies.Where(i => !i.IsDeleted).ToList())
        {
            result.Add(new ListResponseDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre,
                Rating = movie.Rating,
                ImdbId = movie.ImdbId,
                DirectorId = movie.DirectorId
            });
        }

        return _serviceResponseHelper.SetSuccess(result);
    }

    /// <summary>
    /// Updates an existing movie record.
    /// </summary>
    /// <param name="requestDto">Data transfer object containing updated movie details.</param>
    /// <returns>A service response indicating success or failure.</returns>

    public async Task<ServiceResponse> UpdateMovieAsync(MovieRequestDto requestDto)
    {
        if (requestDto == null)
            return _serviceResponseHelper.SetError("Invalid request data");

        var entity = await _movieRepository.GetByIdAsync(requestDto.Id.ToString());

        if (entity == null)
            return _serviceResponseHelper.SetError("Movie not found");

        var director = await _directorRepository.GetByIdAsync(requestDto.DirectorId.ToString());
        if (director == null)
            return _serviceResponseHelper.SetError("Invalid DirectorId. Director not found.");

        entity.Title = requestDto.Title;
        entity.Description = requestDto.Description;
        entity.ReleaseDate = requestDto.ReleaseDate;
        entity.Genre = requestDto.Genre;
        entity.Rating = requestDto.Rating;
        entity.ImdbId = requestDto.ImdbId;
        entity.DirectorId = requestDto.DirectorId.ToString();

        await _movieRepository.UpdateAsync(entity.Id, entity);

        return _serviceResponseHelper.SetSuccess();
    }

    /// <summary>
    /// Deletes a movie record by its ID.
    /// </summary>
    /// <param name="id">The ID of the movie to delete.</param>
    /// <returns>A service response indicating success or failure.</returns>

    public async Task<ServiceResponse> DeleteMovieAsync(string id)
    {
        if (id == null)
            return _serviceResponseHelper.SetError("Invalid movie ID");

        var entity = await _movieRepository.GetByIdAsync(id.ToString());

        if (entity == null)
            return _serviceResponseHelper.SetError("Movie not found");

        entity.IsDeleted = true; // Soft delete

        await _movieRepository.UpdateAsync(entity.Id, entity);

        return _serviceResponseHelper.SetSuccess();
    }

    /// <summary>
    /// Creates a new director record.
    /// </summary>
    /// <param name="requestDto">Data transfer object containing director details.</param>
    /// <returns>A service response indicating success or failure.</returns>

    public async Task<ServiceResponse> CreateDirectorAsync(DirectorCreateRequestDto requestDto)
    {
        if (requestDto == null)
            return _serviceResponseHelper.SetError("Request cannot be null");

        var entity = new Director
        {
            Id = ObjectId.GenerateNewId().ToString(),
            FirstName = requestDto.FirstName,
            SecondName = requestDto.SecondName,
            BirthDate = requestDto.BirthDate,
            Bio = requestDto.Bio
        };

        await _directorRepository.AddAsync(entity);

        return _serviceResponseHelper.SetSuccess();
    }

    /// <summary>
    /// Deletes a director record by its ID.
    /// </summary>
    /// <param name="id">The ID of the director to delete.</param>
    /// <returns>A service response indicating success or failure.</returns>

    public async Task<ServiceResponse> DeleteDirectorAsync(string id)
    {
        if(id == null)
            return _serviceResponseHelper.SetError("Invalid director ID");

        var entity = await _directorRepository.GetByIdAsync(id.ToString());

        if (entity == null)
            return _serviceResponseHelper.SetError("Director not found");

        entity.IsDeleted = true; // Soft delete

        await _directorRepository.UpdateAsync(entity.Id, entity);

        return _serviceResponseHelper.SetSuccess();
    }
}