using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevLiftMediWebAPI.Models
{
    public class MemoryRepository : ITodoItem
    {
        private Dictionary<int, TodoItem> items;
        
        public MemoryRepository()
        {
            items = new Dictionary<int, TodoItem>();
            new List<TodoItem>
            {
                new TodoItem{ Name= "DevLiftMedia project", IsComplete=false},
                new TodoItem{ Name= "DevLiftMedia mobile project", IsComplete=false},
                new TodoItem{ Name= "Travel to London", IsComplete=false},
                new TodoItem{ Name= "Article on men's health", IsComplete=false},

            }.ForEach(r => AddItem(r));
        }
        public TodoItem this[int id] => items.ContainsKey(id) ? items[id] : null;
        public IEnumerable<TodoItem> TodoItems => items.Values;
        public TodoItem AddItem(TodoItem item)
        {
            if(item.Id == 0)
            {
                int key = items.Count;
                while (items.ContainsKey(key)) { key++; };
                item.Id = key;
            }
            int index = (int)item.Id;
            items[index] = item;
           
            return item;
        }
        public void DeleteReservation(int id) => items.Remove(id);
        public TodoItem UpdateItem(TodoItem item)
        => AddItem(item);
        public void DeleteTodoItem(int id) => items.Remove(id);
        public TodoItem UpdateTodoItem(TodoItem item)
        => AddItem(item);
    }
}
