using AutoFixture;
using DiaryApp.Core.Entities;
using DiaryApp.UnitTests.Helpers;
using DiaryApp.UnitTests.Pages;
using FluentAssertions;
using Xunit;

namespace DiaryApp.UnitTests
{
    public class MainPageTests : PageTests<MainPage>
    {
        [Fact]
        public void MainPage_CreateAreas_ShouldCreateAreas()
        {
            var mainPage = _fixture.Create<MainPage>();

            mainPage.CreateAreas();

            mainPage.AssertAreaParams(mainPage.ImportantEventsArea);
            mainPage.AssertAreaParams(mainPage.ImportantThingsArea);

            mainPage.ImportantEventsArea.ImportantEvents.Should().NotBeNull();
            mainPage.ImportantThingsArea.ImportantThings.Should().NotBeNull();
        }

        protected override MainPage CreatePage(int year, int month, AppUser user) => new(year, month, user);
    }
}
