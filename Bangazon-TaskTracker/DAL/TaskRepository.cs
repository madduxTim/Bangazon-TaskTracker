using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bangazon_TaskTracker.Models;

namespace Bangazon_TaskTracker.DAL
{
    public class TaskRepository
    {
        public TaskContext Context { get; set; }
        public TaskRepository(TaskContext _context)
        {
            Context = _context;
        }
        public TaskRepository()
        {
            Context = new TaskContext();
        }

        public List<Task> GetAll()
        {
            return Context.Tasks.ToList();
        }

        public Task AddTask(Task newTask)
        {
            Context.Tasks.Add(newTask);
            Context.SaveChanges();
            return newTask;
        }

        public Task RemoveTask(Task targetTask)
        {
            Task found_task = Context.Tasks.FirstOrDefault(t => t.Id == targetTask.Id);
            if (found_task != null)
            {
                Context.Tasks.Remove(targetTask);
                Context.SaveChanges();
                return targetTask;
            } else
            {
                return null;
            }
        }

        public Task UpdateTask(Task targetTask)
        {
            Task found_task = Context.Tasks.FirstOrDefault(t => t.Id == targetTask.Id);
            if (found_task != null)
            {
                found_task.Name = targetTask.Name;
                found_task.Status = targetTask.Status;
                found_task.CompletedOn = targetTask.CompletedOn;
                found_task.Description = targetTask.Description;
                Context.SaveChanges();
                return targetTask;
            } else
            {
                return null;
            }

        }
    }
}