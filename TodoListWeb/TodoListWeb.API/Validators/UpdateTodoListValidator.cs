using FluentValidation;

namespace TodoListWeb.API.Validators
{
    public class UpdateTodoListValidator: AbstractValidator<Models.DTO.UpdateTodoListRequest>
    {
        public UpdateTodoListValidator()
        {
            var status = new List<string>() { "Upcoming", "Done", "In Progress", "On Hold" };

            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Description.Length).GreaterThan(2);
            RuleFor(x => x.Status)
               .Must(x=>status.Contains(x));
        }
    }
}
    