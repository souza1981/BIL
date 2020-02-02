using BIL.Data.Entidades;
using BIL.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Data.Repository
{
    public class LivroRepository : ILivroRepository
    {
        readonly BILContext _context;
        public LivroRepository(BILContext context)
        {
            _context = context;

        }
        public async Task<Livro> CreateLivroAsync(Livro livro)
        {
            await _context.AddAsync(livro);
            await _context.SaveChangesAsync();
            return livro;

        }

        public async Task<Livro> GetLivroAsync(int id)
        {
            return await _context.Livros.FirstOrDefaultAsync(l => l.Id == id);
        }

        public Task<IEnumerable<Livro>> GetLivrosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
