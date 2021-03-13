using DiaryApp.Core.Configurations;
using DiaryApp.Core.Entities;
using DiaryApp.Core.Entities.DiaryLists;
using DiaryApp.Core.Entities.ListWrappers;
using DiaryApp.Core.Entities.Notifications;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Core.Entities.Pages;
using DiaryApp.Core.Entities.Users;
using DiaryApp.Core.Entities.Users.Settings;
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

            modelBuilder.ApplyConfiguration(new MainPageConfiguration());
            modelBuilder.ApplyConfiguration(new MonthPageConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationSettingsConfiguration());
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
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<PageAreaTransferSettings> PageAreaTransferSettings { get; set; }
        public DbSet<NotificationSettings> NotificationSettings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}