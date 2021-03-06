﻿using System.ComponentModel.DataAnnotations;
using DiaryApp.Core.Entities.Pages;

namespace DiaryApp.Core.Entities.PageAreas
{
    public class ImportantEventsArea : PageAreaBase<MainPage>
    {
        private const string Title = "Важные события";

        public ImportantEventsArea() : base()
        {}

        public ImportantEventsArea(MainPage page, bool withInitialization = false) : base(page, Title, withInitialization)
        {}

        [Required]
        public int ImportantEventsId { get; set; }
        public virtual EventList ImportantEvents { get; set; }        

        protected override void Initialize()
        {
            ImportantEvents = new EventList(Title);
        }
    }
}
