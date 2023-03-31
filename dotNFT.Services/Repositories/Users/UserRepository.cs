using dotNFT.Data;
using dotNFT.Data.Entities;
using dotNFT.Services.Repositories.Artists;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNFT.Services.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateUser(UserDto userDto)
        {
            if (userDto == null) throw new ArgumentNullException(nameof(userDto));
            if (string.IsNullOrEmpty(userDto.FirstName)) throw new ArgumentException($"{nameof(userDto.FirstName)} cannot be null or empty.");
            if (string.IsNullOrEmpty(userDto.LastName)) throw new ArgumentException($"{nameof(userDto.LastName)} cannot be null or empty.");
            if (string.IsNullOrEmpty(userDto.UserName)) throw new ArgumentException($"{nameof(userDto.UserName)} cannot be null or empty.");
            if (string.IsNullOrEmpty(userDto.Email)) throw new ArgumentException($"{nameof(userDto.Email)} cannot be null or empty.");

            if (_context.Users.Any(u => u.Email == userDto.Email))
            {
                throw new Exception("Cannot insert a new User with the same Email.");
            }
            if (_context.Users.Any(u => u.UserName == userDto.UserName))
            {
                throw new Exception("Cannot insert a new User with the same Username.");
            }

            var userEntity = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                UserName = userDto.UserName,
                Email = userDto.Email,
                BirthDate = userDto.BirthDate,
                PasswordHash = userDto.PasswordHash,
                PasswordSalt = userDto.PasswordSalt,
                RegistrationDate = DateTime.UtcNow,
            };

            _context.Users.Add(userEntity);
            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));

            var userToDelete = _context.Users.Find(userId);

            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
            }
        }

        public List<UserDto> GetAll()
        {
            return _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    BirthDate = u.BirthDate,
                    PasswordHash = u.PasswordHash,
                })
                .ToList();
        }

        public UserDto? GetUser(int userId)
        {
            
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));

            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null) return null;

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return userDto;
        }

        public UserDto? GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));

            var user = _context.Users.SingleOrDefault(u => u.Email == email);

            if (user == null) return null;

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt
            };

            return userDto;
        }

        public IEnumerable<UserDto> SearchByUserName(string searchTerm)
        {
            return _context.Users
               .Where(u => u.FirstName.Contains(searchTerm) || u.LastName.Contains(searchTerm))
               .Select(u => new UserDto
               {
                   Id = u.Id,
                   FirstName = u.FirstName,
                   LastName = u.LastName,
                   Email = u.Email,
                   BirthDate = u.BirthDate,
                   PasswordHash = u.PasswordHash,
                   PasswordSalt = u.PasswordSalt,
               })
               .ToList();
        }

        public void UpdateUser(UserDto userDto)
        {
            if (userDto == null) throw new ArgumentNullException(nameof(userDto));
            if (string.IsNullOrEmpty(userDto.FirstName)) throw new ArgumentException($"{nameof(userDto.FirstName)} cannot be null or empty.");
            if (string.IsNullOrEmpty(userDto.LastName)) throw new ArgumentException($"{nameof(userDto.LastName)} cannot be null or empty.");
            if (string.IsNullOrEmpty(userDto.Email)) throw new ArgumentException($"{nameof(userDto.Email)} cannot be null or empty.");

            var userToUpdate = _context.Users.Find(userDto.Id);
            if (userToUpdate == null)
            {
                throw new Exception("The user has not been found");
            }

            userToUpdate.FirstName = userDto.FirstName;
            userToUpdate.LastName = userDto.LastName;
            userToUpdate.Email = userDto.Email;
        }
    }
}

