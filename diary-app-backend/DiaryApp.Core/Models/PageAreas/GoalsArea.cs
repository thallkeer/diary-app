using System.Collections.Generic;
using System.Linq;

namespace DiaryApp.Core.Models.PageAreas
{
    public class GoalsArea : PageAreaBase
    {
        public GoalsArea()
        {

        }
        public GoalsArea(PageBase page, bool needInit) : base(page, "Цели на этот месяц", needInit)
        {

        }
        public virtual List<HabitsTracker> GoalsLists { get; set; } = new List<HabitsTracker>();

        public override PageAreaType AreaType => PageAreaType.Goals;

        public override void AddFromOtherArea(PageAreaBase otherArea)
        {
            if (otherArea is GoalsArea other)
            {
                if (this.GoalsLists.Count == 1 && this.GoalsLists[0].SelectedDays.Count == 0)
                    this.GoalsLists = new List<HabitsTracker>(other.GoalsLists.Count);
                this.GoalsLists.AddRange(other.GoalsLists.Select(gl => new HabitsTracker(gl, this)));
            }
        }

        public override PageAreaBase TransferAreaData(PageBase page)
        {
            var newArea = new GoalsArea(page, false)
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
            GoalsLists.Add(new HabitsTracker() { GoalName = "Название цели" });
        }
    }
}
