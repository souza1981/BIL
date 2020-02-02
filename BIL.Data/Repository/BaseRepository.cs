using BIL.Data.Entidades;
using BIL.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Data.Repository
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : EntidadeBase
    {
        private readonly BILContext _context;

        public BaseRepository(BILContext context)
        {
            _context = context;
        }
        public Task<T> CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }
    }
}
