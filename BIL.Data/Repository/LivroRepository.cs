using BIL.Data.Entidades;
using BIL.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Data.Repository
{
    public class LivroRepository : ILivroRepository
    {
        private IBaseRepository<Livro> _baseRepository;
        public LivroRepository(IBaseRepository<Livro> baseRepository)
        {
            _baseRepository = baseRepository;            

        }
        public async Task<Livro> CreateLivroAsync(Livro livro)
        {
            return await _baseRepository.CreateAsync(livro);

        }

        public async Task<Livro> GetLivroAsync(int id)
        {
            return await _baseRepository.GetAsync(id);
        }

        public async Task<IEnumerable<Livro>> GetLivrosAsync()
        {
            return await _baseRepository.GetAsync();
        }

        public Task<IEnumerable<Livro>> GetLivrosRecentesAsync()
        {
            Func<IQueryable<Livro>, IOrderedQueryable<Livro>> orderByFunc = x =>
                 x.OrderByDescending(l => l.Id);
            return _baseRepository.GetAsync(orderBy: orderByFunc);
        }
    }
}
