using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace ToDo.Service.Helpers
{
    public static class BadRequestMessageHelper
    {
        public static IEnumerable<object> BadRequestFormat(IList<ValidationFailure> errors) => errors.Select(x => new { field = x.PropertyName, message = x.ErrorMessage });

        public static object BadRequestFormat(IList<ValidationFailure> errors, string name) => new
        {
            ToDoName = name,
            errors = errors.Select(x => new { field = x.PropertyName, message = x.ErrorMessage })
        };
    }
}
