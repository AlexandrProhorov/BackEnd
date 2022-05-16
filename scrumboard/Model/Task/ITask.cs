namespace ScrumBoard.Model.Task
{
    public interface ITask
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
    }
}
