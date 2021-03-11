using System;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DTO;
using DiaryApp.Services.Exceptions;
using DiaryApp.Services.DataInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Services.DataServices
{
    public class UserService : CrudService<UserDto,AppUser>, IUserService
    {

        public UserService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<UserDto> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(username));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            var user = await _dbSet.SingleOrDefaultAsync(x => x.Username == username);

            if (user == null)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new { UserName = "Username is incorrect!" });

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new { Password = "Password is incorrect!" });

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> RegisterAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            if (await _dbSet.AnyAsync(x => x.Username == username))
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new { UserName = $"Username '{username}' is already taken!" });

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new AppUser
            {
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _dbSet.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }       

        public async Task<bool> IsUserExists(int userId)
        {
            var res = await _dbSet.AnyAsync(u => u.Id == userId);
            return res;
        }

        public async Task<UserSettingsDto> GetSettingsAsync(int userId)
        {
            AppUser user = await GetUserByIdOrThrow(userId);
            return _mapper.Map<UserSettingsDto>(user.Settings);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(storedHash));
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", nameof(storedSalt));

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        private async Task<AppUser> GetUserByIdOrThrow(int userId)
        {
            var user = await _dbSet.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                throw new UserNotExistsException();
            return user;
        }
    }
}
