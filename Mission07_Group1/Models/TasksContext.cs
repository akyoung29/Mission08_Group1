using Microsoft.EntityFrameworkCore;


namespace Mission08_Group1.Models
{
    public class TasksContext : DbContext
    {

        public DbSet <Category> Categories { get; set; }
        public DbSet<ToTask> Tasks { get; set; }
        public TasksContext(DbContextOptions<TasksContext> options) : base (options)
            {
            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToTask>().ToTable("ToTask");  // This forces EF to use the correct table name

            modelBuilder.Entity<ToTask>()
                .HasOne(t => t.Category)
                .WithMany() // Assuming one category can have many tasks
                .HasForeignKey(t => t.CategoryId);
        }

    }
}
