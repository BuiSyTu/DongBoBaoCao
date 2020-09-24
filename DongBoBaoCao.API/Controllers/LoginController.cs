using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.Services;
using DongBoBaoCao.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DongBoBaoCao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly string _url;
        private readonly string _user;
        private readonly string _pass;
        private readonly string _bearToken;

        private readonly IHttpService _httpService;

        public LoginController(IConfiguration config, IHttpService httpService)
        {
            _config = config;
            _url = _config.GetSection("Login:url").Value;
            _user = _config.GetSection("Login:user").Value;
            _pass = _config.GetSection("Login:pass").Value;
            _bearToken = _config.GetSection("Login:bearToken").Value;

            _httpService = httpService;
        }

        [HttpGet("")]
        public IActionResult GetToken()
        {
            Account account = new Account
            {
                user = _user,
                pass = _pass
            };

            var rs = _httpService.Post(_url, _bearToken, account);
            LoginResult result = JsonConvert.DeserializeObject<LoginResult>(rs);
            string token = result.data.accessToken;
            return Ok(token);
        }
    }
}
