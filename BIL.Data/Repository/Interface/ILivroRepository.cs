using BIL.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Data.Repository.Interface
{
    public interface ILivroRepository
    {
        Task<Livro> CreateLivroAsync(Livro livro);
        Task<Livro> GetLivroAsync(int id);
        Task<IEnumerable<Livro>> GetLivrosAsync();

        Task<IEnumerable<Livro>> GetLivrosRecentesAsync();

    }
}
