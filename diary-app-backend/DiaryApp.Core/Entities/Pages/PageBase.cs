using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DiaryApp.Core.Entities.Users;

namespace DiaryApp.Core.Entities.Pages
{
    /// <summary>
    /// Represents base page class for a diary.
    /// </summary>
    public abstract class PageBase : BaseEntity
    {
        private const int MinimumYear = 2020;
        private const int MaximumYear = 9999;
        private const int MinimumMonth = 1;
        private const int MaximumMonth = 12;

        private int year;
        private int month;

        public PageBase()
        { }

        public PageBase(int year, int month, AppUser user)
        {
            Guard.Against.OutOfRange(year, nameof(year), MinimumYear, MaximumYear);
            Guard.Against.OutOfRange(month, nameof(month), MinimumMonth, MaximumMonth);
            Guard.Against.Null(user, nameof(user));
            Year = year;
            Month = month;
            User = user;
        }

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
                Guard.Against.OutOfRange(value, nameof(value), MinimumMonth, MaximumMonth);
                month = value;
            }
        }

        [Required]
        public int UserId { get; set; }

        public virtual AppUser User { get; set; }

        /// <summary>
        /// Returns the year and month for the next page
        /// </summary>
        public PageDate GetNextPageDate()
        {
            return new PageDate(Year, Month).GetNextPageDate();
        }

        /// <summary>
        /// Returns the year and month for the previous page
        /// </summary>
        public PageDate GetPreviousPageDate()
        {
            return new PageDate(Year, Month).GetPreviousPageDate();
        }

        /// <summary>
        /// Method for descendants to implement custom creation of page areas
        /// </summary>
        public abstract void CreateAreas();

        public override string ToString()
        {
            return $"{Id} {Year} {Month} | {UserId}";
        }
    }

    public record PageDate(int Year, int Month)
    {
        public PageDate GetPreviousPageDate()
        {
            bool isJanuary = Month == 1;
            int year = isJanuary ? Year - 1 : Year;
            int month = isJanuary ? 12 : Month - 1;
            return new PageDate(year, month);
        }

        public PageDate GetNextPageDate()
        {
            bool isDecember = Month == 12;
            int year = isDecember ? Year + 1 : Year;
            int month = isDecember ? 1 : Month + 1;
            return new PageDate(year, month);
        }
    }
}
