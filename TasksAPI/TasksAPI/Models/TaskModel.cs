using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TasksAPI.Models
{
    public class TaskModel
    {
        [Key]
        public int IdTaskApi { get; set; }
        public int IdTaskApp { get; set; }
        public string Title { get; set; }
        public DateTime DateHour { get; set; }
        public string Local { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool Done { get; set; }
        public bool Removed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
