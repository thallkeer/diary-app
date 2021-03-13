using DiaryApp.Core.Entities;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Core.Entities.Pages;
using FluentAssertions;

namespace DiaryApp.UnitTests.Helpers
{
    public static class Assertions
    {
        public static void AssertAreaParams<TPage, TArea>(this TPage page, TArea area)
            where TPage : PageBase, new()
            where TArea : PageAreaBase<TPage>
        {
            area.Should().NotBeNull();
            area.Page.Should().BeSameAs(page);
            area.Header.Should().NotBeNullOrEmpty();
        }
    }
}
