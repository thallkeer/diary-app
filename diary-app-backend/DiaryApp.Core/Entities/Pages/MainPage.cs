﻿using System.ComponentModel.DataAnnotations;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Core.Entities.Users;

namespace DiaryApp.Core.Entities.Pages
{
    public class MainPage : PageBase
    {
        public MainPage() : base()
        {

        }

        public MainPage(int year, int month, AppUser user) : base(year, month, user)
        {
        }

        [Required]
        public virtual ImportantEventsArea ImportantEventsArea { get; set; }
        [Required]
        public virtual ImportantThingsArea ImportantThingsArea { get; set; }

        public override void CreateAreas()
        {
            ImportantEventsArea = new ImportantEventsArea(this, true);
            ImportantThingsArea = new ImportantThingsArea(this, true);
        }
    }
}
