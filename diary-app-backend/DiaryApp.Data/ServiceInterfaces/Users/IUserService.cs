using DiaryApp.Core;
using DiaryApp.Core.DTO;
using System.Threading.Tasks;

namespace DiaryApp.Data.ServiceInterfaces
{
    public interface IUserService : ICrudService<UserDto, AppUser>
    {
        UserDto Authenticate(string username, string password);
        Task RegisterAsync(UserDto user, string password);
        Task<bool> IsUserExists(int userId);
        string GenerateToken(UserDto user);
    }
}
