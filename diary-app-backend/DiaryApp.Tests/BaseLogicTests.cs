using AutoFixture;
using DiaryApp.Tests.Helpers;

namespace DiaryApp.Tests
{
    public class BaseLogicTests
    {
        internal static IFixture _fixture;

        static BaseLogicTests()
        {
            _fixture = Configurations.GetFixture();
        }
    }
}
