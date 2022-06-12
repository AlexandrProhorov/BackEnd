using ScrumBoard.Exception;
using ScrumBoard.Model.Task;
using ScrumBoard.Model.Column;
using Xunit;

namespace ScrumBoardTest
{
    public class ColumnTest
    {
        [Fact]
        public void CreateColumn_ItHasNameAndEmptyTasks()
        {
            IColumn column = MockColumn();

            Assert.Equal(_mockName, column.Name);
            Assert.Empty(column.FindAllTasks());
        }

        [Fact]
        public void ChangeColumnName_NameChanges()
        {
            IColumn column = MockColumn();
            string newName = "Updated";

            column.Name = newName;

            Assert.Equal(newName, column.Name);
        }

        [Fact]
        public void AddTask_ItAppearsInTheList()
        {
            IColumn column = MockColumn();
            ITask task = MockTask();

            column.AddTask(task);

            Assert.Collection(column.FindAllTasks(),
                    columnTask => Assert.Equal(task, columnTask)
                );
        }

        [Fact]
        public void AddSeveralTasks_TheyAppearInTheOrderOfAddition()
        {
            IColumn column = MockColumn();
            int amount = 3;

            for (int i = 0; i < amount; ++i)
            {
                column.AddTask(new Task(i.ToString(), _mockDescription, _mockPriority));
            }

            Assert.Collection(column.FindAllTasks(),
                    task => Assert.Equal("0", task.Name),
                    task => Assert.Equal("1", task.Name),
                    task => Assert.Equal("2", task.Name)
                );
        }

        [Fact]
        public void AddTaskWithTheSameName_ThrowsException()
        {
            IColumn column = MockColumn();
            ITask task = MockTask();

            column.AddTask(task);

            Assert.Throws<TaskAlreadyExists>(() => column.AddTask(task));
        }

        [Fact]
        public void FindExistingTask_ReturnsTask()
        {
            IColumn column = MockColumn();
            ITask task = MockTask();

            column.AddTask(task);

            Assert.Equal(task, column.FindTask(task.Name));
        }

        [Fact]
        public void FindNonExistingTask_ReturnsNull()
        {
            IColumn column = MockColumn();

            Assert.Null(column.FindTask("Why me not exist"));
        }

        [Fact]
        public void RemoveExistingTask_RemovesTaskFromTheList()
        {
            IColumn column = MockColumn();
            ITask task = MockTask();
            column.AddTask(task);

            column.RemoveTask(task.Name);

            Assert.Empty(column.FindAllTasks());
        }

        [Fact]
        public void RemoveNonExistingTask_TaskListRemainsUnchanged()
        {
            IColumn column = MockColumn();
            ITask task = MockTask();
            column.AddTask(task);

            column.RemoveTask("Nekaya stroka");

            Assert.Single(column.FindAllTasks());
        }

        private IColumn MockColumn()
        {
            return new Column(_mockName);
        }

        private ITask MockTask()
        {
            return new Task(_mockName, _mockDescription, _mockPriority);
        }

        private string _mockName = "Lazy to name :)";
        private string _mockDescription = "alt+255";
        private TaskPriority _mockPriority = TaskPriority.MEDIUM;
    }
}
