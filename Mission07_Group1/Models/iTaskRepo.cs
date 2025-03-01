using Mission08_Group1.Models;

public interface iTaskRepo
{
    List<ToTask> Tasks { get; }
    List<Category> Categories { get; }

    void AddTask(ToTask task);
    void UpdateTask(ToTask task); // Rename it from Update to UpdateTask
    void DeleteTask(ToTask task);
    void SaveChanges();
}
