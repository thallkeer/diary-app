using DiaryApp.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DiaryApp.Core.Models.PageAreas
{
    public class GoalsArea : PageAreaBase<MonthPage>, IMonthPageArea<GoalsArea>
    {
        private const string HeaderSTR = "Цели на этот месяц";
        public const string GoalNameSTR = "Название цели";

        public virtual List<HabitTracker> GoalLists { get; set; } = new List<HabitTracker>();

        public GoalsArea()
        {

        }
        public GoalsArea(MonthPage page, bool needInit = false) : base(page, HeaderSTR, needInit)
        {

        }

        public void AddFromOtherArea(GoalsArea other)
        {
            GoalLists.RemoveAll(gl => gl.SelectedDays.Count == 0 && gl.GoalName == GoalNameSTR);

            var otherLists = other.GoalLists.Where(gl => gl.GoalName != GoalNameSTR).Select(gl => new HabitTracker(gl, this));

            GoalLists.AddRange(otherLists);
            if (GoalLists.Count == 0)
                Initialize();
        }

        protected override void Initialize()
        {
            GoalLists.Add(new HabitTracker() { GoalName = GoalNameSTR });
        }
    }
}