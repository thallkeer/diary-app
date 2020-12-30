using AutoFixture;
using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Tests.Customizations;
using DiaryApp.Tests.Extensions;
using DiaryApp.Tests.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DiaryApp.Tests
{
    public class BaseTests
    {
        protected ApplicationContext _dbContext;
        protected IMapper _mapper => GetService<IMapper>();
        internal static IFixture _fixture;

        static BaseTests()
        {
            _fixture = Configurations.GetFixture();
        }

        public BaseTests()
        {
            _dbContext = Configurations.GetServiceProvider();
        }

        protected T GetService<T>()
        {
            T service = _dbContext.GetService<T>();
            return service;
        }
    }
}
