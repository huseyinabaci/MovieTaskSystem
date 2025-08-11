using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskSystem.Infrastructure.Repository.Interfaces;

namespace TaskSystem.Infrastructure.Repository
{
    /// <summary>
    /// Provides a generic repository implementation for MongoDB operations.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The MongoDB data context.</param>
        public Repository(MongoDataContext context)
        {
            _collection = context.Database.GetCollection<T>(typeof(T).Name);
        }

        /// <summary>
        /// Retrieves all entities of type <typeparamref name="T"/> from the collection.
        /// </summary>
        /// <returns>A list of all entities.</returns>
        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Retrieves an entity by its unique identifier and ensures it is not marked as deleted.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity if found and not marked as deleted; otherwise, null.</returns>
        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.And(
                Builders<T>.Filter.Eq("_id", ObjectId.Parse(id)),
                Builders<T>.Filter.Eq("isDeleted", false)
            );
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        /// <summary>
        /// Updates an existing entity in the collection.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to update.</param>
        /// <param name="entity">The updated entity.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(string id, T entity)
        {
            await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", ObjectId.Parse(id)), entity);
        }

        /// <summary>
        /// Deletes an entity from the collection by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", ObjectId.Parse(id)));
        }
    }
}