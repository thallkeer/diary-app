﻿using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Tests.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DiaryApp.Tests
{
    public class BaseTests : BaseLogicTests
    {
        protected ApplicationContext _dbContext;
        protected IMapper _mapper => GetService<IMapper>();

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