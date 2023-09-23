using System;
using System.ComponentModel.DataAnnotations;

namespace PilotTask_MVC.Models.Domain
{
    public class TaskDetail
    {
        [Key]
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime StartTime { get; set; }
        public int Status { get; set; }
    }
}
