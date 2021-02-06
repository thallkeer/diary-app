using DiaryApp.Data.DataInterfaces;
using DiaryApp.Data.DataInterfaces.Lists;
using DiaryApp.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryApp.Data.Bootstrap
{
    public static class ServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICrudService<,>), typeof(CrudService<,>));
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
            //services.AddScoped<ICrudService<HabitDayDto, HabitDay>, CrudService<HabitDayDto, HabitDay>>();
            //services.AddScoped<ICrudService<PurchaseListDto, PurchaseList>, CrudService<PurchaseListDto, PurchaseList>>();
            //services.AddScoped<ICrudService<UserSettingsDto, UserSettings>, CrudService<UserSettingsDto, UserSettings>>();
        }
    }
}
