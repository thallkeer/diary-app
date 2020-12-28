using DiaryApp.Core.Models.PageAreas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core
{
    public class MonthPage : PageBase
    {       
        [Required]
        public virtual PurchasesArea PurchasesArea { get; set; }
        [Required]
        public virtual DesiresArea DesiresArea { get; set; }
        [Required]
        public virtual IdeasArea IdeasArea { get; set; }
        [Required]
        public virtual GoalsArea GoalsArea { get; set; }

        public MonthPage() : base()
        {
        }

        public MonthPage(int year, int month, AppUser user) : base(year, month, user)
        {
        }

        public override void CreateAreas()
        {
            this.DesiresArea = new DesiresArea(this, true);
            this.GoalsArea = new GoalsArea(this, true);
            this.PurchasesArea = new PurchasesArea(this, true);
            this.IdeasArea = new IdeasArea(this, true);
        }

        public override bool Equals(object obj)
        {
            return obj is MonthPage page &&
                   base.Equals(obj) &&
                   EqualityComparer<PurchasesArea>.Default.Equals(PurchasesArea, page.PurchasesArea) &&
                   EqualityComparer<DesiresArea>.Default.Equals(DesiresArea, page.DesiresArea) &&
                   EqualityComparer<IdeasArea>.Default.Equals(IdeasArea, page.IdeasArea) &&
                   EqualityComparer<GoalsArea>.Default.Equals(GoalsArea, page.GoalsArea);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), PurchasesArea, DesiresArea, IdeasArea, GoalsArea);
        }
    }
}
