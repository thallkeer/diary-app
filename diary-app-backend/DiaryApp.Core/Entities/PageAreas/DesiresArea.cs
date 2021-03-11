using System;
using System.Collections.Generic;
using DiaryApp.Core.Interfaces;

namespace DiaryApp.Core.Entities.PageAreas
{
    public class DesiresArea : MonthPageArea, IMonthPageArea<DesiresArea>
    {
        private const string HeaderStr = "В этом месяце я хочу";
        public const string ToVisitStr = "Посетить";
        public const string ToWatchStr = "Посмотреть";
        public const string ToReadStr = "Прочитать";

        public DesiresArea() : base()
        { }

        public DesiresArea(MonthPage page, bool withInitialization) : base(page, HeaderStr, withInitialization)
        { }

        public virtual List<DesiresList> DesiresLists { get; set; } = new(3);

        public void AddDataFromOtherArea(DesiresArea other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            if (DesiresLists.Count == 0)
                Initialize();
            for (var i = 0; i < other.DesiresLists.Count; i++)
            {
                var otherItemsCopy = other.DesiresLists[i].CopyItems();
                DesiresLists[i].Items.AddRange(otherItemsCopy);
            }
        }

        protected override void Initialize()
        {
            DesiresLists = new List<DesiresList>
            {
                new(ToVisitStr),
                new(ToWatchStr),
                new(ToReadStr)
            };
        }
    }
}
