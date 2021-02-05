using AutoFixture;
using DiaryApp.Core.Entities;
using DiaryApp.Models.DTO;

namespace DiaryApp.Tests.Extensions
{
    internal static class FixtureExtensions
    {
        public static int CreateInt(this IFixture fixture, int min, int max) => fixture.Create<int>() % (max - min + 1) + min;

        public static int CreateMonth(this IFixture fixture) => fixture.CreateInt(1, 12);

        public static int CreateYear(this IFixture fixture) => fixture.CreateInt(2020, 9999);
 
        public static MonthPage CreateMonthPageForNotLastMonth(this IFixture fixture) => fixture.Build<MonthPage>().With(p => p.Month, fixture.CreateInt(1,11)).Create();

        public static UserDto CreateUser(this IFixture fixture) => fixture.Build<UserDto>().Without(u => u.Id).Create();

        internal static TList CreateList<TList, TItem>(this IFixture fixture)
            where TList : DiaryList<TItem>
            where TItem : DiaryListItem
        {
            var list = fixture.Build<TList>()
                                    .Without(l => l.Items)
                                    .Create();

            var items = fixture.Build<TItem>()
                                    .With(i => i.OwnerID, list.Id)
                                    .With(i => i.Owner, list)
                                    .CreateMany();

            list.Items.AddRange(items);

            return list;
        }

        internal static T CreateListWrapper<T, TList, TItem, TArea>(this IFixture fixture, bool withItems)
            where T : DiaryAreaList<TList, TItem, TArea, MonthPage>
            where TList : DiaryList<TItem>, new()
            where TItem : DiaryListItem
            where TArea : MonthPageArea
        {
            var list = fixture
                                    .Build<T>()
                                    .With(lw => lw.List, fixture.CreateList<TList, TItem>())
                                    .Without(lw => lw.AreaOwner)
                                    .Without(lw => lw.Items)
                                    .Create();

            if (!withItems)
                list.List.Items.Clear();

            return list;
        }

        internal static void CustomizeList<TList, TItem>(this IFixture fixture)
            where TList : DiaryList<TItem>, new()
            where TItem : DiaryListItem
        {
            fixture.Customize<TList>(composer => composer.Without(l => l.Items));
            fixture.Customize<TItem>(composer => composer.Without(i => i.Owner));
        }

        internal static void CustomizeListWrapper<T, TList, TItem, TArea>(this IFixture fixture)
            where T : DiaryAreaList<TList, TItem, TArea, MonthPage>, new()
            where TList : DiaryList<TItem>, new()
            where TItem : DiaryListItem
            where TArea : MonthPageArea
        {
            fixture.Customize<T>(composer =>
                    composer
                            .With(lw => lw.List, fixture.CreateList<TList, TItem>())
                            .Without(lw => lw.AreaOwner)
                            .Without(lw => lw.Items));
        }

        internal static void CustomizePageArea<T>(this IFixture fixture)
            where T : MonthPageArea
        {
            fixture.Customize<T>(composer => composer.Without(pa => pa.Page));
        }
    }
}
