using DiaryApp.API.Filters;
using DiaryApp.Core;
using DiaryApp.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DiaryApp.API.Extensions.ConfigureServices
{
    public static class ServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ModelValidationAttribute>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<ICommonListService, CommonListService>();
            services.AddScoped<IMainPageService, MainPageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMonthPageService, MonthPageService>();
            services.AddScoped<IHabitTrackerService, HabitTrackerService>();
        }
    }
}
