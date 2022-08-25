using FluentValidation;

namespace TodoListWeb.API.Validators
{
    public class AddTodoListValidators : AbstractValidator<Models.DTO.AddTodoListRequest>
    {
        public AddTodoListValidators()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Description.Length).GreaterThan(2);

        }
    }
}
