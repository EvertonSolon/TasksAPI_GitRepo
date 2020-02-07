using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksAPI.Models;

namespace TasksAPI.Ropositories.Contracts
{
    public interface ITaskRepository : ICrudBaseRepository<TaskModel>
    {
        List<TaskModel> Sync(List<TaskModel> tasks);
        List<TaskModel> SyncRestore(ApplicationUser user, DateTime lastSyncDate);
    }
}
