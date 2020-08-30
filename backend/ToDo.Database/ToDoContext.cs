using ToDo.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ToDo.Database
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {

        }

        public DbSet<ToDoEntity> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoEntity>()
                .HasIndex(e => e.Assignee);
            modelBuilder.Entity<ToDoEntity>()
                .HasIndex(e => e.Author);
            modelBuilder.Entity<ToDoEntity>()
                .HasIndex(e => e.TableId);
            modelBuilder.Entity<ToDoEntity>();
        }
    }
}