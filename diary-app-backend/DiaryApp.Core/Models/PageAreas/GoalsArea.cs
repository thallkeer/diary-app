using DiaryApp.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DiaryApp.Core.Models
{
    public class GoalsArea : MonthPageArea, IMonthPageArea<GoalsArea>
    {
        private const string HeaderSTR = "Цели на этот месяц";
        public const string GoalNameSTR = "Название цели";

        public GoalsArea()
        {}

        public GoalsArea(MonthPage page, bool needInit) : base(page, HeaderSTR, needInit)
        {}

        public virtual List<HabitTracker> GoalLists { get; set; } = new List<HabitTracker>();

        public void AddFromOtherArea(GoalsArea other)
        {
            GoalLists.RemoveAll(gl => gl.SelectedDays.Count == 0 && gl.GoalName == GoalNameSTR);

            var otherLists = other.GoalLists.Where(gl => gl.GoalName != GoalNameSTR).Select(gl => gl.GetCopy());

            GoalLists.AddRange(otherLists);

            if (GoalLists.Count == 0)
                Initialize();
        }

        protected override void Initialize()
        {
            GoalLists = new List<HabitTracker>
            {
                new HabitTracker(GoalNameSTR)
            };
        }
    }
}