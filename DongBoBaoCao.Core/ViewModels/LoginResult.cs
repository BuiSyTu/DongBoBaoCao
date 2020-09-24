using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DongBoBaoCao.Core.ViewModels
{
    public class LoginResult
    {
        public Token data { get; set; }
        public ErrorResult error { get; set; }
    }

    public class Token
    {
        public string accessToken { get; set; }
        public string expiresIn { get; set; }
        public string rerefreshToken { get; set; }
        public string tokenType { get; set; }
    }
}
