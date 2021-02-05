using AutoFixture;
using DiaryApp.Core.Entities;
using DiaryApp.Tests.Extensions;

namespace DiaryApp.Tests.Customizations
{
    public class PageCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<MainPage>(composer =>
                                                      composer.With(p => p.Year, fixture.CreateYear())
                                                              .With(p => p.Month, fixture.CreateMonth()));
            fixture.Customize<MonthPage>(composer =>
                                                      composer.With(p => p.Year, fixture.CreateYear())
                                                              .With(p => p.Month, fixture.CreateMonth()));
        }
    }
}
