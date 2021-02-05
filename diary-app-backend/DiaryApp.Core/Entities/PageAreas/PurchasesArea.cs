using DiaryApp.Core.Extensions;
using DiaryApp.Core.Interfaces;
using System.Collections.Generic;

namespace DiaryApp.Core.Entities
{
    public class PurchasesArea : MonthPageArea, IMonthPageArea<PurchasesArea>
    {
        private const string Title = "Название списка";
        private const string HeaderSTR = "Покупки";

        public PurchasesArea() : base()
        { }

        public PurchasesArea(MonthPage page, bool needInit) : base(page, HeaderSTR, needInit)
        { }

        public virtual List<PurchaseList> PurchasesLists { get; set; } = new List<PurchaseList>();

        public void AddFromOtherArea(PurchasesArea other)
        {
            PurchasesLists.RemoveAll(pl => pl.Items.Count == 0);
            PurchasesLists.AddRange(other.PurchasesLists.CopyPurchaseLists());
        }

        protected override void Initialize()
        {
            PurchasesLists = new List<PurchaseList>
            {
                new PurchaseList(Title),
                new PurchaseList(Title)
            };
        }
    }
}