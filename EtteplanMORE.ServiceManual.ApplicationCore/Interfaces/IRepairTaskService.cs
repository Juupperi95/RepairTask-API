using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Interfaces
{
    public interface IRepairTaskService
    {
        Task<IEnumerable<RepairTask>> GetAll();
        Task<IEnumerable<RepairTask>> GetByDeviceId(int deviceId);
        Task<RepairTask> GetByTaskId(int taskId);
        Task<RepairTask> AddTask(RepairTaskPost task);
        Task<RepairTask> UpdateTask(RepairTaskPost task, int taskId); 
        Task<RepairTask> DeleteTaskById(int taskId);
    }
}
