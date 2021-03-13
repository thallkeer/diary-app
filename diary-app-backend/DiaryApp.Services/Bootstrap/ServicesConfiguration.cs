using DiaryApp.Services.DataInterfaces;
using DiaryApp.Services.DataServices.Notifications;
using DiaryApp.Services.DataServices;
using Microsoft.Extensions.DependencyInjection;
using DiaryApp.Services.DataInterfaces.ListItems;
using DiaryApp.Services.DataInterfaces.Users;
using DiaryApp.Services.DataServices.Pages;

namespace DiaryApp.Services.Bootstrap
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            return services.AddScoped(typeof(ICrudService<,>), typeof(CrudService<,>))
                           .AddScoped<IMainPageService, MainPageService>()
                           .AddScoped<IUserService, UserService>()
                           .AddScoped<IMonthPageService, MonthPageService>()
                           .AddScoped<IEventItemService, EventItemService>()
                           .AddScoped<ITodoItemService, TodoItemService>()
                           .AddScoped<IEventNotificationService, EventNotificationService>();
        }
    }
}
