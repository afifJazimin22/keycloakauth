using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginMicroService.Models;

namespace LoginMicroService.Services
{
    public interface IAuthService
    {
        Task<Response> Login(User user);
    }
}