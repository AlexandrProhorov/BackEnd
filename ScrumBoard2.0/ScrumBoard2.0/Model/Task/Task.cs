namespace ScrumBoard.Model.Task
{
    public enum TaskPriority
    {
        HIGH,
        MEDIUM,
        LOW,
        NONE
    }

    public class Task : ITask
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }

        public Task(string name, string description, TaskPriority priority)
        {
            Name = name;
            Description = description;
            Priority = priority;
        }
    }
}
