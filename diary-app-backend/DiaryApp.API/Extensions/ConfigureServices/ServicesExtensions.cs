using DiaryApp.API.Filters;
using DiaryApp.Data.ServiceInterfaces;
using DiaryApp.Data.ServiceInterfaces.Lists;
using DiaryApp.Data.Services;
using DiaryApp.Data.Services.Lists;
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
            services.AddScoped<IEventItemService, EventItemService>();
            services.AddScoped<ITodoItemService, TodoItemService>();
            services.AddScoped<ICommonListItemService, CommonListItemService>();
        }
    }
}
