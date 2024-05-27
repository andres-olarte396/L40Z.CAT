using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Exceptions;
using Core.Domain.Entities;
using Core.Domain.Interfaces;

namespace Core.Application.Services
{
    /// <summary>
    /// User service.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userRepository">
        /// User repository.
        /// </param>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">
        /// User id.
        /// </param>
        /// <returns>
        /// User DTO.
        /// </returns>
        /// <exception cref="NotFoundException">
        /// Thrown when user not found.
        /// </exception>
        public UserDto GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                throw new NotFoundException($"User with id {id} not found.");

            return new UserDto { Id = user.Id, Name = user.Name, Email = user.Email };
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>
        /// List of user DTOs.
        /// </returns>
        public IEnumerable<UserDto> GetAllUsers()
        {
            return _userRepository.GetAll().Select(user => new UserDto { Id = user.Id, Name = user.Name, Email = user.Email });
        }

        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="userDto">
        /// User DTO.
        /// </param>
        /// <exception cref="Core.Exceptions.ValidationException">
        /// Thrown when user name is empty.
        /// </exception>
        public void CreateUser(UserDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Name))
                throw new Core.Exceptions.ValidationException(new Dictionary<string, string[]> { { "Name", new[] { "Name is required" } } });

            var user = new User { Name = userDto.Name, Email = userDto.Email };
            _userRepository.Add(user);
        }

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="userDto">
        /// User DTO.
        /// </param>
        /// <exception cref="NotFoundException">
        /// Thrown when user not found.
        /// </exception>
        public void UpdateUser(UserDto userDto)
        {
            var user = _userRepository.GetById(userDto.Id);
            if (user == null)
                throw new NotFoundException($"User with id {userDto.Id} not found.");

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            _userRepository.Update(user);
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="id">
        /// User id.
        /// </param>
        /// <exception cref="NotFoundException">
        /// Thrown when user not found.
        /// </exception>
        public void DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                throw new NotFoundException($"User with id {id} not found.");

            _userRepository.Delete(id);
        }
    }
}