using DiaryApp.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DiaryApp.Core.Models.PageAreas
{
    public class GoalsArea : PageAreaBase<MonthPage>, IMonthPageArea<GoalsArea>
    {
        private const string HeaderSTR = "Цели на этот месяц";
        private const string GoalNameSTR = "Название цели";

        public virtual List<HabitsTracker> GoalsLists { get; set; } = new List<HabitsTracker>();

        public PageAreaType AreaType => PageAreaType.Goals;

        public GoalsArea()
        {

        }
        public GoalsArea(MonthPage page, bool needInit = false) : base(page, HeaderSTR, needInit)
        {

        }       

        public void AddFromOtherArea(GoalsArea other)
        {
            if (this.GoalsLists.Count == 1 && this.GoalsLists[0].SelectedDays.Count == 0)
                this.GoalsLists = new List<HabitsTracker>(other.GoalsLists.Count);
            this.GoalsLists.AddRange(other.GoalsLists.Select(gl => new HabitsTracker(gl, this)));
        }

        public GoalsArea TransferAreaData(MonthPage page)
        {
            var newArea = new GoalsArea(page)
            {
                GoalsLists = new List<HabitsTracker>()
            };
            this.GoalsLists?.ForEach(tracker =>
            {
                newArea.GoalsLists.Add(new HabitsTracker(tracker, newArea));
            });
            return newArea;
        }

        protected override void Initialize()
        {
            GoalsLists.Add(new HabitsTracker() { GoalName = GoalNameSTR });
        }
    }
}
