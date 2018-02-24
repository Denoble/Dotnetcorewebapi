using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevLiftMediWebAPI.Models
{
    public interface ITodoItem
    {
        IEnumerable<TodoItem> TodoItems { get; }
        TodoItem this[int id] { get; }
        TodoItem AddItem(TodoItem item);
        TodoItem UpdateItem(TodoItem item);
        void DeleteTodoItem(int id);
    }
}
