using System.Collections.Generic;
using System.Linq;
using DiaryApp.Core.Entities.DiaryLists;
using DiaryApp.Core.Entities.Pages;
using DiaryApp.Core.Interfaces;

namespace DiaryApp.Core.Entities.PageAreas
{
    public class GoalsArea : MonthPageArea, IMonthPageArea<GoalsArea>
    {
        private const string HeaderStr = "Трекеры привычек";
        public const string GoalNameStr = "Название цели";

        public GoalsArea()
        {}

        public GoalsArea(MonthPage page, bool needInit) : base(page, HeaderStr, needInit)
        {}

        public virtual List<HabitTracker> GoalLists { get; set; } = new();

        public void AddDataFromOtherArea(GoalsArea other)
        {
            GoalLists.RemoveAll(gl => gl.SelectedDays.Count == 0 && gl.GoalName == GoalNameStr);

            var otherLists = other.GoalLists.Where(gl => gl.GoalName != GoalNameStr).Select(gl => gl.GetCopy());

            GoalLists.AddRange(otherLists);

            if (GoalLists.Count == 0)
                Initialize();
        }

        protected override void Initialize()
        {
            GoalLists = new List<HabitTracker>
            {
                new(GoalNameStr)
            };
        }
    }
}