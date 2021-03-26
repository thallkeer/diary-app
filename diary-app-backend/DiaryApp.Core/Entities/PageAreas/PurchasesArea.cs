using System.Collections.Generic;
using System.Linq;
using DiaryApp.Core.Entities.ListWrappers;
using DiaryApp.Core.Entities.Pages;
using DiaryApp.Core.Interfaces;

namespace DiaryApp.Core.Entities.PageAreas
{
    public class PurchasesArea : MonthPageArea, IMonthPageArea<PurchasesArea>
    {
        private const string Title = "Название списка";
        private const string HeaderStr = "Покупки";

        public PurchasesArea() : base()
        { }

        public PurchasesArea(MonthPage page, bool needInit) : base(page, HeaderStr, needInit)
        { }

        public virtual List<PurchaseList> PurchasesLists { get; set; } = new();

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
                    AreaOwnerId = Id
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
                new(Title),
                new(Title)
            };
        }
    }
}