using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoShieeet
{
    public class TodoBoard
    {
        public string? Name { get; set; }
        public int Id { get; set; }
        public List<Todo>? Todos { get; set; }        
        public TodoBoard(string name, int id)
        {
            Name = name;
            Id = id;
            Todos = new List<Todo>();
        }

    }
    public class Todo
    {
        public int Id { get; }
        private string? _publishDate { get; set; }
        public bool IsDone { get; set; } = false;
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Todo(int id, string publishDate, bool isdone, string title, string description)
        {
            Id = id;
            _publishDate = publishDate;
            IsDone = isdone;
            Title = title;
            Description = description;
        }
    }
}
