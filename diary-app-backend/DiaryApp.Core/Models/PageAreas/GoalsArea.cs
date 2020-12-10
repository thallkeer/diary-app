using DiaryApp.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DiaryApp.Core.Models.PageAreas
{
    public class GoalsArea : PageAreaBase<MonthPage>, IMonthPageArea<GoalsArea>
    {
        private const string HeaderSTR = "Цели на этот месяц";
        private const string GoalNameSTR = "Название цели";

        public virtual List<HabitTracker> GoalLists { get; set; } = new List<HabitTracker>();

        public PageAreaType AreaType => PageAreaType.Goals;

        public GoalsArea()
        {

        }
        public GoalsArea(MonthPage page, bool needInit = false) : base(page, HeaderSTR, needInit)
        {

        }       

        public void AddFromOtherArea(GoalsArea other)
        {
            if (this.GoalLists.Count == 1 && this.GoalLists[0].SelectedDays.Count == 0)
                this.GoalLists = new List<HabitTracker>(other.GoalLists.Count);
            this.GoalLists.AddRange(other.GoalLists.Select(gl => new HabitTracker(gl, this)));
        }

        public GoalsArea TransferAreaData(MonthPage page)
        {
            var newArea = new GoalsArea(page)
            {
                GoalLists = new List<HabitTracker>()
            };
            this.GoalLists?.ForEach(tracker =>
            {
                newArea.GoalLists.Add(new HabitTracker(tracker, newArea));
            });
            return newArea;
        }

        protected override void Initialize()
        {
            GoalLists.Add(new HabitTracker() { GoalName = GoalNameSTR });
        }
    }
}
