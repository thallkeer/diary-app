using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DiaryApp.Core.Entities.ListWrappers;
using DiaryApp.Core.Entities.PageAreas;

namespace DiaryApp.Core.Entities
{
    public class WeekPlansArea : PageAreaBase<WeekPage>
    {
        public WeekPlansArea() : base()
        { }

        public WeekPlansArea(WeekPage page, bool init) : base(page, "На этой неделе я хочу", init)
        { }

        [Required]
        public int WeekNumber { get; set; }
        [Required]
        public virtual WeekPlansList WeekPlansList { get; set; }
        public virtual ICollection<WeekDay> WeekDays { get; set; } = new List<WeekDay>();

        protected override void Initialize()
        {
            WeekPlansList = new WeekPlansList(string.Empty);
        }
    }    
}
