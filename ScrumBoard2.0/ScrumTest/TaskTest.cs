using Xunit;
using ScrumBoard.Model.Task;

namespace ScrumBoardTest
{
    public class TaskTest
    {
        [Fact]
        public void CreateTask_ItHasProperties()
        {
            ITask task = MockTask();

            Assert.Equal(_mockName, task.Name);
            Assert.Equal(_mockDescription, task.Description);
            Assert.Equal(_mockPriority, task.Priority);
        }

        [Fact]
        public void ChangeTaskName_NameChanges()
        {
            ITask task = MockTask();
            string newName = "Updated";

            task.Name = newName;

            Assert.Equal(newName, task.Name);
        }

        [Fact]
        public void ChangeTaskDescription_DescriptionChanges()
        {
            ITask task = MockTask();
            string newDescription = "Updated";

            task.Description = newDescription;

            Assert.Equal(newDescription, task.Description);
        }

        [Fact]
        public void ChangeTaskPriority_PriorityChanges()
        {
            ITask task = MockTask();
            TaskPriority newPriority = TaskPriority.HIGH;

            task.Priority = newPriority;

            Assert.Equal(newPriority, task.Priority);
        }

        private ITask MockTask()
        {
            return new Task(_mockName, _mockDescription, _mockPriority);
        }

        private string _mockName = "Lazy to name :)";
        private string _mockDescription = "alt+255";
        private TaskPriority _mockPriority = TaskPriority.NONE;
    }
}
