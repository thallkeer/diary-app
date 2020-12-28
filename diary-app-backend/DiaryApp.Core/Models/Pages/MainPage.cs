using DiaryApp.Core.Models.PageAreas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core
{
    public class MainPage : PageBase
    {
        [Required]
        public virtual ImportantEventsArea ImportantEvents { get; set; }
        [Required]
        public virtual ImportantThingsArea ImportantThings { get; set; }

        public MainPage() : base()
        {

        }

        public MainPage(int year, int month, AppUser user) : base(year, month, user)
        {
        }

        public override void CreateAreas()
        {
            ImportantEvents = new ImportantEventsArea(this, true);
            ImportantThings = new ImportantThingsArea(this, true);
        }

        public override bool Equals(object obj)
        {
            return obj is MainPage page &&
                   base.Equals(obj) &&
                   EqualityComparer<ImportantEventsArea>.Default.Equals(ImportantEvents, page.ImportantEvents) &&
                   EqualityComparer<ImportantThingsArea>.Default.Equals(ImportantThings, page.ImportantThings);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), ImportantEvents, ImportantThings);
        }
    }
}
