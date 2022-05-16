using ScrumBoard.Model.Task;
using System.Collections.Generic;

namespace ScrumBoard.Model.Column
{
    public interface IColumn
    {
        public string Name { get; set; }

        public void AddTask(ITask task);
        public IReadOnlyCollection<ITask> FindAllTasks();
        public ITask? FindTask(string name);
        public void RemoveTask(string name);
    }
}
