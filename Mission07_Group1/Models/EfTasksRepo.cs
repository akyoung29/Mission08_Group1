
using SQLitePCL;

namespace Mission08_Group1.Models
{
    public class EfTasksRepo : iTaskRepo
    {
        private TasksContext _context;

        public EfTasksRepo(TasksContext temp)
        {
            _context = temp;
        }

        public List<ToTask> Tasks => _context.ToTask.ToList();
        public List<Category> Categories => _context.Category.ToList();

        public void AddTask(ToTask task)
        {
            _context.ToTask.Add(task);
        }

        public void UpdateTask(ToTask task) // This method now exists
        {
            _context.ToTask.Update(task);
        }

        public void DeleteTask(ToTask task)
        {
            _context.ToTask.Remove(task);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

}
