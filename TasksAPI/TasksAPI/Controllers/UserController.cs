using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

            //_signInManager.SignInAsync(user, false); Nâo é mais necessário fazer login dessa forma porque gera cookie.

            return Ok(BuildToken(user));
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

        public object BuildToken(ApplicationUser user)
        {
            var claims = new[] { 
            new Claim(JwtRegisteredClaimNames.Aud, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("chave-api-jwt-mytasks-secret-login"));
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expMin = DateTime.Now.AddMinutes(10);
            //var expHour = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expMin,
                signingCredentials: sign
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new { token = tokenString, expiration = expMin, refreshToken = "", expirationRefreshToken = expMin};
        }
    }
}