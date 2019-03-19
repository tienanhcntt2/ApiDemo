using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDemo.model;
using ApiDemo.service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserService userService;
        public LoginController(UserService context)
        {
            userService = context;
        }
      
        [HttpPost]
        public ActionResult<JObject> checkLogin(User user)
        {
            return userService.login(user);
        }
    }

}