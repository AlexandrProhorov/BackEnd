namespace ScrumBoard.Exception
{
    public class TaskAlreadyExists : System.Exception
    {
        public TaskAlreadyExists()
            : base("task already exists") {}
    }
}
