using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskSystem.Domain.Base;

namespace TaskSystem.Domain
{
    /// <summary>
    /// Represents a movie entity in the system.
    /// Includes details about the movie and audit information for tracking changes.
    /// </summary>
    public class Movie : IAudit
    {
        /// <summary>
        /// Gets or sets the unique identifier of the movie.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the movie.
        /// </summary>
        [BsonElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the movie.
        /// </summary>
        [BsonElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the release date of the movie.
        /// </summary>
        [BsonElement("releaseDate")]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the genre of the movie.
        /// </summary>
        [BsonElement("genre")]
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the rating of the movie.
        /// </summary>
        [BsonElement("rating")]
        public double Rating { get; set; }

        /// <summary>
        /// Gets or sets the IMDb identifier of the movie.
        /// </summary>
        [BsonElement("imdbId")]
        public string ImdbId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the director associated with the movie.
        /// </summary>
        [BsonElement("directorId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DirectorId { get; set; }

        #region Audit

        /// <summary>
        /// Gets or sets the user who inserted the record.
        /// </summary>
        [BsonElement("insertedUser")]
        public string InsertedUser { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was inserted.
        /// </summary>
        [BsonElement("insertedDate")]
        public DateTime? InsertedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who last updated the record.
        /// </summary>
        [BsonElement("updatedUser")]
        public string UpdatedUser { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was last updated.
        /// </summary>
        [BsonElement("updatedDate")]
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who deleted the record.
        /// </summary>
        [BsonElement("deletedUser")]
        public string DeletedUser { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was deleted.
        /// </summary>
        [BsonElement("deletedDate")]
        public DateTime? DeletedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the record is deleted.
        /// </summary>
        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; }

        #endregion
    }
}