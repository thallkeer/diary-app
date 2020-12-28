using AutoFixture;
using DiaryApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryApp.Tests.Extensions
{
    internal static class FixtureExtensions
    {
        public static int CreateInt(this IFixture fixture, int min, int max)
        {
            return fixture.Create<int>() % (max - min + 1) + min;
        }

        public static int CreateMonth(this IFixture fixture) => fixture.CreateInt(1, 12);

        public static int CreateYear(this IFixture fixture) => fixture.CreateInt(2020, 9999);

        public static UserDto CreateUser(this IFixture fixture) => fixture.Build<UserDto>().Without(u => u.Id).Create();
    }
}
