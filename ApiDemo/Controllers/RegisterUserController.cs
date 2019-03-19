using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDemo.model;
using ApiDemo.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private readonly UserService userService;
        public RegisterUserController(UserService user)
        {
            userService = user;
        }

        [HttpGet]
        public ActionResult<List<User>> getUser()
        {
            return userService.getList();
        }

        [HttpGet("{acount}")]
        public ActionResult<User>FindUser(string acount)
        {
            var user = userService.getUser(acount);
            if(user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public ActionResult<Message> registerUser(User user)
        {
            return userService.AddUser(user);
        }
        [HttpPut("{acount}")]
        public ActionResult<User> update(string acount,User user)
        {
            var check = userService.getUser(acount);
            if(check == null)
            {
                return NotFound();
            }
            return userService.updateUser(acount,user);
        }

        [HttpDelete("{acount}")]
        public ActionResult<Message> delete(string acount)
        {
            return userService.deleteUser(acount);
        }
    }
}