using ScrumBoard.Exception;
using ScrumBoard.Model.Task;
using System.Collections.Generic;

namespace ScrumBoard.Model.Column
{
    public class Column : IColumn
    {
        public string Name { get; set; }

        public Column(string name)
        {
            Name = name;
            _tasks = new List<ITask>();
        }

        public void AddTask(ITask task)
        {
            if (FindTask(task.Name) != null)
            {
                throw new TaskAlreadyExists();
            }

            _tasks.Add(task);
        }

        public IReadOnlyCollection<ITask> FindAllTasks()
        {
            return _tasks;
        }

        public ITask? FindTask(string name)
        {
            return _tasks.Find(task => task.Name == name);
        }

        public void RemoveTask(string name)
        {
            _tasks.RemoveAll(task => task.Name == name);
        }

        private List<ITask> _tasks;
    }
}
