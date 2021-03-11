using DiaryApp.Core.Interfaces;
using System.Collections.Generic;
using DiaryApp.Core.Entities.PageAreas;
using System.Linq;

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

        public void AddDataFromOtherArea(PurchasesArea other)
        {
            if (Id == 0)
            {
                PurchasesLists.RemoveAll(pl => pl.Items.Count == 0);
            }

            var copiedLists = other.PurchasesLists.Select(pl =>
            {
                var listCopy = new PurchaseList(pl.List.Title)
                {
                    AreaOwner = this,
                    AreaOwnerID = Id
                };

                listCopy.List.Items.AddRange(pl.CopyItems());

                return listCopy;
            });

            PurchasesLists.AddRange(copiedLists);
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