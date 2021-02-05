using System;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Entities
{
    /// <summary>
    /// Represents one day of month in a habit tracker
    /// </summary>
    public class HabitDay : BaseEntity
    {
        public HabitDay()
        {

        }

        public HabitDay(int number, string note)
        {
            if (number <= 0)
                throw new ArgumentOutOfRangeException(nameof(number));
            Number = number;
            Note = note;
        }

        /// <summary>
        /// Number of day in month
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Note for day
        /// </summary>
        public string Note { get; set; }

        [Required]
        /// <summary>
        /// Id of habit tracker which this day is belongs to
        /// </summary>
        public int HabitTrackerId { get; set; }

        /// <summary>
        /// Habit tracker which this day is belongs to
        /// </summary>
        public virtual HabitTracker HabitTracker { get; set; }

        public HabitDay GetCopy()
        {
            return new HabitDay(Number, Note);
        }

        public override string ToString()
        {
            return $"{Number},{Note}";
        }
    }
}
