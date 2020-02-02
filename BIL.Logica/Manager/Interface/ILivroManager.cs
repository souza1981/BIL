using BIL.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Logica.Manager.Interface
{
    public interface ILivroManager
    {
        Task<LivroDto> CreateLivroAsync(LivroDto livro);
        Task<LivroDto> GetLivroAsync(int id);
        Task<IEnumerable<LivroDto>> GetLivrosAsync();
    }
}
