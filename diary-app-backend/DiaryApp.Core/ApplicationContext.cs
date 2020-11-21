using DiaryApp.Core.Models;
using DiaryApp.Core.Models.Lists;
using DiaryApp.Core.Models.PageAreas;
using DiaryApp.Core.Models.Pages;
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
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
                );

            modelBuilder.Entity<HabitsTracker>()
                .Property(e => e.SelectedDays)
                .HasConversion(converter)
                .Metadata.SetValueComparer(valueComparer);

            //modelBuilder.Entity<DesiresList>()
            //   .HasOne(nameof(DesiresList.DesiresArea))
            //   .WithMany(nameof(DesiresArea.DesiresLists))
            //   .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<PurchasesList>()
            //    .HasOne(nameof(PurchasesList.PurchasesArea))
            //    .WithMany(nameof(PurchasesArea.PurchasesLists))
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<WeekPlansList>()
            //    .HasOne(nameof(WeekPlansList.WeekPlansArea))
            //    .WithOne(nameof(WeekPlansArea.WeekPlansList))
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<IdeasList>()
            //    .HasOne(nameof(IdeasList.IdeasArea))
            //    .WithOne(nameof(IdeasArea.IdeasList))
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<HabitsTracker>()
            //   .HasOne(nameof(HabitsTracker.GoalsArea))
            //   .WithMany(nameof(GoalsArea.GoalsLists))
            //   .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<PurchasesArea>()
            //    .HasOne(typeof(MonthPage))
            //    .WithOne(nameof(MonthPage.PurchasesArea))
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<GoalsArea>()
            //   .HasOne(typeof(MonthPage))
            //   .WithOne(nameof(MonthPage.GoalsArea))
            //   .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<IdeasArea>()
            // .HasOne(typeof(MonthPage))
            // .WithOne(nameof(MonthPage.IdeasArea))
            // .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<DesiresArea>()
            //   .HasOne(typeof(MonthPage))
            //   .WithOne(nameof(MonthPage.DesiresArea))
            //   .OnDelete(DeleteBehavior.Restrict);                  
        }

        public DbSet<EventList> EventLists { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<CommonList> CommonLists { get; set; }
        public DbSet<DesiresList> DesiresLists { get; set; }
        public DbSet<IdeasList> IdeasLists { get; set; }
        public DbSet<PurchasesList> PurchasesLists { get; set; }
        public DbSet<WeekPlansList> WeekPlansLists { get; set; }
        public DbSet<HabitsTracker> HabitsTrackers { get; set; }
        public DbSet<ListItem> ListItems { get; set; }
        public DbSet<TodoItem> Todos { get; set; }
        public DbSet<EventItem> Events { get; set; }
        public DbSet<MainPage> MainPages { get; set; }
        public DbSet<MonthPage> MonthPages { get; set; }
        public DbSet<WeekPage> WeekPages { get; set; }
        public DbSet<ImportantEventsArea> ImportantEventsAreas { get; set; }
        public DbSet<ImportantThingsArea> ImportantThingsAreas { get; set; }
        public DbSet<DesiresArea> DesiresAreas { get; set; }
        public DbSet<GoalsArea> GoalsAreas { get; set; }
        public DbSet<IdeasArea> IdeasAreas { get; set; }
        public DbSet<PurchasesArea> PurchasesAreas { get; set; }
        public DbSet<WeekPlansArea> WeekPlansAreas { get; set; }
        public DbSet<NotesArea> NotesAreas { get; set; }
        public DbSet<WeekDay> WeekDays { get; set; }
        public DbSet<AppUser> Users { get; set; }
    }
}