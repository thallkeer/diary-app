using System.Threading.Tasks;
using DiaryApp.Core.Entities.Users;
using DiaryApp.Services.DTO;
using JetBrains.Annotations;

namespace DiaryApp.Services.DataInterfaces.Users
{
    public interface IUserService : ICrudService<UserDto, AppUser>
    {
        Task<UserDto> AuthenticateAsync(string username, string password);
        Task<UserDto> RegisterAsync(string username, string password);
        Task<bool> IsUserExists(int userId);
        [ItemCanBeNull] Task<UserSettingsDto> GetSettingsAsync(int userId);
    }
}
