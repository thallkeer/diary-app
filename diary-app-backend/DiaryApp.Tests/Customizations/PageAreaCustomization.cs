using AutoFixture;
using DiaryApp.Core.Models.PageAreas;
using DiaryApp.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
