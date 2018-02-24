using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevLiftMediWebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DevLiftMediWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private  ITodoItem todo;
        public TodoController(ITodoItem itodo) => todo = itodo;
        // GET: /<controller>/
        [HttpGet]
        public IEnumerable<TodoItem> GetAll() => todo.TodoItems;
        [HttpGet("{id}", Name = "GetTodo")]
        public TodoItem GetById(long id)
        {
            int index = (int)id;
            return todo[index];
        }
        [HttpGet("Get/{id}")]
        public TodoItem Get(int  id) => todo[id];
        [HttpPost]
        public TodoItem Post([FromBody] TodoItem item) => todo.AddItem(new TodoItem() {
            Name = item.Name, IsComplete=item.IsComplete});
        [HttpPut]
        public TodoItem Put([FromBody] TodoItem item) =>
        todo.UpdateItem(item);
        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id,
        [FromBody]JsonPatchDocument<TodoItem> patch)
        {
            TodoItem item = Get(id);
            if (item != null)
            {
                patch.ApplyTo(item);
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public void Delete(int id) => todo.DeleteTodoItem(id);
    }
}
