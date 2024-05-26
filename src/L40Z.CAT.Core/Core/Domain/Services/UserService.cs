using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Exceptions;
using Core.Domain.Entities;
using Core.Domain.Interfaces;

namespace Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDto GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                throw new NotFoundException($"User with id {id} not found.");

            return new UserDto { Id = user.Id, Name = user.Name, Email = user.Email };
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            return _userRepository.GetAll().Select(user => new UserDto { Id = user.Id, Name = user.Name, Email = user.Email });
        }

        public void CreateUser(UserDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Name))
                throw new Core.Exceptions.ValidationException(new Dictionary<string, string[]> { { "Name", new[] { "Name is required" } } });

            var user = new User { Name = userDto.Name, Email = userDto.Email };
            _userRepository.Add(user);
        }

        public void UpdateUser(UserDto userDto)
        {
            var user = _userRepository.GetById(userDto.Id);
            if (user == null)
                throw new NotFoundException($"User with id {userDto.Id} not found.");

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                throw new NotFoundException($"User with id {id} not found.");

            _userRepository.Delete(id);
        }
    }
}