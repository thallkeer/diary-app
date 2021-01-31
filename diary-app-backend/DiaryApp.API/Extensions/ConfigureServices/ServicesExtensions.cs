using DiaryApp.API.Filters;
using DiaryApp.Core.Models;
using DiaryApp.Data.DTO;
using DiaryApp.Data.ServiceInterfaces;
using DiaryApp.Data.ServiceInterfaces.Lists;
using DiaryApp.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DiaryApp.API.Extensions.ConfigureServices
{
    public static class ServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ModelValidationAttribute>();

            services.AddScoped<IEventListService, EventListService>();
            services.AddScoped<ITodoListService, TodoListService>();
            services.AddScoped<ICommonListService, CommonListService>();
            services.AddScoped<IMainPageService, MainPageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMonthPageService, MonthPageService>();
            services.AddScoped<IHabitTrackerService, HabitTrackerService>();
            services.AddScoped<ICrudService<HabitDayDto, HabitDay>, CrudService<HabitDayDto, HabitDay>>();
            services.AddScoped<IEventItemService, EventItemService>();
            services.AddScoped<ITodoItemService, TodoItemService>();
            services.AddScoped<ICommonListItemService, CommonListItemService>();
            services.AddScoped<ICrudService<PurchaseListDto, PurchaseList>, CrudService<PurchaseListDto, PurchaseList>>();
            services.AddScoped<ICrudService<UserSettingsDto, UserSettings>, CrudService<UserSettingsDto, UserSettings>>();
        }
    }
}
