using DiaryApp.Core.Models.Lists;

namespace DiaryApp.Core.Models.PageAreas
{
    public class IdeasArea : PageAreaBase
    {
        public virtual CommonList IdeasList { get; set; }

        public override PageAreaType AreaType => PageAreaType.Ideas;

        public IdeasArea()
        {

        }
        public IdeasArea(PageBase page, bool needInit) : base(page, "Идеи этого месяца", needInit)
        {

        }

        public override PageAreaBase TransferAreaData(PageBase page)
        {
            var newArea = new IdeasArea(page, false)
            {
                IdeasList = this.IdeasList.CreateDeepCopy<CommonList, ListItem>(page)
            };
            return newArea;
        }

        public override void AddFromOtherArea(PageAreaBase otherArea)
        {
            if (otherArea is IdeasArea other)
            {
                this.IdeasList.Items.AddRange(other.IdeasList.Items);
            }
        }

        protected override void Initialize()
        {
            IdeasList = new CommonList("", this.Page);
        }
    }
}
