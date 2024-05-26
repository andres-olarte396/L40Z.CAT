using Core.Application.DTOs;

namespace Core.Application.Interfaces
{
    public interface IUserService
    {
        UserDto GetUserById(int id);
        IEnumerable<UserDto> GetAllUsers();
        void CreateUser(UserDto userDto);
        void UpdateUser(UserDto userDto);
        void DeleteUser(int id);
    }
}
