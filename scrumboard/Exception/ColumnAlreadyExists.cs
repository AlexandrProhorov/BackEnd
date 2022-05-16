namespace ScrumBoard.Exception
{
    public class ColumnAlreadyExists : System.Exception
    {
        public ColumnAlreadyExists()
            : base("column already exists") {}
    }
}
