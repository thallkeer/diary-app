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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var converter = new ValueConverter<List<int>, string>(
                v => string.Join(";", v),
                v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(val => int.Parse(val)).ToList()
                );

            var valueComparer = new ValueComparer<List<int>>(
                (o1, o2) => o1.SequenceEqual(o2),
                c => c.Aggregate(0, (a,v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
                );

            modelBuilder.Entity<HabitsTracker>()
                .Property(e => e.SelectedDays)
                .HasConversion(converter)
                .Metadata.SetValueComparer(valueComparer);
        }

        public DbSet<EventList> EventLists { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<TodoItem> Todos { get; set; }
        public DbSet<EventItem> Events { get; set; }
        public DbSet<HabitsTracker> HabitsTrackers { get; set; }
        public DbSet<MainPage> MainPages { get; set; }
        public DbSet<MonthPage> MonthPages {get;set;}
        public DbSet<PurchasesArea> PurchasesAreas { get; set; }
        public DbSet<DesiresArea> DesiresAreas { get; set; }
        public DbSet<IdeasArea> IdeasAreas { get; set; }
        public DbSet<GoalsArea> GoalsAreas { get; set; }
        public DbSet<AppUser> Users { get; set; }
    }
}