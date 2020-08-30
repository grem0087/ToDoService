using ToDo.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ToDo.Database.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoContext _ToDoContext;
        private bool _disposed = false;

        public ToDoRepository(ToDoContext ToDoContext)
        {
            _ToDoContext = ToDoContext;
        }

        public async Task AddListAsync(IEnumerable<ToDoEntity> ToDos)
        {
            _ToDoContext.ToDos.AddRange(ToDos);
            await _ToDoContext.SaveChangesAsync();
        }

        public async Task AddAsync(ToDoEntity ToDo)
        {
            _ToDoContext.ToDos.Add(ToDo);
            await _ToDoContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDoEntity>> GetListAsync(Expression<Func<ToDoEntity, bool>> predicate)
        {
            return await _ToDoContext.ToDos.AsNoTracking().Where(predicate).ToArrayAsync();
        }

        public async Task UpdateAsync(ToDoEntity item)
        {
            _ToDoContext.Entry(item).State = EntityState.Modified;
            await _ToDoContext.SaveChangesAsync();
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var item = _ToDoContext.ToDos.Find(id);
            if (item == null)
            {
                return false;
            }

            _ToDoContext.ToDos.Remove(item);
            var result = await _ToDoContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<int> SaveAsync() => await _ToDoContext.SaveChangesAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _ToDoContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
