using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskSystem.Application.Abstractions.Movie.Contracts;
using TaskSystem.Common.Helper;
using TaskSystem.Common.Model;
using TaskSystem.Domain;
using TaskSystem.Infrastructure.Repository.Interfaces;
using Xunit;

public class MovieServiceTests
{
    private readonly Mock<IRepository<Movie>> _movieRepositoryMock;
    private readonly Mock<IRepository<Director>> _directorRepositoryMock;
    private readonly Mock<IServiceResponseHelper> _serviceResponseHelperMock;
    private readonly MovieService _movieService;

    public MovieServiceTests()
    {
        _movieRepositoryMock = new Mock<IRepository<Movie>>();
        _directorRepositoryMock = new Mock<IRepository<Director>>();
        _serviceResponseHelperMock = new Mock<IServiceResponseHelper>();
        _movieService = new MovieService(
            _movieRepositoryMock.Object,
            _directorRepositoryMock.Object,
            _serviceResponseHelperMock.Object
        );
    }

    [Fact]
    public async Task CreateMovieAsync_ShouldReturnError_WhenRequestIsNull()
    {
        // Arrange
        _serviceResponseHelperMock
            .Setup(helper => helper.SetError(It.IsAny<string>()))
            .Returns(new ServiceResponse(false));

        // Act
        var result = await _movieService.CreateMovieAsync(null);

        // Assert
        Assert.False(result.IsSuccessful);
        _serviceResponseHelperMock.Verify(helper => helper.SetError("Request cannot be null"), Times.Once);
    }

    [Fact]
    public async Task CreateMovieAsync_ShouldReturnError_WhenDirectorNotFound()
    {
        // Arrange
        var requestDto = new MovieCreateRequestDto { DirectorId = "invalidId" };
        _directorRepositoryMock
            .Setup(repo => repo.GetByIdAsync(requestDto.DirectorId))
            .ReturnsAsync((Director)null);
        _serviceResponseHelperMock
            .Setup(helper => helper.SetError(It.IsAny<string>()))
            .Returns(new ServiceResponse(false));

        // Act
        var result = await _movieService.CreateMovieAsync(requestDto);

        // Assert
        Assert.False(result.IsSuccessful);
        _serviceResponseHelperMock.Verify(helper => helper.SetError("Invalid DirectorId. Director not found."), Times.Once);
    }

    [Fact]
    public async Task CreateMovieAsync_ShouldReturnSuccess_WhenMovieIsCreated()
    {
        // Arrange
        var requestDto = new MovieCreateRequestDto
        {
            Title = "Test Movie",
            DirectorId = "validId"
        };
        var director = new Director { Id = "validId" };
        _directorRepositoryMock
            .Setup(repo => repo.GetByIdAsync(requestDto.DirectorId))
            .ReturnsAsync(director);
        _serviceResponseHelperMock
            .Setup(helper => helper.SetSuccess())
            .Returns(new ServiceResponse(true));

        // Act
        var result = await _movieService.CreateMovieAsync(requestDto);

        // Assert
        Assert.True(result.IsSuccessful);
        _movieRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Movie>()), Times.Once);
        _serviceResponseHelperMock.Verify(helper => helper.SetSuccess(), Times.Once);
    }

    [Fact]
    public async Task GetAllMoviesAsync_ShouldReturnError_WhenNoMoviesFound()
    {
        // Arrange
        _movieRepositoryMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync((List<Movie>)null);
        _serviceResponseHelperMock
            .Setup(helper => helper.SetError(It.IsAny<List<ListResponseDto>>(), It.IsAny<string>()))
            .Returns(new ServiceResponse<List<ListResponseDto>>(new List<ListResponseDto>(), false));

        // Act
        var result = await _movieService.GetAllMoviesAsync();

        // Assert
        Assert.False(result.IsSuccessful);
        _serviceResponseHelperMock.Verify(helper => helper.SetError(It.IsAny<List<ListResponseDto>>(), "No movies found"), Times.Once);
    }

    [Fact]
    public async Task UpdateMovieAsync_ShouldReturnError_WhenMovieNotFound()
    {
        // Arrange
        var requestDto = new MovieRequestDto { Id = "invalidId" };
        _movieRepositoryMock
            .Setup(repo => repo.GetByIdAsync(requestDto.Id))
            .ReturnsAsync((Movie)null);
        _serviceResponseHelperMock
            .Setup(helper => helper.SetError(It.IsAny<string>()))
            .Returns(new ServiceResponse(false));

        // Act
        var result = await _movieService.UpdateMovieAsync(requestDto);

        // Assert
        Assert.False(result.IsSuccessful);
        _serviceResponseHelperMock.Verify(helper => helper.SetError("Movie not found"), Times.Once);
    }

    [Fact]
    public async Task DeleteMovieAsync_ShouldReturnError_WhenMovieNotFound()
    {
        // Arrange
        var movieId = "invalidId";
        _movieRepositoryMock
            .Setup(repo => repo.GetByIdAsync(movieId))
            .ReturnsAsync((Movie)null);
        _serviceResponseHelperMock
            .Setup(helper => helper.SetError(It.IsAny<string>()))
            .Returns(new ServiceResponse(false));

        // Act
        var result = await _movieService.DeleteMovieAsync(movieId);

        // Assert
        Assert.False(result.IsSuccessful);
        _serviceResponseHelperMock.Verify(helper => helper.SetError("Movie not found"), Times.Once);
    }

    [Fact]
    public async Task CreateDirectorAsync_ShouldReturnError_WhenRequestIsNull()
    {
        // Arrange
        _serviceResponseHelperMock
            .Setup(helper => helper.SetError(It.IsAny<string>()))
            .Returns(new ServiceResponse(false));

        // Act
        var result = await _movieService.CreateDirectorAsync(null);

        // Assert
        Assert.False(result.IsSuccessful);
        _serviceResponseHelperMock.Verify(helper => helper.SetError("Request cannot be null"), Times.Once);
    }

    [Fact]
    public async Task DeleteDirectorAsync_ShouldReturnError_WhenDirectorNotFound()
    {
        // Arrange
        var directorId = "invalidId";
        _directorRepositoryMock
            .Setup(repo => repo.GetByIdAsync(directorId))
            .ReturnsAsync((Director)null);
        _serviceResponseHelperMock
            .Setup(helper => helper.SetError(It.IsAny<string>()))
            .Returns(new ServiceResponse(false));

        // Act
        var result = await _movieService.DeleteDirectorAsync(directorId);

        // Assert
        Assert.False(result.IsSuccessful);
        _serviceResponseHelperMock.Verify(helper => helper.SetError("Director not found"), Times.Once);
    }
}