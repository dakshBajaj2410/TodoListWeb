using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoListWeb.API.Models.DTO;
using TodoListWeb.API.Repository;

namespace TodoListWeb.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoListController : Controller
    {
        private readonly ITodoListRepository todoListRepository;
        private readonly IMapper mapper;

        public TodoListController(ITodoListRepository todoListRepository, IMapper mapper)
        {
            this.todoListRepository = todoListRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> getAllAsync()
        {
            var TodoListItemsDomain = await todoListRepository.GetAllAsync();

            var TodoListItemsDTO = mapper.Map<List<Models.DTO.TodoItems>>(TodoListItemsDomain);

            return Ok(TodoListItemsDTO);

        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetAsync")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var TodoListItemDomain = await todoListRepository.GetAsync(id);
            var TodoListItemDTO = mapper.Map<Models.DTO.TodoItems>(TodoListItemDomain);
            return Ok(TodoListItemDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AddTodoListRequest addTodoListRequest)
        {
            var TodoListItem = new Models.Domain.TodoItems();

            TodoListItem.Description = addTodoListRequest.Description;
            TodoListItem.Status = addTodoListRequest.Status;

            TodoListItem = await todoListRepository.createAsync(TodoListItem);
            return CreatedAtAction(nameof(GetAsync), new { id = TodoListItem.Id }, TodoListItem);
        }


    }
}
