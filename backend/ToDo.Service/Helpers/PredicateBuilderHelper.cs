using ToDo.Database.Entities;
using ToDo.Service.Controllers.Dtos;
using LinqKit;

namespace ToDo.Service.Helpers
{
    public static class PredicateBuilderHelper
    {
        public static ExpressionStarter<ToDoEntity> CreateSearchPredicate(SearchRequestDto request)
        {
            ExpressionStarter<ToDoEntity> predicate = PredicateBuilder.New<ToDoEntity>(true);
            if (request.TableId != null)
            {
                predicate.And(x => x.TableId == request.TableId);
            }

            return predicate;
        }
    }
}
