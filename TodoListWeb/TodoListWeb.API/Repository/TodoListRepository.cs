using Microsoft.EntityFrameworkCore;
using TodoListWeb.API.Data;
using TodoListWeb.API.Models.Domain;

namespace TodoListWeb.API.Repository
{
    public class TodoListRepository : ITodoListRepository
    {
        private readonly TodoListDBContext todoListDBContext;

        public TodoListRepository(TodoListDBContext todoListDBContext)
        {
            this.todoListDBContext = todoListDBContext;
        }

        public async Task<TodoItems> createAsync(TodoItems todoItem)
        {
            todoItem.Id = Guid.NewGuid();
            await todoListDBContext.AddAsync(todoItem);
            await todoListDBContext.SaveChangesAsync();
            return todoItem;
        }

        public async Task<IEnumerable<TodoItems>> GetAllAsync()
        {
            return await todoListDBContext.TodoItems.ToListAsync();
        }

        public async Task<TodoItems> GetAsync(Guid id)
        {
            var TodoListItem = await todoListDBContext.TodoItems.FindAsync(id);
            if(TodoListItem == null)
            {
                return null;
            }
            return TodoListItem;
        }


    }
}
