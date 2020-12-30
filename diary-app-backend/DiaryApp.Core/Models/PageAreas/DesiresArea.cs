using DiaryApp.Core.Interfaces;
using System.Collections.Generic;

namespace DiaryApp.Core.Models.PageAreas
{
    public class DesiresArea : MonthPageArea, IMonthPageArea<DesiresArea>
    {
        private const string HeaderSTR = "В этом месяце я хочу";
        public const string ToVisitSTR = "Посетить";
        public const string ToWatchSTR = "Посмотреть";
        public const string ToReadSTR = "Прочитать";

        public DesiresArea() : base()
        { }

        public DesiresArea(MonthPage page, bool withInitialization) : base(page, HeaderSTR, withInitialization)
        { }

        public virtual List<DesiresList> DesiresLists { get; set; } = new List<DesiresList>(3);

        public void AddFromOtherArea(DesiresArea other)
        {
            if (DesiresLists.Count == 0)
                Initialize();
            for (int i = 0; i < other.DesiresLists.Count; i++)
            {
                var otherItemsCopy = other.DesiresLists[i].CopyItems();
                DesiresLists[i].Items.AddRange(otherItemsCopy);
            }
        }

        protected override void Initialize()
        {
            DesiresLists = new List<DesiresList>
            {
                new DesiresList(ToVisitSTR),
                new DesiresList(ToWatchSTR),
                new DesiresList(ToReadSTR)
            };
        }
    }
}
