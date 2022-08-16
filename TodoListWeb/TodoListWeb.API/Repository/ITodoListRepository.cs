using TodoListWeb.API.Models.Domain;

namespace TodoListWeb.API.Repository
{
    public interface ITodoListRepository
    {
        Task<IEnumerable<TodoItems>> GetAllAsync();

        Task<TodoItems> GetAsync(Guid id);
        Task<TodoItems> createAsync(TodoItems todoItem);
        //Task<TodoItems> deleteAsync(Guid id);
        //Task<TodoItems> updateAsync(Guid id, TodoItems todoItem)


    }
}
