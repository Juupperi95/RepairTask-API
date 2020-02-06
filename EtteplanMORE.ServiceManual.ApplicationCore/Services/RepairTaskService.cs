using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EtteplanMORE.ServiceManual.ApplicationCore.DBContexts;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;


namespace EtteplanMORE.ServiceManual.ApplicationCore.Services
{
    public class RepairTaskService : IRepairTaskService
    {
        private readonly DatabaseContext _dbContext;
        public RepairTaskService(DatabaseContext DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<IEnumerable<RepairTask>> GetAll()
        {
            var repairTasks = await Task.FromResult(_dbContext.RepairTasks
                .OrderByDescending(rt => rt.Criticality)
                .ThenByDescending(rt => rt.DateAdded)
                .ToList());

            return repairTasks;
        }

        public async Task<RepairTask> GetByTaskId(int taskId)
        {
            var repairTask = await Task.FromResult(_dbContext.RepairTasks
                .FirstOrDefault(c => c.TaskId == taskId));

            return repairTask;
        }

        public async Task<IEnumerable<RepairTask>> GetByDeviceId(int deviceId)
        {
            var repairTasks = await Task.FromResult(_dbContext.RepairTasks
                .Where(rt => rt.DeviceId == deviceId)
                .OrderByDescending(rt => rt.Criticality)
                .ThenByDescending(rt => rt.DateAdded)
                .ToList());

            return repairTasks;
        }

        public async Task<RepairTask> AddTask(RepairTaskPost rt)
        {
            // Ignore datetime-input from user and use system time instead
            // so format is always same
            RepairTask repairTask = new RepairTask
            {
                DeviceId = rt.DeviceId,
                DateAdded = DateTime.Now,
                Description = rt.Description,
                Criticality = rt.Criticality,
                Completed = rt.Completed
            };

            await _dbContext.RepairTasks.AddAsync(repairTask);
            await _dbContext.SaveChangesAsync();
            return repairTask;
        }

        public async Task<RepairTask> DeleteTaskById(int taskId)
        {
            // First find entity that has the correct taskId
            RepairTask repairTask = _dbContext.RepairTasks
                .Where(rt => rt.TaskId == taskId)
                .First();

            _dbContext.RepairTasks.Remove(repairTask);
            await _dbContext.SaveChangesAsync();
            return repairTask;
        }
        public async Task<RepairTask> UpdateTask(RepairTaskPost task, int taskId)
        {
            var oldTask = _dbContext.RepairTasks.Where(rt => rt.TaskId == taskId).First();
            if(task.DeviceId.ToString() != "")
            {
                oldTask.DeviceId = task.DeviceId;
            }
            oldTask.DateAdded = DateTime.Now;  // Update also timestamp
            oldTask.Description = task.Description;
            oldTask.Criticality = task.Criticality;
            oldTask.Completed = task.Completed;
            await _dbContext.SaveChangesAsync();
            return oldTask;
        }
    }
}
