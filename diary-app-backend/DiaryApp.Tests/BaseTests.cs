using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Tests.Helpers;

namespace DiaryApp.Tests
{
    public class BaseTests
    {
        protected ApplicationContext _dbContext;
        protected IConfigurationProvider _mapperProvider;
        protected IMapper _mapper;

        public BaseTests()
        {
            _dbContext = Configurations.GetDbContext();
            _mapperProvider = Configurations.GetMapperProvider();
            _mapper = Configurations.GetMapper();
        }
    }
}
