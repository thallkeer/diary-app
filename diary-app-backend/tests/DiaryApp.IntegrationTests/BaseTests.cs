using AutoMapper;
using DiaryApp.Core;
using DiaryApp.IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DiaryApp.IntegrationTests
{
    public class BaseTests
    {
        protected ApplicationContext _dbContext;
        protected IMapper _mapper => GetService<IMapper>();

        public BaseTests()
        {
            _dbContext = Configurations.GetServiceProvider();
        }

        protected T GetService<T>() where T : class
        {
            T service = _dbContext.GetService<T>();
            return service;
        }
    }
}
