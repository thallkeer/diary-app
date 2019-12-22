using DiaryApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Core
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<EventList> Events { get; set; }
        public DbSet<TodoList> Todos { get; set; }
    }
}
