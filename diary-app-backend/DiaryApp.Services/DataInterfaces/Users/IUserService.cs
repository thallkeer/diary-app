using DiaryApp.Core.Entities;
using DiaryApp.Models.DTO;
using System.Threading.Tasks;

namespace DiaryApp.Services.DataInterfaces
{
    public interface IUserService : IGetable<AppUser>
    {
        UserDto Authenticate(string username, string password);
        Task RegisterAsync(UserDto user, string password);
        Task<bool> IsUserExists(int userId);
        Task<UserSettingsDto> GetSettingsAsync(int userId);      
    }
}
