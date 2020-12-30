using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models.PageAreas
{
    public class ImportantEventsArea : PageAreaBase<MainPage>
    {
        private const string Title = "Важные события";

        public ImportantEventsArea() : base()
        {}

        public ImportantEventsArea(MainPage page, bool withInitialization = false) : base(page, Title, withInitialization)
        {}

        [Required]
        public int ImportantEventsID { get; set; }
        public virtual EventList ImportantEvents { get; set; }        

        protected override void Initialize()
        {
            ImportantEvents = new EventList(Title);
        }
    }
}
