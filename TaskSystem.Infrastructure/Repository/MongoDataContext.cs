using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MongoDB.Driver;
using TaskSystem.Domain;
using TaskSystem.Infrastructure.Extensions;
using TaskSystem.Infrastructure.Repository.Interfaces;

namespace TaskSystem.Infrastructure.Repository
{
    /// <summary>
    /// Represents the MongoDB data context for accessing the database.
    /// </summary>
    public class MongoDataContext
    {
        /// <summary>
        /// Gets the MongoDB database instance.
        /// </summary>
        public IMongoDatabase Database { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDataContext"/> class.
        /// </summary>
        /// <param name="client">The MongoDB client used to connect to the database.</param>
        /// <param name="databaseName">The name of the database to connect to.</param>
        public MongoDataContext(IMongoClient client, string databaseName)
        {
            Database = client.GetDatabase(databaseName);
        }
    }
}