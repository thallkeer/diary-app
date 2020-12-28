using System;
using System.Collections.Generic;

namespace DiaryApp.Core.Models.PageAreas
{
    public class IdeasArea : PageAreaBase<MonthPage>, IMonthPageArea<IdeasArea>
    {
        private const string HeaderSTR = "Идеи этого месяца";

        public virtual IdeasList IdeasList { get; set; }

        public IdeasArea()
        {

        }

        public IdeasArea(MonthPage page, bool needInit) : base(page, HeaderSTR, needInit)
        {

        }

        public void AddFromOtherArea(IdeasArea other)
        {
            var otherListItemsCopy = other.IdeasList?.CopyItems();
            if (IdeasList == null)
                Initialize();
            IdeasList.Items.AddRange(otherListItemsCopy);
        }

        protected override void Initialize()
        {
            IdeasList = new IdeasList(string.Empty);
        }

        public override bool Equals(object obj)
        {
            return obj is IdeasArea area &&
                   base.Equals(obj) &&
                   EqualityComparer<IdeasList>.Default.Equals(IdeasList, area.IdeasList);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), IdeasList);
        }
    }   
}
