using ScrumBoard.Model.Task;
using ScrumBoard.Model.Column;
using System.Collections.Generic;

namespace ScrumBoard.Model.Board
{
    public interface IBoard
    {
        public string Name { get; set; }

        public void AddColumn(IColumn column);
        public void ChangeColumnName(string columnName, string newName);
        public IReadOnlyCollection<IColumn> FindAllColumns();
        public IColumn? FindColumn(string name);

        public void AddTaskToColumn(ITask task, string? columnName = null);
        public void ChangeTaskName(string columnName, string taskName, string newName);
        public void ChangeTaskDescription(string columnName, string taskName, string newDescription);
        public void ChangeTaskPriority(string columnName, string taskName, TaskPriority newPriority);
        public void MoveTask(string columnName, string taskName);
        public void RemoveTask(string columnName, string taskName);
    }
}
