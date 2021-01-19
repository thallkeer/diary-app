using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Data.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Core.Models.PageAreas;
using DiaryApp.API.Models.Users;
using System.Collections.Generic;
using System.Linq;

namespace DiaryApp.API
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<TodoItem, TodoItemDto>().ReverseMap();
            CreateMap<TodoList, TodoListDto>().ReverseMap();

            CreateMap<EventItem, EventItemDto>().ReverseMap();
            CreateMap<EventList, EventListDto>().ReverseMap();

            CreateMap<ListItem, ListItemDto>().ReverseMap();           
            CreateMap<CommonList, CommonListDto>().ReverseMap();

            CreateMap<PurchaseList, ListWrapperDto>();
            CreateMap<PurchaseList, PurchaseListDto>().ReverseMap();
            CreateMap<IdeasList, ListWrapperDto>();
            CreateMap<IdeasList, IdeasListDto>().ReverseMap();
            CreateMap<DesiresList, ListWrapperDto>();
            CreateMap<DesiresList, DesireListDto>().ReverseMap();

            CreateMap<MainPage, PageDto>().ReverseMap();
            CreateMap<MonthPage, PageDto>().ReverseMap();
            CreateMap<MainPage, MainPageDto>().ReverseMap();
            CreateMap<MonthPage, MonthPageDto>().ReverseMap();            

            CreateMap<ImportantThingsArea, ImportantThingsAreaDto>().ReverseMap();                
            CreateMap<ImportantEventsArea, ImportantEventsAreaDto>().ReverseMap();

            CreateMap<PurchasesArea, PurchasesAreaDto>().ReverseMap();
            CreateMap<DesiresArea, DesiresAreaDto>().ReverseMap();
            CreateMap<IdeasArea, IdeasAreaDto>().ReverseMap();
            CreateMap<GoalsArea, GoalsAreaDto>().ReverseMap();
            CreateMap<HabitTracker, HabitTrackerDto>()
                .ForMember(ht => ht.Items, htdto => htdto.MapFrom(src => src.SelectedDays))
                .ForMember(ht => ht.AreaOwnerId, htdto => htdto.MapFrom(src => src.GoalsAreaID))
                .ReverseMap();
            CreateMap<HabitDay, HabitDayDto>().ReverseMap();

            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<AppUser, UserWithPasswordDto>().ReverseMap();
            CreateMap<UserDto, UserWithPasswordDto>().ReverseMap();
            CreateMap<UserDto, UserWithPasswordModel>().ReverseMap();
        }
    }
}
