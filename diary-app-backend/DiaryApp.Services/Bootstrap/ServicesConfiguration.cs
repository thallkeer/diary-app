using DiaryApp.Services.DataInterfaces;
using DiaryApp.Services.DataInterfaces.Lists;
using DiaryApp.Services.Services;
using Microsoft.Extensions.DependencyInjection;

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
                           .AddScoped<ITodoItemService, TodoItemService>();
            //services.AddScoped<ICrudService<HabitDayDto, HabitDay>, CrudService<HabitDayDto, HabitDay>>();
            //services.AddScoped<ICrudService<PurchaseListDto, PurchaseList>, CrudService<PurchaseListDto, PurchaseList>>();
            //services.AddScoped<ICrudService<UserSettingsDto, UserSettings>, CrudService<UserSettingsDto, UserSettings>>();
        }
    }
}
