using Microsoft.EntityFrameworkCore;


namespace Mission08_Group1.Models
{
    public class TasksContext : DbContext
    {

        public DbSet <Category> Categories { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public TasksContext(DbContextOptions<TasksContext> options) : base (options)
            {
            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships and constraints if necessary
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Category)
                .WithMany() // Assuming one Category can have many Tasks
                .HasForeignKey(t => t.CategoryId);
        }
    }
}
