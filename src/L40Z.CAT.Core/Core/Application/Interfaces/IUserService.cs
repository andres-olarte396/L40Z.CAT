using Core.Application.DTOs;

namespace Core.Application.Interfaces
{
    /// <summary>
    /// Interface for User Service
    /// </summary>
    public interface IUserService
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
        UserDto GetUserById(int id);

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>
        /// The list of users
        /// </returns>
        IEnumerable<UserDto> GetAllUsers();

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="userDto">
        /// The user to create
        /// </param>
        void CreateUser(UserDto userDto);

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="userDto">
        /// The user to update
        /// </param>
        void UpdateUser(UserDto userDto);

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">
        /// The id of the user to delete
        /// </param>
        void DeleteUser(int id);
    }
}
