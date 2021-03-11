using DiaryApp.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using DiaryApp.Core.Entities.PageAreas;

namespace DiaryApp.Core.Entities
{
    public class GoalsArea : MonthPageArea, IMonthPageArea<GoalsArea>
    {
        private const string HeaderSTR = "Трекеры привычек";
        public const string GoalNameSTR = "Название цели";

        public GoalsArea()
        {}

        public GoalsArea(MonthPage page, bool needInit) : base(page, HeaderSTR, needInit)
        {}

        public virtual List<HabitTracker> GoalLists { get; set; } = new List<HabitTracker>();

        public void AddDataFromOtherArea(GoalsArea other)
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