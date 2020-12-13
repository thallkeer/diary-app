using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Data.Extensions;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Data.Services
{
    public class UserService : CrudServiceWithAutoSave<UserDto,AppUser>, IUserService
    {        
        public UserService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public UserDto Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = dbSet.SingleOrDefault(x => x.Username == username);

            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user.ToDto<AppUser, UserDto>(mapper);
        }

        public async Task CreateAsync(UserDto userDto, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (await dbSet.AnyAsync(x => x.Username == userDto.Username))
                throw new Exception($"Username '{userDto.Username}' is already taken");

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = userDto.ToEntity<AppUser, UserDto>(mapper);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var userWithPassword = user.ToDto<AppUser, UserWithPasswordDto>(mapper);

            await base.CreateAsync(userWithPassword);
        }

        public async override Task UpdateAsync(UserDto userToUpdate)
        {
            var user = await dbSet.FindAsync(userToUpdate.Id);

            if (user == null)
                throw new Exception("User not found");

            if (userToUpdate.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (dbSet.Any(x => x.Username == userToUpdate.Username))
                    throw new Exception($"Username '{userToUpdate.Username}' is already taken");
            }

            await base.UpdateAsync(userToUpdate);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(storedHash));
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", nameof(storedSalt));

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public async Task<bool> IsUserExists(int userId)
        {
            var res = await dbSet.AnyAsync(u => u.Id == userId);
            return res;
        }
    }
}
