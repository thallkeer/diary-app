using DiaryApp.Core;

namespace DiaryApp.Data.Services
{
    public class CommonListService : ListService<CommonList, ListItem>, ICommonListService
    {
        public CommonListService(ApplicationContext context) : base(context)
        {
        }
    }
}
