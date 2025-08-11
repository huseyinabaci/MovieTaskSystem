using System;

namespace TaskSystem.Infrastructure.Extensions
{
    /// <summary>
    /// Represents the settings required to configure a connection to a MongoDB database.
    /// </summary>
    public class MongoDbSettings
    {
        /// <summary>
        /// Gets or sets the connection string used to connect to the MongoDB server.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the name of the MongoDB database.
        /// </summary>
        public string DatabaseName { get; set; }
    }
}