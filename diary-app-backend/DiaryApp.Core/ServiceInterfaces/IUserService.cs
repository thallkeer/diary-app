using System.Threading.Tasks;

namespace DiaryApp.Core
{
    public interface IUserService : ICrudService<AppUser>
    {
        AppUser Authenticate(string username, string password);
        Task Create(AppUser user, string password);
    }
}
