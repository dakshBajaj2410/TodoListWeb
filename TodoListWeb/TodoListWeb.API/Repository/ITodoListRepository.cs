using TodoListWeb.API.Models.Domain;

namespace TodoListWeb.API.Repository
{
    public interface ITodoListRepository
    {
        Task<IEnumerable<TodoItems>> GetAllAsync();

        Task<TodoItems> GetAsync(Guid id);
        Task<TodoItems> CreateAsync(TodoItems todoItem);
        Task<TodoItems> DeleteAsync(Guid id);
        Task<TodoItems> UpdateAsync(Guid id, TodoItems todoItem);


    };
}
