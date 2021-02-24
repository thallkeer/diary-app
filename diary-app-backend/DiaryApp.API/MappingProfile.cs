using AutoMapper;
using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.API.Models.Users;
using DiaryApp.Core.Entities.Users.Settings;
using DiaryApp.Services.DTO.Notifications;
using DiaryApp.Core.Entities.Notifications;
using DiaryApp.Services.DTO.Users;

namespace DiaryApp.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
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
            CreateMap<AppUser, UserWithSettingsDto>().ReverseMap();
            CreateMap<UserDto, UserWithPasswordDto>().ReverseMap();
            CreateMap<UserDto, UserAuthRequest>().ReverseMap();

            CreateMap<UserSettings, UserSettingsDto>().ReverseMap();
            CreateMap<PageAreaTransferSettings, PageAreaTransferSettingsDto>()
               .ReverseMap();
            CreateMap<NotificationsSettingsDto, NotificationSettings>()
               .ReverseMap();

            CreateMap<Notification, NotificationDto>().ReverseMap();
        }
    }
}
