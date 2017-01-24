using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bangazon_TaskTracker.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public eStatus Status { get; set; }
        public DateTime CompletedOn { get; set; }
        public enum eStatus
        {
            ToDo,
            InProgress,
            Complete
        }
    }
}