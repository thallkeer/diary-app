using DiaryApp.Core.Extensions;
using DiaryApp.Core.Interfaces;
using System.Collections.Generic;

namespace DiaryApp.Core.Models.PageAreas
{
    public class DesiresArea : PageAreaBase<MonthPage>, IMonthPageArea<DesiresArea>
    {
        private const string HeaderSTR = "В этом месяце я хочу";
        public const string ToVisitSTR = "Посетить";
        public const string ToWatchSTR = "Посмотреть";
        public const string ToReadSTR = "Прочитать";

        public virtual List<DesiresList> DesiresLists { get; set; } = new List<DesiresList>(3);
        public PageAreaType AreaType => PageAreaType.Desires;

        public DesiresArea() : base()
        {

        }
        public DesiresArea(MonthPage page, bool withInitialization = false) : base(page, HeaderSTR, withInitialization)
        {
        }      

        public void AddFromOtherArea(DesiresArea other)
        {
            if (DesiresLists.Count == 0)
                Initialize();
            for (int i = 0; i < other.DesiresLists.Count; i++)
            {
                this.DesiresLists[i].Items.AddRange(other.DesiresLists[i].Items);
            }
        }

        public DesiresArea TransferAreaData(MonthPage page)
        {
            var newArea = new DesiresArea(page, false)
            {
                DesiresLists = new List<DesiresList>(3)
            };
            newArea.DesiresLists = this.DesiresLists.GetCopy<DesiresList, CommonList, ListItem>();          
            return newArea;
        }

        public override void Initialize()
        {
            DesiresLists.AddRange(new DesiresList[]
                {
                    new DesiresList(ToVisitSTR),
                    new DesiresList(ToWatchSTR),
                    new DesiresList(ToReadSTR)
                });
        }
    }    
}
