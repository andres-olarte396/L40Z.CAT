using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
            if (user == null) return null;

            return new UserDto { Id = user.Id, Name = user.Name, Email = user.Email };
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            return _userRepository.GetAll().Select(user => new UserDto { Id = user.Id, Name = user.Name, Email = user.Email });
        }

        public void CreateUser(UserDto userDto)
        {
            var user = new User { Name = userDto.Name, Email = userDto.Email };
            _userRepository.Add(user);
        }

        public void UpdateUser(UserDto userDto)
        {
            var user = _userRepository.GetById(userDto.Id);
            if (user == null) return;

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }
    }
}
