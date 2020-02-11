using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserRepository userRepository, SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]UserDto userDto)
        {
            ModelState.Remove(nameof(userDto.Name));
            ModelState.Remove(nameof(userDto.RepeatPassword));

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var user = _userRepository.GetEntity(userDto.Email, userDto.Password);

            if (user == null)
                return NotFound("Usuário não localizado!");

            _signInManager.SignInAsync(user, false);

            return Ok();
        }

        [HttpPost("")]
        public ActionResult Create([FromBody]UserDto userDto)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var user = new ApplicationUser
            {
                FullName = userDto.Name,
                UserName = userDto.Email,
                Email = userDto.Email
            };

            var result = _userManager.CreateAsync(user, userDto.Password).Result;

            if (result.Succeeded)
                return Ok(user);

            var errors = new List<string>();

            foreach (var erro in result.Errors)
            {
                errors.Add(erro.Description);
            }

            return UnprocessableEntity(errors);

        }
    }
}