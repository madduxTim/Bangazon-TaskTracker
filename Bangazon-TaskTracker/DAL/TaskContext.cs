using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bangazon_TaskTracker.Models;
using System.Data.Entity;

namespace Bangazon_TaskTracker.DAL
{
    public class TaskContext : DbContext
    {
        public virtual DbSet<Task> Tasks { get; set; }
    }
}