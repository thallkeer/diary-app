using AutoMapper;
using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.API.Models.Users;
using DiaryApp.Core.Entities.DiaryLists;
using DiaryApp.Core.Entities.ListWrappers;
using DiaryApp.Core.Entities.Users.Settings;
using DiaryApp.Services.DTO.Notifications;
using DiaryApp.Core.Entities.Notifications;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Core.Entities.Pages;
using DiaryApp.Core.Entities.Users;
using DiaryApp.Services.DTO.Users;
using DiaryApp.Services.DTO.Lists;
using DiaryApp.Services.DTO.PageAreas;

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

            CreateMap<PurchaseList, TodoListDto>()
                .ForMember(dto => dto.Id,
                    dto => dto.MapFrom(src => src.ListID))
                .ForMember(dto => dto.Title,
                    dto => dto.MapFrom(src => src.List.Title));
            CreateMap<IdeasList, CommonListDto>()
                .ForMember(dto => dto.Id,
                    dto => dto.MapFrom(src => src.ListID))
                .ForMember(dto => dto.Title,
                    dto => dto.MapFrom(src => src.List.Title));
            CreateMap<DesiresList, CommonListDto>()
                .ForMember(dto => dto.Id,
                    dto => dto.MapFrom(src => src.ListID))
                .ForMember(dto => dto.Title,
                    dto => dto.MapFrom(src => src.List.Title));
            
            CreateMap<HabitTracker, HabitTrackerDto>().ReverseMap();

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

            CreateMap<Notification, NotificationDto>()
                .ForMember(n => n.UserTelegramId, dto => dto.MapFrom(src => src.User.TelegramId));
            //not using reverse map because mapping userTelegramId creates new empty user
            CreateMap<NotificationDto, Notification>();
        }
    }
}
