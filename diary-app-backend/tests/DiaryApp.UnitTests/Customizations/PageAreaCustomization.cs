using AutoFixture;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.UnitTests.Extensions;

namespace DiaryApp.UnitTests.Customizations
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
