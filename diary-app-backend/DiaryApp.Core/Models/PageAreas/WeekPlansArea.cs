using DiaryApp.Core.Models.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models.PageAreas
{
    public class WeekPlansArea : PageAreaBase<WeekPage>
    {
        [Required]
        public int WeekNumber { get; set; }
        [Required]
        public virtual WeekPlansList WeekPlansList { get; set; }
        public virtual List<WeekDay> WeekDays { get; set; } = new List<WeekDay>();

        public WeekPlansArea() : base()
        { }

        public WeekPlansArea(WeekPage page, bool init) : base(page, "На этой неделе я хочу", init)
        { }

        protected override void Initialize()
        {
            WeekPlansList = new WeekPlansList(string.Empty);
        }

        public void AddFromOtherArea(WeekPlansArea other)
        {
            throw new NotImplementedException();
        }

        public WeekPlansArea TransferAreaData(WeekPage page)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return obj is WeekPlansArea area &&
                   base.Equals(obj) &&
                   WeekNumber == area.WeekNumber &&
                   EqualityComparer<WeekPlansList>.Default.Equals(WeekPlansList, area.WeekPlansList) &&
                   EqualityComparer<List<WeekDay>>.Default.Equals(WeekDays, area.WeekDays);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), WeekNumber, WeekPlansList, WeekDays);
        }
    }    
}
