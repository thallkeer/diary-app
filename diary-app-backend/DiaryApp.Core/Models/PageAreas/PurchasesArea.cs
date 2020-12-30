using DiaryApp.Core.Extensions;
using System;
using System.Collections.Generic;

namespace DiaryApp.Core.Models.PageAreas
{
    public class PurchasesArea : MonthPageArea, IMonthPageArea<PurchasesArea>
    {
        private const string Title = "Название списка";
        private const string HeaderSTR = "Покупки";

        public virtual List<PurchaseList> PurchasesLists { get; set; } = new List<PurchaseList>();

        public PurchasesArea() : base()
        {

        }

        public PurchasesArea(MonthPage page, bool needInit) : base(page, HeaderSTR, needInit)
        { }        

        public void AddFromOtherArea(PurchasesArea other)
        {
            PurchasesLists.RemoveAll(pl => pl.Items.Count == 0);
            PurchasesLists.AddRange(other.PurchasesLists.CopyPurchaseLists());           
        }

        protected override void Initialize()
        {
            PurchasesLists.AddRange(new PurchaseList[]
            {
                new PurchaseList(Title),
                new PurchaseList(Title)
            });
        }

        public override bool Equals(object obj)
        {
            return obj is PurchasesArea area &&
                   base.Equals(obj) &&
                   EqualityComparer<List<PurchaseList>>.Default.Equals(PurchasesLists, area.PurchasesLists);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), PurchasesLists);
        }
    }    
}
