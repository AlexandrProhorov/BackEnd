namespace ScrumBoard.Exception
{
    public class MaxColumnCount : System.Exception
    {
        public MaxColumnCount()
            : base("column count exceeded") {}
    }
}
