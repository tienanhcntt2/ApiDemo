using ApiDemo.config;
using ApiDemo.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

 

        public TodoController(TodoContext context)
        {
            _context = context;
            if(_context.TodoItems.Count() == 0)
            {
                TodoItem todoItem = null;
                for(var i = 0; i <= 10; i++)
                {
                    todoItem = new TodoItem();
                    todoItem.Id = i + 1;
                    todoItem.Name = "anh " + i;
                    if(i%2 == 0)
                    {
                        todoItem.IsComplete = true;
                    }
                    else
                    {
                        todoItem.IsComplete = false;
                    }

                    _context.TodoItems.Add(todoItem);
                }
                
                _context.SaveChanges();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
        [HttpPost]
        public async Task<ActionResult<TodoItem>>PostTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult>PutTodoItem (long id,TodoItem item)
        {
            if(id != item.Id)
            {
                var resultt = new { Success = "false", Message = "Update Fiald" };
                return Ok(resultt);
            }
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var result = new { Success = "true", Message = "Update suscees" };
            //return Json(JsonResponseFactory.SuccessResponse(result));
            return Ok(result);
            //return NoContent();
           

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> deleteTodoItem(long id)
        {
            Message message = new Message();
            var item = await _context.TodoItems.FindAsync(id);
            if(item == null)
            {
                message.success = false;
                message.message = "Faild";
               
            }
            else
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();
                message.success = true;
                message.message = "Success";
            }
            
            return Ok(message);
        }
    }

    
}
