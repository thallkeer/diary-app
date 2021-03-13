using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DiaryApp.Core.Entities.Users;

namespace DiaryApp.Core.Entities.Pages
{
    /// <summary>
    /// Represents base page class for diary
    /// </summary>
    public abstract class PageBase : BaseEntity
    {
        private const int MinimumYear = 2020;
        private const int MaximumYear = 9999;
        private const int MinimumMonth = 1;
        private const int MaximumMonth = 12;
        
        public PageBase()
        {}

        public PageBase(int year, int month, AppUser user)
        {
            Guard.Against.OutOfRange(year, nameof(year), MinimumYear, MaximumYear);
            Guard.Against.OutOfRange(month, nameof(month), MinimumMonth ,MaximumMonth);
            Year = year;
            Month = month;
            User = user;
            UserId = user?.Id ?? 0;
        }

        private int year;
        [Required]
        [Range(MinimumYear, MaximumYear)]
        public int Year 
        {
            get
            {
                return year;
            }
            set
            {
                Guard.Against.OutOfRange(value, nameof(value), MinimumYear, MaximumYear);
                year = value;
            } 
        }

        private int month;
        [Required]
        [Range(MinimumMonth, MaximumMonth)]
        public int Month
        {
            get
            {
                return month;
            }
            set
            {
                Guard.Against.OutOfRange(month, nameof(month), MinimumMonth ,MaximumMonth);
                month = value;
            }
        }

        [Required]
        public int UserId { get; set; }

        public virtual AppUser User { get; set; }        

        /// <summary>
        /// Method for descendants to implement custom creation of page areas
        /// </summary>
        public abstract void CreateAreas();

        public override string ToString()
        {
            return $"{Id} {Year} {Month} | {UserId}";
        }
    }
}
