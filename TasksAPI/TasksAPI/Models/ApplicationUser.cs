using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TasksAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        //
        // Resumo:
        //     Gets or sets the user full name for this user.
        [ProtectedPersonalData]
        public virtual string UserFullName { get; set; }

        [ForeignKey("UserId")]
        public ICollection<TaskModel> Tasks { get; }
    }
}
