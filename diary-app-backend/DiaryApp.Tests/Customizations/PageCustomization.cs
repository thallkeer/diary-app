using AutoFixture;
using DiaryApp.Core;
using DiaryApp.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
