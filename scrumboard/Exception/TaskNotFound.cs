namespace ScrumBoard.Exception
{
    public class TaskNotFound : System.Exception
    {
        public TaskNotFound()
            : base("task not found") {}
    }
}
