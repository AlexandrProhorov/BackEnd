using Xunit;
using ScrumBoard.Exception;
using ScrumBoard.Model.Task;
using ScrumBoard.Model.Column;
using ScrumBoard.Model.Board;

namespace ScrumBoardTest
{
    public class BoardTest
    {
        [Fact]
        public void CreatingBoard_WithNameAndEmptyColumns()
        {
            IBoard board = MockBoard();

            Assert.Equal(_mockName, board.Name);
        }

        [Fact]
        public void AddColumn_ToTheList()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();

            board.AddColumn(column);

            Assert.Collection(board.FindAllColumns(),
                    boardColumn => Assert.Equal(column, boardColumn)
                );
        }

        [Fact]
        public void AddSeveralColumns_ToTheOrder()
        {
            IBoard board = MockBoard();
            int amount = 3;

            for (int i = 0; i < amount; ++i)
            {
                board.AddColumn(new Column(i.ToString()));
            }

            Assert.Collection(board.FindAllColumns(),
                    column => Assert.Equal("0", column.Name),
                    column => Assert.Equal("1", column.Name),
                    column => Assert.Equal("2", column.Name)
                );
        }

        [Fact]
        public void AddBoardWithTheSameName_ThrowsException()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();

            board.AddColumn(column);

            Assert.Throws<ColumnAlreadyExists>(() => board.AddColumn(column));
        }

        [Fact]
        public void AddMoreThanMaxColumnsCount_ThrowsException()
        {
            IBoard board = MockBoard();
            for (int i = 0; i < 10; ++i)
            {
                board.AddColumn(new Column(i.ToString()));
            }

            Assert.Throws<MaxColumnCount>(() => board.AddColumn(MockColumn()));
        }

        [Fact]
        public void FindExistingColumn_ReturnsColumn()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();

            board.AddColumn(column);

            Assert.Equal(column, board.FindColumn(column.Name));
        }

        [Fact]
        public void FindNonExistingColumn_ReturnsNull()
        {
            IBoard board = MockBoard();

            Assert.Null(board.FindColumn(""));
        }

        [Fact]
        public void MoveExistingTask_ItGoToTheNextColumn()
        {
            IBoard board = MockBoard();
            IColumn column1 = new Column("1");
            IColumn column2 = new Column("2");
            board.AddColumn(column1);
            board.AddColumn(column2);
            ITask task = MockTask();
            board.AddTaskToColumn(task, "1");

            board.MoveTask("1", task.Name);

            Assert.Empty(column1.FindAllTasks());
            Assert.Collection(column2.FindAllTasks(),
                    columnTask => Assert.Equal(task, columnTask)
                );
        }

        [Fact]
        public void MoveNonExistingTask_ThrowsException()
        {
            IBoard board = MockBoard();
            for (int i = 0; i < 5; ++i)
            {
                board.AddColumn(new Column(i.ToString()));
            }

            Assert.Throws<TaskNotFound>(() => board.MoveTask("0", "No task"));
        }

        [Fact]
        public void MoveTaskPastLastColumn_ThrowsException()
        {
            IBoard board = MockBoard();
            for(int i = 1; i < 11; i++)
            {
                board.AddColumn(MockColumn());
            }
            //board.AddColumn(MockColumn());
            ITask task = MockTask();
            board.AddTaskToColumn(task);

            Assert.Throws<MaxColumnCount>(() => board.MoveTask(_mockColumnName, task.Name));
        }

        [Fact]
        public void MoveTaskWithNoColumns_ThrowsException()
        {
            IBoard board = MockBoard();

            Assert.Throws<NoColumns>(() => board.MoveTask("No column", "_"));
        }

        [Fact]
        public void AddTaskToBoardWithColumns_ItAppearsInTheFirstColumn()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            ITask task = MockTask();
            board.AddColumn(column);

            board.AddTaskToColumn(task);

            Assert.Collection(column.FindAllTasks(),
                    columnTask => Assert.Equal(task, columnTask)
                );
        }

        [Fact]
        public void AddTaskToBoardWithoutColumns_ThrowsException()
        {
            IBoard board = MockBoard();
            ITask task = MockTask();

            Assert.Throws<NoColumns>(() => board.AddTaskToColumn(task));
        }

        [Fact]
        public void AddTaskToExistingColumn_ItAppearsInTheColumn()
        {
            IBoard board = MockBoard();
            for (int i = 0; i < 5; ++i)
            {
                board.AddColumn(new Column(i.ToString()));
            }
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();

            board.AddTaskToColumn(task, column.Name);

            Assert.Collection(column.FindAllTasks(),
                    columnTask => Assert.Equal(task, columnTask)
                );
        }

        [Fact]
        public void AddTaskToNonExistingColumn_ThrowsException()
        {
            IBoard board = MockBoard();
            for (int i = 0; i < 5; ++i)
            {
                board.AddColumn(new Column(i.ToString()));
            }
            ITask task = MockTask();

            Assert.Throws<ColumnNotFound>(() => board.AddTaskToColumn(task, "No column"));
        }

        [Fact]
        public void RemoveExistingTask_RemovesTaskFromTheColumn()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);

            board.RemoveTask(column.Name, task.Name);

            Assert.Empty(column.FindAllTasks());
        }

        [Fact]
        public void RemoveNonExistingTask_ColumnRemainsUnchanged()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            ITask task = MockTask();
            column.AddTask(task);

            board.RemoveTask(column.Name, task.Name);

            Assert.Collection(column.FindAllTasks(),
                    columnTask => Assert.Equal(task, columnTask)
                );
        }

        [Fact]
        public void ChangeExistingColumnName_NameChanges()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            string newName = "Updated";

            board.ChangeColumnName(column.Name, newName);

            Assert.Collection(board.FindAllColumns(),
                    column => Assert.Equal(newName, column.Name)
                );
        }

        [Fact]
        public void ChangeNonExistingColumnName_ThrowsException()
        {
            IBoard board = MockBoard();

            Assert.Throws<ColumnNotFound>(() => board.ChangeColumnName("No column", "Updated"));
        }

        [Fact]
        public void ChangeExistingTaskName_NameChanges()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);
            string newName = "Updated";

            board.ChangeTaskName(column.Name, task.Name, newName);

            Assert.Equal(newName, task.Name);
        }

        [Fact]
        public void ChangeNonExistingTaskName_ThrowsException()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            //column.AddTask(task);

            Assert.Throws<TaskNotFound>(() => board.ChangeTaskName(column.Name, task.Name, "Updated"));
        }

        [Fact]
        public void ChangeExistingTaskDescription_DescriptionChanges()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);
            string newDescription = "Updated";

            board.ChangeTaskDescription(column.Name, task.Name, newDescription);

            Assert.Equal(newDescription, task.Description);
        }

        [Fact]
        public void ChangeNonExistingTaskDescription_ThrowsException()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            //column.AddTask(task);

            Assert.Throws<TaskNotFound>(() => board.ChangeTaskDescription(column.Name, task.Name, "Updated"));
        }

        [Fact]
        public void ChangeExistingTaskPriority_PriorityChanges()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            column.AddTask(task);
            TaskPriority newPriority = TaskPriority.NONE;

            board.ChangeTaskPriority(column.Name, task.Name, newPriority);

            Assert.Equal(newPriority, task.Priority);
        }

        [Fact]
        public void ChangeNonExistingTaskPriority_ThrowsException()
        {
            IBoard board = MockBoard();
            IColumn column = MockColumn();
            board.AddColumn(column);
            ITask task = MockTask();
            //column.AddTask(task);

            Assert.Throws<TaskNotFound>(() => board.ChangeTaskPriority(column.Name, task.Name, TaskPriority.NONE));
        }

        private IBoard MockBoard()
        {
            return new Board(_mockName);
        }

        private IColumn MockColumn()
        {
            return new Column(_mockName);
        }

        private ITask MockTask()
        {
            return new Task(_mockName, _mockDescription, _mockPriority);
        }

        private string _mockName = "Board title";

        private string _mockColumnName = "Column title";

        private string _mockDescription = "Description";
        private TaskPriority _mockPriority = TaskPriority.MEDIUM;
    }
}
