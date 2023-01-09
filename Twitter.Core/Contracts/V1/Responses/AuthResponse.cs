using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Core.Contracts.V1.Responses
{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
    }
}
