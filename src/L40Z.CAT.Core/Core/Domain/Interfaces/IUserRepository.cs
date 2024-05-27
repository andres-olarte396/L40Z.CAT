using Core.Domain.Entities;

namespace Core.Domain.Interfaces
{
    /// <summary>
    /// Interface for User Repository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="id">
        /// The id of the user
        /// </param>
        /// <returns>
        /// The user
        /// </returns>
        User GetById(int id);

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>
        /// The list of users
        /// </returns>
        IEnumerable<User> GetAll();

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="user">
        /// The user to add
        /// </param>
        void Add(User user);

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="user">
        /// The user to update
        /// </param>
        void Update(User user);

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">
        /// The id of the user to delete
        /// </param>
        void Delete(int id);
    }
}
