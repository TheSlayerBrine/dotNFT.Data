using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNFT.Services.Repositories.Users
{
    public interface IUserRepository
    {
        void CreateUser(UserDto userDto);
        IEnumerable<UserDto> SearchByUserName(string searchTerm);
        void DeleteUser(int userId);
        List<UserDto> GetAll();

        void UpdateUser(UserDto userDto);
        UserDto? GetUser(int userId);
        UserDto? GetUserByEmail(string email);
    }
}
