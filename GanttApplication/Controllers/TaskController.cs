using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GanttApplication.Models;
using System.Data.Entity;

namespace GanttApplication.Controllers
{
    public class TaskController : ApiController
    {
        private GanttContext db = new GanttContext();

        // GET api/Task
        public IEnumerable<TaskDto> Get()
        {
            return db.Tasks
                .OrderBy(t => t.SortOrder)
                .ToList()
                .Join(db.Users, t => t.OwnerId, u => u.Id, (t, u) => new Task()
                {
                    Id = t.Id,
                    Text = t.Text,
                    OwnerId = t.OwnerId,
                    Owner = u.Name,
                    StartDate = t.StartDate,
                    Duration = t.Duration,
                    Progress = t.Progress,
                    ParentId = t.ParentId
                }).Select(x =>(TaskDto)x);
        }

        // GET api/Task/5
        [System.Web.Http.HttpGet]
        public TaskDto Get(int id)
        {
            return (TaskDto)db
                .Tasks
                .Find(id);
        }

        // PUT api/Task/5
        [System.Web.Http.HttpPut]
        public IHttpActionResult EditTask(int id, TaskDto taskDto)
        {
            var updatedTask = (Task)taskDto;
            updatedTask.Id = id;

            if (!string.IsNullOrEmpty(taskDto.target))
            {
                // reordering occurred
                this._UpdateOrders(updatedTask, taskDto.target);
            }

            db.Entry(updatedTask).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(new
            {
                action = "updated"
            });
        }
        private void _UpdateOrders(Task updatedTask, string orderTarget)
        {
            int adjacentTaskId;
            var nextSibling = false;

            var targetId = orderTarget;

            // adjacent task id is sent either as '{id}' or as 'next:{id}' depending 
            // on whether it's the next or the previous sibling
            if (targetId.StartsWith("next:"))
            {
                targetId = targetId.Replace("next:", "");
                nextSibling = true;
            }

            if (!int.TryParse(targetId, out adjacentTaskId))
            {
                return;
            }

            var adjacentTask = db.Tasks.Find(adjacentTaskId);
            var startOrder = adjacentTask.SortOrder;

            if (nextSibling)
                startOrder++;

            updatedTask.SortOrder = startOrder;

            var updateOrders = db.Tasks
             .Where(t => t.Id != updatedTask.Id)
             .Where(t => t.SortOrder >= startOrder)
             .OrderBy(t => t.SortOrder);

            var taskList = updateOrders.ToList();

            taskList.ForEach(t => t.SortOrder++);
        }

        // POST api/Task
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateTask(TaskDto taskDto)
        {
            var newTask = (Task)taskDto;

            newTask.SortOrder = db.Tasks.Max(t => t.SortOrder) + 1;

            db.Tasks.Add(newTask);
            db.SaveChanges();

            return Ok(new
            {
                tid = newTask.Id,
                action = "inserted"
            });
        }

        // DELETE api/Task/5
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteTask(int id)
        {
            var task = db.Tasks.Find(id);
            if (task != null)
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
            }

            return Ok(new
            {
                action = "deleted"
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
