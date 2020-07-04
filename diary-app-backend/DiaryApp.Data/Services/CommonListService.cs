using DiaryApp.Core;
using DiaryApp.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiaryApp.Data.Services
{
    public class CommonListService : ListService<CommonList, ListItem>, ICommonListService
    {
        public CommonListService(ApplicationContext context) : base(context)
        {
        }
    }
}
