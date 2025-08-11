using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskSystem.Application.Abstractions.Movie.Contracts;
using TaskSystem.Common.Model;
using TaskSystem.WebApi.Controllers;
using TaskSystem.WebApi.Model.Movie;
using Xunit;

namespace TaskSystem.WebApi.Tests.Controllers
{
    /// <summary>
    /// Contains unit tests for the <see cref="MovieController"/> class.
    /// </summary>
    public class MovieControllerTests
    {
        private readonly Mock<IMovieService> _movieServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly MovieController _controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieControllerTests"/> class.
        /// Sets up mocks and the controller instance for testing.
        /// </summary>
        public MovieControllerTests()
        {
            _movieServiceMock = new Mock<IMovieService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new MovieController(_movieServiceMock.Object, _mapperMock.Object);
        }

        /// <summary>
        /// Tests that <see cref="MovieController.CreateMovie"/> returns an Ok result when the service response is successful.
        /// </summary>
        [Fact]
        public async Task CreateMovie_ReturnsOkResult_WhenServiceResponseIsSuccessful()
        {
            // Arrange
            var request = new MovieCreateRequest { Title = "Test Movie" };
            var mappedRequest = new MovieCreateRequestDto { Title = "Test Movie" };
            var serviceResponse = new ServiceResponse(true);

            _mapperMock.Setup(m => m.Map<MovieCreateRequestDto>(request)).Returns(mappedRequest);
            _movieServiceMock.Setup(s => s.CreateMovieAsync(mappedRequest)).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.CreateMovie(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(serviceResponse, okResult.Value);
        }

        /// <summary>
        /// Tests that <see cref="MovieController.CreateMovie"/> returns a BadRequest result when the service response is not successful.
        /// </summary>
        [Fact]
        public async Task CreateMovie_ReturnsBadRequest_WhenServiceResponseIsNotSuccessful()
        {
            // Arrange
            var request = new MovieCreateRequest { Title = "Test Movie" };
            var mappedRequest = new MovieCreateRequestDto { Title = "Test Movie" };
            var serviceResponse = new ServiceResponse(false);

            _mapperMock.Setup(m => m.Map<MovieCreateRequestDto>(request)).Returns(mappedRequest);
            _movieServiceMock.Setup(s => s.CreateMovieAsync(mappedRequest)).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.CreateMovie(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests that <see cref="MovieController.GetAllMovies"/> returns an Ok result when the service response is successful.
        /// </summary>
        [Fact]
        public async Task GetAllMovies_ReturnsOkResult_WhenServiceResponseIsSuccessful()
        {
            // Arrange
            var serviceResponse = new ServiceResponse<List<ListResponseDto>>(new List<ListResponseDto>(), true);

            _movieServiceMock.Setup(s => s.GetAllMoviesAsync()).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GetAllMovies();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(serviceResponse, okResult.Value);
        }

        /// <summary>
        /// Tests that <see cref="MovieController.GetAllMovies"/> returns a BadRequest result when the service response is not successful.
        /// </summary>
        [Fact]
        public async Task GetAllMovies_ReturnsBadRequest_WhenServiceResponseIsNotSuccessful()
        {
            // Arrange
            var serviceResponse = new ServiceResponse<List<ListResponseDto>>(null, false);

            _movieServiceMock.Setup(s => s.GetAllMoviesAsync()).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GetAllMovies();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests that <see cref="MovieController.UpdateMovie"/> returns an Ok result when the service response is successful.
        /// </summary>
        [Fact]
        public async Task UpdateMovie_ReturnsOkResult_WhenServiceResponseIsSuccessful()
        {
            // Arrange
            var request = new MovieRequest { Id = "1", Title = "Updated Movie" };
            var mappedRequest = new MovieRequestDto { Id = "1", Title = "Updated Movie" };
            var serviceResponse = new ServiceResponse(true);

            _mapperMock.Setup(m => m.Map<MovieRequestDto>(request)).Returns(mappedRequest);
            _movieServiceMock.Setup(s => s.UpdateMovieAsync(mappedRequest)).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.UpdateMovie(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(serviceResponse, okResult.Value);
        }

        /// <summary>
        /// Tests that <see cref="MovieController.UpdateMovie"/> returns a BadRequest result when the service response is not successful.
        /// </summary>
        [Fact]
        public async Task UpdateMovie_ReturnsBadRequest_WhenServiceResponseIsNotSuccessful()
        {
            // Arrange
            var request = new MovieRequest { Id = "1", Title = "Updated Movie" };
            var mappedRequest = new MovieRequestDto { Id = "1", Title = "Updated Movie" };
            var serviceResponse = new ServiceResponse(false);

            _mapperMock.Setup(m => m.Map<MovieRequestDto>(request)).Returns(mappedRequest);
            _movieServiceMock.Setup(s => s.UpdateMovieAsync(mappedRequest)).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.UpdateMovie(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests that <see cref="MovieController.DeleteMovie"/> returns an Ok result when the service response is successful.
        /// </summary>
        [Fact]
        public async Task DeleteMovie_ReturnsOkResult_WhenServiceResponseIsSuccessful()
        {
            // Arrange
            var id = "1";
            var serviceResponse = new ServiceResponse(true);

            _movieServiceMock.Setup(s => s.DeleteMovieAsync(id)).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.DeleteMovie(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(serviceResponse, okResult.Value);
        }

        /// <summary>
        /// Tests that <see cref="MovieController.DeleteMovie"/> returns a BadRequest result when the service response is not successful.
        /// </summary>
        [Fact]
        public async Task DeleteMovie_ReturnsBadRequest_WhenServiceResponseIsNotSuccessful()
        {
            // Arrange
            var id = "1";
            var serviceResponse = new ServiceResponse(false);

            _movieServiceMock.Setup(s => s.DeleteMovieAsync(id)).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.DeleteMovie(id);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests that <see cref="MovieController.CreateDirector"/> returns an Ok result when the service response is successful.
        /// </summary>
        [Fact]
        public async Task CreateDirector_ReturnsOkResult_WhenServiceResponseIsSuccessful()
        {
            // Arrange
            var request = new DirectorCreateRequest { FirstName = "John", SecondName = "Doe" };
            var mappedRequest = new DirectorCreateRequestDto { FirstName = "John", SecondName = "Doe" };
            var serviceResponse = new ServiceResponse(true);

            _mapperMock.Setup(m => m.Map<DirectorCreateRequestDto>(request)).Returns(mappedRequest);
            _movieServiceMock.Setup(s => s.CreateDirectorAsync(mappedRequest)).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.CreateDirector(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(serviceResponse, okResult.Value);
        }

        /// <summary>
        /// Tests that <see cref="MovieController.CreateDirector"/> returns a BadRequest result when the service response is not successful.
        /// </summary>
        [Fact]
        public async Task CreateDirector_ReturnsBadRequest_WhenServiceResponseIsNotSuccessful()
        {
            // Arrange
            var request = new DirectorCreateRequest { FirstName = "John", SecondName = "Doe" };
            var mappedRequest = new DirectorCreateRequestDto { FirstName = "John", SecondName = "Doe" };
            var serviceResponse = new ServiceResponse(false);

            _mapperMock.Setup(m => m.Map<DirectorCreateRequestDto>(request)).Returns(mappedRequest);
            _movieServiceMock.Setup(s => s.CreateDirectorAsync(mappedRequest)).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.CreateDirector(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests that <see cref="MovieController.DeleteDirector"/> returns an Ok result when the service response is successful.
        /// </summary>
        [Fact]
        public async Task DeleteDirector_ReturnsOkResult_WhenServiceResponseIsSuccessful()
        {
            // Arrange
            var id = "1";
            var serviceResponse = new ServiceResponse(true);

            _movieServiceMock.Setup(s => s.DeleteDirectorAsync(id)).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.DeleteDirector(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(serviceResponse, okResult.Value);
        }

        /// <summary>
        /// Tests that <see cref="MovieController.DeleteDirector"/> returns a BadRequest result when the service response is not successful.
        /// </summary>
        [Fact]
        public async Task DeleteDirector_ReturnsBadRequest_WhenServiceResponseIsNotSuccessful()
        {
            // Arrange
            var id = "1";
            var serviceResponse = new ServiceResponse(false);

            _movieServiceMock.Setup(s => s.DeleteDirectorAsync(id)).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.DeleteDirector(id);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}