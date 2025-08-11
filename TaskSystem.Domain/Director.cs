using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSystem.Domain.Base;

namespace TaskSystem.Domain
{
    /// <summary>
    /// Represents a director entity in the system.
    /// Includes details about the director and audit information for tracking changes.
    /// </summary>
    public class Director : IAudit
    {
        /// <summary>
        /// Gets or sets the unique identifier of the director.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the director.
        /// </summary>
        [BsonElement("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the second name of the director.
        /// </summary>
        [BsonElement("secondName")]
        public string SecondName { get; set; }

        /// <summary>
        /// Gets or sets the birth date of the director.
        /// </summary>
        [BsonElement("birthDate")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the biography of the director.
        /// </summary>
        [BsonElement("bio")]
        public string Bio { get; set; }

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