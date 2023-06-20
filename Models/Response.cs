using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginMicroService.Models
{
    public class Response
    {
        public string Message { get; set; }
        public bool Success { get; set; } = true;
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string ExpiresIn { get; set; }
        public string RefreshExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}