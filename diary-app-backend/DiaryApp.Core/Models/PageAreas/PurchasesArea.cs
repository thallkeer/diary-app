using DiaryApp.Core.Models.Lists;
using System.Collections.Generic;
using System.Linq;

namespace DiaryApp.Core.Models.PageAreas
{
    public class PurchasesArea : PageAreaBase
    {
        public PurchasesArea()
        {

        }
        public PurchasesArea(PageBase page, bool needInit) : base(page, "Покупки", needInit)
        { }

        public virtual List<TodoList> PurchasesLists { get; set; } = new List<TodoList>();

        public override PageAreaType AreaType => PageAreaType.Purchases;

        public override void AddFromOtherArea(PageAreaBase otherArea)
        {
            if (otherArea is PurchasesArea other)
            {
                var emptyLists = this.PurchasesLists.FindAll(pl => pl.Items.Count == 0);
                this.PurchasesLists.RemoveAll(pl => emptyLists.Contains(pl));
                this.PurchasesLists.AddRange(other.PurchasesLists.Select(pl => pl.CreateDeepCopy<TodoList,TodoItem>(this.Page)));
            }
        }

        public override PageAreaBase TransferAreaData(PageBase page)
        {
            var newArea = new PurchasesArea(page, false)
            {
                PurchasesLists = new List<TodoList>()
            };
            newArea.PurchasesLists.AddRange(this.PurchasesLists.Select(pl => pl.CreateDeepCopy<TodoList, TodoItem>(page)));
            return newArea;
        }

        protected override void Initialize()
        {
            PurchasesLists.AddRange(new TodoList[]
                {
                new TodoList {Title = "Название списка", Page = this.Page},
                new TodoList {Title = "Название списка", Page = this.Page}
                });
        }
    }
}
