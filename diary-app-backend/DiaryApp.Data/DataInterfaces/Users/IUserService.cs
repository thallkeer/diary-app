using DiaryApp.Models.DTO;
using System.Threading.Tasks;

namespace DiaryApp.Data.DataInterfaces
{
    public interface IUserService
    {
        UserDto Authenticate(string username, string password);
        Task RegisterAsync(UserDto user, string password);
        Task<bool> IsUserExists(int userId);
        Task<UserSettingsDto> GetSettingsAsync(int userId);      
    }
}
