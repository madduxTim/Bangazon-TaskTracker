using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bangazon_TaskTracker.Models;
using static Bangazon_TaskTracker.Models.Task;

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

        public void RemoveTask(int targetTaskId)
        {
            Task found_task = Context.Tasks.FirstOrDefault(t => t.Id == targetTaskId);
            Context.Tasks.Remove(found_task);
            Context.SaveChanges();
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

        public List<Task> GetTasksByStatus(int statusNum)
        {
            List<Task> listToReturn = new List<Task>();
            if (statusNum == 1)
            {
                foreach (Task task in Context.Tasks.Where(t => t.Status == eStatus.ToDo))
                {
                    listToReturn.Add(task);
                }
            } else if (statusNum == 2)
            {
                foreach (Task task in Context.Tasks.Where(t => t.Status == eStatus.InProgress))
                {
                    listToReturn.Add(task);
                }
            } else if (statusNum == 3)
            {
                foreach (Task task in Context.Tasks.Where(t => t.Status == eStatus.Complete))
                {
                    listToReturn.Add(task);
                }
            }
            //return Context.Tasks.ToList();
            return listToReturn;
        }
    }
}