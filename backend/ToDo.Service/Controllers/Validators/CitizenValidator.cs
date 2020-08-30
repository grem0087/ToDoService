using ToDo.Service.Controllers.Dtos;
using FluentValidation;

namespace ToDo.Service.Controllers.Validators
{
    public class ToDoValidator : AbstractValidator<CreateToDoDto>
    {
        public ToDoValidator()
        {
            RuleFor(ToDo => ToDo.Title).NotEmpty().WithMessage("Не указано имя гражданина");
            RuleFor(ToDo => ToDo.Description).NotEmpty().WithMessage("Не указан ИНН");
        }
    }
}
