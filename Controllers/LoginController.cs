using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginMicroService.Models;
using LoginMicroService.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoginMicroService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("/api/login")]
        public async Task<IActionResult> Login(User user)
        {
            return Ok(await _authService.Login(user));
        }
    }
}