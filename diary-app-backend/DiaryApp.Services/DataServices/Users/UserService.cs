using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DTO;
using DiaryApp.Services.Exceptions;
using DiaryApp.Services.DataInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Services.Services
{
    public class UserService : CrudService<UserDto,AppUser>, IUserService
    {

        public UserService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public UserDto Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(username));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            var user = _dbSet.SingleOrDefault(x => x.Username == username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task RegisterAsync(UserDto userDto, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            if (await _dbSet.AnyAsync(x => x.Username == userDto.Username))
                throw new Exception($"Username '{userDto.Username}' is already taken");

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = _mapper.Map<AppUser>(userDto);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var userWithPassword = _mapper.Map<UserWithPasswordDto>(user);

            int userId = await base.CreateAsync(userWithPassword);
            //TODO: fix
            userDto.Id = userId;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
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

        private async Task<AppUser> GetUserByIdOrThrow(int userId)
        {
            var user = await _dbSet.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                throw new UserNotExistsException();
            return user;
        }
    }
}
