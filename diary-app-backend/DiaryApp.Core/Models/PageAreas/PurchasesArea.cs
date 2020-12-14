﻿using DiaryApp.Core.Extensions;
using DiaryApp.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DiaryApp.Core.Models.PageAreas
{
    public class PurchasesArea : PageAreaBase<MonthPage>, IMonthPageArea<PurchasesArea>
    {
        private const string Title = "Название списка";
        private const string HeaderSTR = "Покупки";

        public virtual List<PurchasesList> PurchasesLists { get; set; } = new List<PurchasesList>();

        public PageAreaType AreaType => PageAreaType.Purchases;

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

        public PurchasesArea TransferAreaData(MonthPage page)
        {
            var newArea = new PurchasesArea(page)
            {
                PurchasesLists = new List<PurchasesList>(this.PurchasesLists.Count)
            };
            newArea.PurchasesLists = this.PurchasesLists.GetCopy<PurchasesList, TodoList, TodoItem>();
            return newArea;
        }

        public override void Initialize()
        {
            PurchasesLists.AddRange(new PurchasesList[]
                {
                new PurchasesList(Title),
                new PurchasesList(Title)
                });
        }
    }    
}
