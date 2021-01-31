using AutoFixture;
using DiaryApp.Core.Models;
using Xunit;

namespace DiaryApp.Tests
{
    public class MainPageTests : BaseLogicTests
    {
        [Fact]
        public void CreateAreasShouldCreateAreas()
        {
            var mainPage = _fixture.Create<MainPage>();

            mainPage.CreateAreas();

            Assert.NotNull(mainPage.ImportantEventsArea);
            Assert.NotNull(mainPage.ImportantThingsArea);
            Assert.NotEqual(string.Empty, mainPage.ImportantThingsArea.Header);
            Assert.NotEqual(string.Empty, mainPage.ImportantEventsArea.Header);

            Assert.NotNull(mainPage.ImportantEventsArea.ImportantEvents);
            Assert.Equal(mainPage, mainPage.ImportantEventsArea.Page);
            Assert.Equal(mainPage.Id, mainPage.ImportantEventsArea.PageId);        

           
            Assert.NotNull(mainPage.ImportantThingsArea.ImportantThings);
            Assert.Equal(mainPage.Id, mainPage.ImportantThingsArea.PageId);
            Assert.Equal(mainPage, mainPage.ImportantThingsArea.Page);
            
        }
    }
}
