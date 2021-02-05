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

        //TODO: move to jwt token manager or smth
        /// <summary>
        /// Creates jwt access token for user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        string GenerateToken(UserDto user);        
    }
}
