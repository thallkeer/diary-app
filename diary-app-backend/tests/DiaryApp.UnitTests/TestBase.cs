using AutoFixture;
using DiaryApp.UnitTests.Helpers;

namespace DiaryApp.UnitTests
{
    public class TestBase
    {
        internal static IFixture _fixture;

        static TestBase()
        {
            _fixture = Configurations.GetFixture();
        }
    }
}
