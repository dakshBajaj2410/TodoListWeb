using Microsoft.EntityFrameworkCore;
using TodoListWeb.API.Models.Domain;

namespace TodoListWeb.API.Data
{
    public class TodoListDBContext: DbContext
    {
        public TodoListDBContext(DbContextOptions<TodoListDBContext> options): base(options)
        {
           
        }

        public DbSet<TodoItems> TodoItems { get; set; }
    }
}
