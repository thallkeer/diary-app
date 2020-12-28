using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models.PageAreas
{
    public class ImportantThingsArea : PageAreaBase<MainPage>
    {
        private const string Title = "Важные дела";

        [Required]
        public int ImportantThingsID { get; set; }
        public virtual TodoList ImportantThings { get; set; }

        public ImportantThingsArea() : base()
        {

        }

        public ImportantThingsArea(MainPage page, bool withInitialization = false) : base(page, Title, withInitialization)
        {

        }

        protected override void Initialize()
        {
            ImportantThings = new TodoList(Title);
        }

        public override bool Equals(object obj)
        {
            return obj is ImportantThingsArea area &&
                   base.Equals(obj) &&
                   ImportantThingsID == area.ImportantThingsID &&
                   EqualityComparer<TodoList>.Default.Equals(ImportantThings, area.ImportantThings);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), ImportantThingsID, ImportantThings);
        }
    }
}
