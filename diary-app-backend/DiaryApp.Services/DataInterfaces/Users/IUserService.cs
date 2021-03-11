using DiaryApp.Core.Entities;
using DiaryApp.Services.DTO;
using System.Threading.Tasks;

namespace DiaryApp.Services.DataInterfaces
{
    public interface IUserService : ICrudService<UserDto, AppUser>
    {
        Task<UserDto> AuthenticateAsync(string username, string password);
        Task<UserDto> RegisterAsync(string username, string password);
        Task<bool> IsUserExists(int userId);
        Task<UserSettingsDto> GetSettingsAsync(int userId);
    }
}
