using DiaryApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiaryApp.Core.Configurations
{
    public class HabitTrackerConfiguration : IEntityTypeConfiguration<HabitTracker>
    {
        public void Configure(EntityTypeBuilder<HabitTracker> builder)
        {
            var converter = new ValueConverter<List<HabitDay>, string>(
              v => string.Join(";", v),
              v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(FromString).ToList()
          );

            var valueComparer = new ValueComparer<List<HabitDay>>(
                (o1, o2) => o1.SequenceEqual(o2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
                );

            builder
                .Property(e => e.SelectedDays)
                .HasConversion(converter)
                .Metadata.SetValueComparer(valueComparer);
        }

        /// <summary>
        /// Converts string to instance of Habit Day entity
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private HabitDay FromString(string str)
        {
            var splited = str.Split(",");
            return new HabitDay { Number = int.Parse(splited[0]), Note = splited.Length == 2 ? splited[1] : string.Empty };
        }
    }
}
