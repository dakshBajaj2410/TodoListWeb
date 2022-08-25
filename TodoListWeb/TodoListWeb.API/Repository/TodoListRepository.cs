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

        public async Task<TodoItems> CreateAsync(TodoItems todoItem)
        {
            todoItem.Id = Guid.NewGuid();
            todoItem.Status = "Upcoming";
            todoItem.IsChecked = true;
            todoItem.CreationDate = DateTime.Now;
            await todoListDBContext.AddAsync(todoItem);
            await todoListDBContext.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItems> DeleteAsync(Guid id)
        {
            var todoListDomain = await todoListDBContext.TodoItems.FindAsync(id);
            if (todoListDomain == null)
            {
                return null;
            }
            todoListDBContext.Remove(todoListDomain);
            await todoListDBContext.SaveChangesAsync();
            return todoListDomain;
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

        public async Task<TodoItems> UpdateAsync(Guid id, TodoItems todoItem)
        {
            var ExistingTodoListItem = await todoListDBContext.TodoItems.FindAsync(id);
            if (ExistingTodoListItem == null)
            {
                return null;
            }

            ExistingTodoListItem.Description = todoItem.Description;
            ExistingTodoListItem.Status = todoItem.Status;
            await todoListDBContext.SaveChangesAsync();
            return ExistingTodoListItem;
        }
    }
}
