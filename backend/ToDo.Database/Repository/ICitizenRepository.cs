using ToDo.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ToDo.Database.Repository
{
    public interface IToDoRepository : IDisposable
    {
        Task AddAsync(ToDoEntity ToDo);

        Task AddListAsync(IEnumerable<ToDoEntity> ToDos);

        Task<IEnumerable<ToDoEntity>> GetListAsync(Expression<Func<ToDoEntity, bool>> predicate);

        Task UpdateAsync(ToDoEntity item);

        Task<bool> RemoveAsync(int id);

        Task<int> SaveAsync();
    }
}
