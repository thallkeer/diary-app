using DiaryApp.Core.Models;
using DiaryApp.Core.Models.Lists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiaryApp.Core
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        private HabitDay FromString(string str)
        {
            var splited = str.Split(",");
            return new HabitDay { Number = int.Parse(splited[0]), Note = splited.Length == 2 ? splited[1] : string.Empty };
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            
            var converter = new ValueConverter<List<HabitDay>, string>(
                v => string.Join(";", v),
                v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(FromString).ToList()
            );          

            var valueComparer = new ValueComparer<List<HabitDay>>(
                (o1, o2) => o1.SequenceEqual(o2),
                c => c.Aggregate(0, (a,v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
                );

            modelBuilder.Entity<HabitsTracker>()
                .Property(e => e.SelectedDays)
                .HasConversion(converter)
                .Metadata.SetValueComparer(valueComparer);
        }

        public DbSet<DesireList> DesiresLists { get; set; }
        public DbSet<IdeasList> IdeasLists { get; set; }
        public DbSet<EventList> EventLists { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<CommonList> CommonLists { get; set; }
        public DbSet<ListItem> ListItems { get; set; }
        public DbSet<TodoItem> Todos { get; set; }
        public DbSet<EventItem> Events { get; set; }
        public DbSet<HabitsTracker> HabitsTrackers { get; set; }
        public DbSet<MainPage> MainPages { get; set; }
        public DbSet<MonthPage> MonthPages {get;set;}
        public DbSet<PageAreaBase> PageAreas { get; set; }
        public DbSet<AppUser> Users { get; set; }
    }
}