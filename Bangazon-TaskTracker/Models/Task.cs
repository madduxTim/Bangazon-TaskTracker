using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bangazon_TaskTracker.Models
{
    public class Task
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
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