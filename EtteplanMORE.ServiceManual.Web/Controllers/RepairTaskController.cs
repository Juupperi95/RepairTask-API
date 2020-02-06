using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EtteplanMORE.ServiceManual.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RepairTaskController : Controller
    {
        private readonly IRepairTaskService _repairTaskService;

        public RepairTaskController(IRepairTaskService repairTaskService)
        {
            _repairTaskService = repairTaskService;
        }

        /// <summary>
        ///     Get all current repairtasks
        /// </summary>
        /// <returns> Returns all tasks in JSON-format </returns>
        [HttpGet]
        public async Task<IEnumerable<RepairTaskDto>> GetAll()
        {
            return (await _repairTaskService.GetAll())
                .Select(rt =>
                    new RepairTaskDto
                    {
                     TaskId = rt.TaskId,
                     DeviceId = rt.DeviceId,
                     DateAdded = rt.DateAdded,
                     Description = rt.Description,
                     Criticality = rt.Criticality,
                     Completed = rt.Completed
                    }
                );
        }
        /// <summary>
        /// Get repairtask that has taskId
        /// </summary>
        /// <param name="taskId">requested taskID </param>
        /// <returns>Returns correct repairtask as JSON</returns>
        [HttpGet("/api/tasks/{taskId}")]
        public async Task<IActionResult> GetByTaskId(int taskId)
        {
            // Check taskID exists
            var rt = await _repairTaskService.GetByTaskId(taskId);
            if (rt == null)
            {
                return BadRequest("TaskId doesn't exist");
            }
            return Ok(new RepairTaskDto
            {
                TaskId = rt.TaskId,
                DeviceId = rt.DeviceId,
                DateAdded = rt.DateAdded,
                Description = rt.Description,
                Criticality = rt.Criticality,
                Completed = rt.Completed
            });

        }

        /// <summary>
        ///     Get all repairtasks that belong to deviceId
        /// </summary>
        /// /// <param name="deviceId"> requested device id </param>
        /// <returns>Returns all repairtasks for given deviceId as JSON</returns>
        [HttpGet("{deviceId}")]
        public async Task<IActionResult> GetByDeviceId(int deviceId)
        {
            var queryResults = await _repairTaskService.GetByDeviceId(deviceId);
            if(queryResults.Count() == 0)
            {
                return NotFound();
            }
            return Ok(queryResults.Select(rt =>
                new RepairTaskDto
                {
                    TaskId = rt.TaskId,
                    DeviceId = rt.DeviceId,
                    DateAdded = rt.DateAdded,
                    Description = rt.Description,
                    Criticality = rt.Criticality,
                    Completed = rt.Completed
                }
            ));
        }
        /// <summary>
        /// Post new repair task
        /// </summary>
        /// <param name="rt"> Values for new repairtask</param>
        /// <returns> Returns newly created task as JSON </returns>
        [HttpPost]
        public async Task<IActionResult> PostNewTask(RepairTaskPost rt)
        {
            // Check that task criticality is correct
            if(rt.Criticality < 1 || rt.Criticality > 3)
            {
                return StatusCode(400, "Bad criticality value");
            }
            // Check completed field correctness
            if(rt.Completed < 0 || rt.Completed > 1)
            {
                return StatusCode(400, "Bad completed value");
            }
            var queryresult = await _repairTaskService.AddTask(rt);
            return Ok(new RepairTaskDto
            {
                TaskId = queryresult.TaskId,
                DeviceId = queryresult.DeviceId,
                DateAdded = queryresult.DateAdded,
                Description = queryresult.Description,
                Criticality = queryresult.Criticality,
                Completed = queryresult.Completed
            });
        }
        /// <summary>
        /// Updates old repairtask
        /// </summary>
        /// <param name="rt"> New values for repairtask</param>
        /// <param name="taskId">ID of the task to be updated</param>
        /// <returns>Returns updated repairtask as JSON </returns>
        [HttpPut("{taskId}")]

        public async Task<IActionResult> UpdateNewTask(RepairTaskPost rt, int taskId)
        {
            // Check that task exists
            if(await _repairTaskService.GetByTaskId(taskId) == null)
            {
                return BadRequest("task doesnt exist");
            }
            
            // Check that task criticality is correct
            if (rt.Criticality < 1 || rt.Criticality > 3)
            {
                return StatusCode(400, "Bad criticality value");
            }
            // Check completed field correctness
            if (rt.Completed < 0 || rt.Completed > 1)
            {
                return StatusCode(400, "Bad completed value");
            }
            var queryresult = await _repairTaskService.UpdateTask(rt, taskId);
            return Ok(new RepairTaskDto
            {
                TaskId = queryresult.TaskId,
                DeviceId = queryresult.DeviceId,
                DateAdded = queryresult.DateAdded,
                Description = queryresult.Description,
                Criticality = queryresult.Criticality,
                Completed = queryresult.Completed
            });
        }
        /// <summary>
        /// Deletes task with given taskID if one exists
        /// </summary>
        /// <param name="taskId"> TaskId of task to be deleted</param>
        /// <returns> On success returns empty body</returns>
        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            // Check that task with taskId exists
            if (await _repairTaskService.GetByTaskId(taskId) == null)
            {
                return NotFound();
            }
            var rt = await _repairTaskService.DeleteTaskById(taskId);
            return Ok();
        }
    }
}
