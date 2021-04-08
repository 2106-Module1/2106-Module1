using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Owner : Mod 1 Team 9
 * Used by all teams in Mod 1
 */
namespace HotelManagementSystem.DataSource
{
    /// <summary>
    /// A generic interface for performing CRUD operations on a database table
    /// </summary>
    /// <typeparam name="T">The row data represented by an entity class</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves all the data in a table
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Retrieve a specific row by id
        /// </summary>
        /// <param name="id">The id of the row to retrieve</param>
        /// <returns>The data represented by an entity, if it exists</returns>
        T GetById(int id);

        /// <summary>
        /// Inserts a new row into the table
        /// </summary>
        /// <param name="entity">The data to insert, represented by an entity</param>
        void Insert(T entity);

        /// <summary>
        /// Updates an existing row in the table
        /// </summary>
        /// <param name="entity">The data to update, represented by an entity</param>
        void Update(T entity);

        /// <summary>
        /// Deletes an existing row in the table
        /// </summary>
        /// <param name="entity">The data to delete, represented by an entity</param>
        void Delete(T entity);
    }
}
