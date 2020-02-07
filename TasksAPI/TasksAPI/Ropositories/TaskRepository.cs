using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksAPI.Database;
using TasksAPI.Models;
using TasksAPI.Ropositories.Contracts;

namespace TasksAPI.Ropositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TasksContext _context;

        public TaskRepository(TasksContext tasksContext)
        {
            _context = tasksContext;
        }

        public void Create(TaskModel task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void Delete(TaskModel task)
        {
            throw new NotImplementedException();
        }

        public ICollection<TaskModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public TaskModel GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public List<TaskModel> SyncRestore(ApplicationUser user, DateTime lastSyncDate)
        {
            var query = _context.Tasks.Where(x => x.UserId == user.Id).AsQueryable();

            if (lastSyncDate == null || query == null)
                return null;

            query.Where(x => x.CreatedAt >= lastSyncDate || x.UpdatedAt >= lastSyncDate);

            return query.ToList();
        }

        public List<TaskModel> Sync(List<TaskModel> tasks)
        {
            var newTasks = tasks.Where(x => x.IdTaskApi == 0);

            if (newTasks.Count() < 1)
                return tasks;

            foreach (var task in newTasks)
            {
                Create(task);
            }

            var DeletedUpdatedTask = tasks.Where(x => x.IdTaskApi != 0);

            if (DeletedUpdatedTask.Count() < 1)
                return tasks;

            foreach (var task in DeletedUpdatedTask)
            {
                Update(task);
            }

            return newTasks.ToList();
        }

        public void Update(TaskModel task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }
    }
}
