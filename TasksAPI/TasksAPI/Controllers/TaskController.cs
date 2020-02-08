﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasksAPI.Models;
using TasksAPI.Ropositories.Contracts;

namespace TasksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TaskController(ITaskRepository taskRepository, UserManager<ApplicationUser> userManager)
        {
            _taskRepository = taskRepository;
            _userManager = userManager;
        }

        public ActionResult Sync([FromBody]List<TaskModel> tasks)
        {
            return Ok(_taskRepository.Sync(tasks));
        }

        public ActionResult SyncRestore(DateTime date)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            //HttpContext
            return Ok(_taskRepository.SyncRestore(user, date));
        }
    }
}