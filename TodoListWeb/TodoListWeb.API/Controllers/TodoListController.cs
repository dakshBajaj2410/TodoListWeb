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
        public async Task<IActionResult> GetAllAsync()
        {
            //No Validations Required Since No Data is Required From The Client Side
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
            if(TodoListItemDomain == null)
            {
                return NotFound();  
            }
            var TodoListItemDTO = mapper.Map<Models.DTO.TodoItems>(TodoListItemDomain);
            return Ok(TodoListItemDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AddTodoListRequest addTodoListRequest)
        {
            //Validators Required
            //Validate the Request
            //if (!ValidateCreateAsync(addTodoListRequest))
            //{
            //    return BadRequest(ModelState);
            //}

            var TodoListItemDomain = new Models.Domain.TodoItems();
            TodoListItemDomain.Description = addTodoListRequest.Description;
            TodoListItemDomain = await todoListRepository.CreateAsync(TodoListItemDomain);

            var TodoListItemDTO = mapper.Map<Models.DTO.TodoItems>(TodoListItemDomain);

            return CreatedAtAction(nameof(GetAsync), new { id = TodoListItemDTO.Id }, TodoListItemDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var TodoListItemDomain = await todoListRepository.DeleteAsync(id);
            if (TodoListItemDomain == null)
            {
                return NotFound();
            }
            var TodoListItemDTO = mapper.Map<Models.DTO.TodoItems>(TodoListItemDomain);
            return Ok(TodoListItemDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateTodoListRequest updateTodoListRequest)
        {

            //Validation Request
            //if (!ValidateUpdateAsync(updateTodoListRequest))
            //{
            //    return BadRequest(ModelState);
            //}


            var TodoListItemDomain = new Models.Domain.TodoItems();
            TodoListItemDomain.Description = updateTodoListRequest.Description;
            TodoListItemDomain.Status = updateTodoListRequest.Status;

            TodoListItemDomain = await todoListRepository.UpdateAsync(id, TodoListItemDomain);

            if(TodoListItemDomain == null)
            {
                return NotFound();
            }
            var TodoListItemDTO = mapper.Map<Models.DTO.TodoItems>(TodoListItemDomain);
            return Ok(TodoListItemDTO);
        }

        //Private Methods
        #region Private methods

        private bool ValidateCreateAsync(AddTodoListRequest addTodoListRequest)
        {
            if (addTodoListRequest == null)
            {
                ModelState.AddModelError(nameof(addTodoListRequest), "TodoListItem Data is Required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addTodoListRequest.Description))
            {
                ModelState.AddModelError(nameof(addTodoListRequest.Description), 
                    $"{nameof(addTodoListRequest.Description)} cannot be null or empty or white space");
            }

            if (addTodoListRequest.Description.Length<=2)
            {
                ModelState.AddModelError(nameof(addTodoListRequest.Description),
                    $"{nameof(addTodoListRequest.Description)} should have more than 2 characters");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private bool ValidateUpdateAsync(UpdateTodoListRequest updateTodoListRequest)
        {
            if (updateTodoListRequest == null)
            {
                ModelState.AddModelError(nameof(updateTodoListRequest), "TodoListItem Data is Required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateTodoListRequest.Description))
            {
                ModelState.AddModelError(nameof(updateTodoListRequest.Description),
                    $"{nameof(updateTodoListRequest.Description)} cannot be null or empty or white space");
            }

            if(updateTodoListRequest.Status!="Upcoming"&& updateTodoListRequest.Status != "On Hold" &&
                updateTodoListRequest.Status != "In Progress" && updateTodoListRequest.Status != "Done")
            {
                ModelState.AddModelError(nameof(updateTodoListRequest.Status),
                    $"{nameof(updateTodoListRequest.Status)} Does Not Belong To [Upcoming, On Hold, In Progress, Done]");
            }

            if (updateTodoListRequest.Description.Length <= 2)
            {
                ModelState.AddModelError(nameof(updateTodoListRequest.Description),
                    $"{nameof(updateTodoListRequest.Description)} should have more than 2 characters");
            }


            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
