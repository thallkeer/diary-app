using DiaryApp.Core.Models;
using DiaryApp.Core.Models.PageAreas;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Core
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new HabitTrackerConfiguration());
        }

        public DbSet<EventList> EventLists { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<CommonList> CommonLists { get; set; }
        public DbSet<DesiresList> DesireLists { get; set; }
        public DbSet<IdeasList> IdeaLists { get; set; }
        public DbSet<PurchaseList> PurchaseLists { get; set; }
        //public DbSet<WeekPlansList> WeekPlansLists { get; set; }
        public DbSet<HabitTracker> HabitTrackers { get; set; }
        public DbSet<HabitDay> HabitDays { get; set; }
        public DbSet<ListItem> ListItems { get; set; }
        public DbSet<TodoItem> Todos { get; set; }
        public DbSet<EventItem> Events { get; set; }
        public DbSet<MainPage> MainPages { get; set; }
        public DbSet<MonthPage> MonthPages { get; set; }
        //public DbSet<WeekPage> WeekPages { get; set; }
        public DbSet<ImportantEventsArea> ImportantEventsAreas { get; set; }
        public DbSet<ImportantThingsArea> ImportantThingsAreas { get; set; }
        public DbSet<DesiresArea> DesiresAreas { get; set; }
        public DbSet<GoalsArea> GoalsAreas { get; set; }
        public DbSet<IdeasArea> IdeasAreas { get; set; }
        public DbSet<PurchasesArea> PurchasesAreas { get; set; }
        //public DbSet<WeekPlansArea> WeekPlansAreas { get; set; }
        //public DbSet<NotesArea> NotesAreas { get; set; }
        //public DbSet<WeekDay> WeekDays { get; set; }
        public DbSet<AppUser> Users { get; set; }
    }
}