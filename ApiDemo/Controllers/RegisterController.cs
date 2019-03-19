using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDemo.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserContext _context;
        public RegisterController(UserContext context)
        {
            _context = context;
            if (_context.UserItem.Count() == 0)
            {
                User user = new User();
                //user.Id = 1;
                user.acount = "tienanhit";
                user.password = "123456";
                _context.UserItem.Add(user);
                context.SaveChanges();
            }
               
        }
        [HttpPost]
        public async Task<ActionResult<User>>register(User user)
        {
            User _user = await _context.UserItem.FindAsync(user.acount);
            Message message = new Message();
            if (_user != null)
            {
                message.success = false;
                message.message = "Fail";
            }
            else
            {
                _context.UserItem.Add(user);
                await _context.SaveChangesAsync();
                message.success = true;
                message.message = "Success";
            }
            return Ok(message);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> getUser()
        {
            return await _context.UserItem.ToListAsync();
        }
    }
}