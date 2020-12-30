using AutoFixture;
using DiaryApp.Core.Models.PageAreas;
using DiaryApp.Tests.Extensions;

namespace DiaryApp.Tests.Customizations
{
    public class PageAreaCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.CustomizePageArea<PurchasesArea>();
            fixture.CustomizePageArea<DesiresArea>();
            fixture.CustomizePageArea<IdeasArea>();
            fixture.CustomizePageArea<GoalsArea>();
            fixture.Customize(new DiaryListsCustomization());
            fixture.Customize(new DiaryListWrappersCustomization());
        }
    }
}
