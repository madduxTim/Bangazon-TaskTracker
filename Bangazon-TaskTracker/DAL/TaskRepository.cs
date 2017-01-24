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
    }
}