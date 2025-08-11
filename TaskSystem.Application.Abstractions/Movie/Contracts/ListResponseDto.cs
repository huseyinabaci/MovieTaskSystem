using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSystem.Application.Abstractions.Movie.Contracts
{
    /// <summary>
    /// Represents the data transfer object for listing movie details.
    /// </summary>
    public class ListResponseDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the movie.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the movie.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the movie.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the release date of the movie.
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the genre of the movie.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the rating of the movie.
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// Gets or sets the IMDb identifier of the movie.
        /// </summary>
        public string ImdbId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the director associated with the movie.
        /// </summary>
        public string DirectorId { get; set; }
    }
}
