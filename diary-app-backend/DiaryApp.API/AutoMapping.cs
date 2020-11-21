using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using DiaryApp.Core.Models;
using DiaryApp.Core.Models.PageAreas;
using DiaryApp.Core.Models.Pages;
using PageModel = DiaryApp.API.Models.PageModel;

namespace DiaryApp.API
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<TodoItem, TodoModel>().ReverseMap();
            CreateMap<TodoList, TodoListModel>().ReverseMap();

            CreateMap<EventItem, EventModel>().ReverseMap();
            CreateMap<EventList, EventListModel>().ReverseMap();

            CreateMap<ListItem, ListItemModel>().ReverseMap();           
            CreateMap<CommonList, CommonListModel>().ReverseMap(); 

            CreateMap<MainPage, PageModel>().ReverseMap();
            CreateMap<MonthPage, PageModel>().ReverseMap();
            CreateMap<WeekPage, PageModel>().ReverseMap();

            CreateMap<ImportantThingsArea, ImportantThingsAreaModel>().ReverseMap();
            CreateMap<ImportantEventsArea, ImportantEventsAreaModel>().ReverseMap();

            CreateMap<PurchasesArea, PurchasesAreaModel>().ReverseMap();
            CreateMap<DesiresArea, DesiresAreaModel>().ReverseMap();
            CreateMap<IdeasArea, IdeasAreaModel>().ReverseMap();
            CreateMap<GoalsArea, GoalsAreaModel>().ReverseMap();
            CreateMap<HabitsTracker, HabitsTrackerModel>().ReverseMap();

            CreateMap<AppUser, UserModel>().ReverseMap();           
        }
    }
}
