using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using DiaryApp.Core.Models;
using DiaryApp.Core.Models.PageAreas;

namespace DiaryApp.API
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<TodoItem, TodoModel>();
            CreateMap<TodoModel, TodoItem>();
            CreateMap<TodoList, TodoListModel>();
            CreateMap<TodoListModel, TodoList>();

            CreateMap<EventItem, EventModel>();
            CreateMap<EventModel, EventItem>();
            CreateMap<EventList, EventListModel>();
            CreateMap<EventListModel, EventList>();

            CreateMap<ListItem, ListItemModel>();
            CreateMap<ListItemModel, ListItem>();           
            CreateMap<CommonList, CommonListModel>();           
            CreateMap<CommonListModel, CommonList>();

            CreateMap<MainPage, MainPageModel>();

            CreateMap<MonthPage, MonthPageModel>();
            CreateMap<PurchasesArea, PurchasesAreaModel>();
            CreateMap<DesiresArea, DesiresAreaModel>();
            CreateMap<IdeasArea, IdeasAreaModel>();
            CreateMap<GoalsArea, GoalsAreaModel>();
            CreateMap<HabitsTracker, HabitsTrackerModel>();
            CreateMap<HabitsTrackerModel, HabitsTracker>();

            CreateMap<AppUser, UserModel>();
            CreateMap<UserModel, AppUser>();            
        }
    }
}
