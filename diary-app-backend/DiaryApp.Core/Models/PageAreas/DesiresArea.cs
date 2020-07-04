using DiaryApp.Core.Models.Lists;
using System.Collections.Generic;

namespace DiaryApp.Core.Models.PageAreas
{
    public class DesiresArea : PageAreaBase
    {
        public DesiresArea()
        {

        }
        public DesiresArea(PageBase page, bool init) : base(page, "В этом месяце я хочу", init)
        { }

        public virtual List<CommonList> DesiresLists { get; set; } = new List<CommonList>(3);

        public override PageAreaType AreaType => PageAreaType.Desires;

        public override PageAreaBase TransferAreaData(PageBase page)
        {
            var newArea = new DesiresArea(page, false)
            {
                DesiresLists = new List<CommonList>(3)
            };
            this.DesiresLists.ForEach(dl =>
            {
                newArea.DesiresLists.Add(dl.CreateDeepCopy<CommonList, ListItem>(page));
            });
            return newArea;
        }

        public override void AddFromOtherArea(PageAreaBase otherArea)
        {
            if (otherArea is DesiresArea other)
            {
                //var emptyLists = this.DesiresLists.FindAll(pl => pl.Items.Count == 0);
                //this.DesiresLists.RemoveAll(pl => emptyLists.Contains(pl));
                for (int i = 0; i < other.DesiresLists.Count; i++)
                {
                    this.DesiresLists[i].Items.AddRange(other.DesiresLists[i].Items);
                }
            }
        }

        protected override void Initialize()
        {
            DesiresLists.AddRange(new CommonList[]
                {
                    new CommonList("Посетить", Page),
                    new CommonList("Посмотреть", Page),
                    new CommonList("Прочитать", Page)
                });
        }
    }
}
