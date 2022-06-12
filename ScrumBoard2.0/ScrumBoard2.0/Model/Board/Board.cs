using ScrumBoard.Exception;
using ScrumBoard.Model.Task;
using ScrumBoard.Model.Column;
using System.Collections.Generic;

namespace ScrumBoard.Model.Board
{
    public class Board : IBoard
    {
        public string Name { get; set; }

        public Board(string name)
        {
            Name = name;
            _columns = new List<IColumn>();
        }

        public void AddColumn(IColumn column)
        {
            if (_columns.Count == MaxColumnCount)
            {
                throw new MaxColumnCount();
            }

            if (FindColumn(column.Name) != null)
            {
                throw new ColumnAlreadyExists();
            }

            _columns.Add(column);
        }

        public IReadOnlyCollection<IColumn> FindAllColumns()
        {
            return _columns;
        }

        public IColumn? FindColumn(string name)
        {
            return _columns.Find(column => column.Name == name);
        }

        public void MoveTask(string columnName, string taskName)
        {
            if (_columns.Count == 0)
            {
                throw new NoColumns();
            }

            int columnIndex = _columns.FindIndex(elem => elem.Name == columnName);
            if (columnIndex == -1)
            {
                throw new ColumnNotFound();
            }

            ITask? task = _columns[columnIndex].FindTask(taskName);
            if (task != null)
            {
                _columns[columnIndex].RemoveTask(taskName);

                if (columnIndex != _columns.Count - 1)
                {
                    _columns[columnIndex + 1].AddTask(task);
                }
                return;
            }

            throw new TaskNotFound();
        }

        public void AddTaskToColumn(ITask task, string? columnName = null)
        {
            if (_columns.Count == 0)
            {
                throw new NoColumns();
            }

            if (columnName == null)
            {
                _columns[0].AddTask(task);
                return;
            }

            IColumn? column = FindColumn(columnName);
            if (column == null)
            {
                throw new ColumnNotFound();
            }

            column.AddTask(task);
        }

        public void RemoveTask(string columnName, string taskName)
        {
            IColumn? column = FindColumn(columnName);
            if (column == null)
            {
                throw new ColumnNotFound();
            }

            column.RemoveTask(taskName);
            
        }

        public void ChangeColumnName(string columnName, string newName)
        {
            IColumn? column = FindColumn(columnName);
            if (column == null)
            {
                throw new ColumnNotFound();
            }

            column.Name = newName;
        }

        public void ChangeTaskName(string columnName, string taskName, string newName)
        {
            IColumn? column = FindColumn(columnName);
            if (column == null)
            {
                throw new ColumnNotFound();
            }

            ITask? task = column.FindTask(taskName);
                if (task != null)
                {
                    task.Name = newName;
                    return;
                }

            throw new TaskNotFound();
        }

        public void ChangeTaskDescription(string columnName, string taskName, string newDescription)
        {
            IColumn? column = FindColumn(columnName);
            if (column == null)
            {
                throw new ColumnNotFound();
            }

            ITask? task = column.FindTask(taskName);
            if (task != null)
            {
                task.Description = newDescription;
                return;
            }

            throw new TaskNotFound();
        }

        public void ChangeTaskPriority(string columnName, string taskName, TaskPriority newPriority)
        {
            IColumn? column = FindColumn(columnName);
            if (column == null)
            {
                throw new ColumnNotFound();
            }

            ITask? task = column.FindTask(taskName);
            if (task != null)
            {
                task.Priority = newPriority;
                return;
            }

            throw new TaskNotFound();
        }

        public const int MaxColumnCount = 10;
        public List<IColumn> _columns;
    }
}
