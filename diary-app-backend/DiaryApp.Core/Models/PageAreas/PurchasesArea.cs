using DiaryApp.Core.Extensions;
using DiaryApp.Core.Interfaces;
using System.Collections.Generic;

namespace DiaryApp.Core.Models.PageAreas
{
    public class PurchasesArea : PageAreaBase<MonthPage>, IMonthPageArea<PurchasesArea>
    {
        private const string Title = "Название списка";
        private const string HeaderSTR = "Покупки";

        public virtual List<PurchasesList> PurchasesLists { get; set; } = new List<PurchasesList>();

        public PurchasesArea() : base()
        {

        }

        public PurchasesArea(MonthPage page, bool needInit = false) : base(page, HeaderSTR, needInit)
        { }        

        public void AddFromOtherArea(PurchasesArea other)
        {
            this.PurchasesLists.RemoveAll(pl => pl.Items.Count == 0);
            this.PurchasesLists.AddRange(other.PurchasesLists.GetCopy<PurchasesList, TodoList, TodoItem>());            
        }

        protected override void Initialize()
        {
            PurchasesLists.AddRange(new PurchasesList[]
                {
                new PurchasesList(Title),
                new PurchasesList(Title)
                });
        }
    }    
}
