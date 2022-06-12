using System;
using ScrumBoard.Model.Task;
using ScrumBoard.Model.Board;
using ScrumBoard.Model.Column;

namespace ScrumBoard
{
    public class ScrumBoard
    {
        public static void Main()
        {
            try
            {
                IBoard board = new Board("University");
                IColumn todoColumn = new Column("In queue");
                IColumn lessons = new Column("Lessons");

                board.AddColumn(todoColumn);
                board.AddColumn(lessons);

                Console.WriteLine("\n Created board  \n");
                PrintBoard(board);

                ITask math = new Task("Math", "preparing for the control work", TaskPriority.MEDIUM);
                board.AddTaskToColumn(math);
                Console.WriteLine("\n  One task added  \n");
                PrintBoard(board);

                board.MoveTask(todoColumn.Name, math.Name);
                Console.WriteLine("\n  Task moved  \n");
                PrintBoard(board);

                ITask physics = new Task("Physics", "exploring Newtons laws", TaskPriority.HIGH);
                board.AddTaskToColumn(physics, todoColumn.Name);
                Console.WriteLine("\n  Task added in \"In queue\"  \n");
                PrintBoard(board);

                board.ChangeTaskName(lessons.Name, math.Name, "Probability theory");
                board.ChangeTaskDescription(lessons.Name, math.Name, "learning next theme");
                board.ChangeTaskPriority(lessons.Name, math.Name, TaskPriority.HIGH);
                Console.WriteLine("\n  Updated math task  \n");
                PrintBoard(board);

                board.MoveTask(todoColumn.Name, physics.Name);
                Console.WriteLine("\n  All in lessons  \n");
                PrintBoard(board);

                Console.WriteLine("\n  Move task in last column  \n");
                board.MoveTask(lessons.Name, math.Name);
                PrintBoard(board);

                board.RemoveTask(lessons.Name, physics.Name);
                Console.WriteLine("\n  Removed all tasks  \n");
                PrintBoard(board);
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public static void PrintBoard(IBoard board)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (IColumn column in board.FindAllColumns())
            {
                PrintColumn(column);
            }
            Console.ResetColor();
        }

        public static void PrintColumn(IColumn column)
        {
            Console.WriteLine($"   {column.Name}  ");
            PrintTasks(column);
        }

        public static void PrintTasks(IColumn column)
        {
            foreach (ITask task in column.FindAllTasks())
            {
                PrintTask(task);
            }
        }

        public static void PrintTask(ITask task)
        {
            Console.WriteLine($"  [{TaskPriorityToString[(int)task.Priority]}] {task.Name}: {task.Description}");
        }

        public static readonly string[] TaskPriorityToString = 
        {
            "HIGH",
            "MEDIUM",
            "LOW",
            "NONE"
        };
    }
}
