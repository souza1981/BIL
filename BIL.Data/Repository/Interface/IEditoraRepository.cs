using BIL.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Data.Repository.Interface
{
    interface IEditoraRepository
    {
        Task<Editora> CreateEditoraAsync(Editora editora);
        Task<Editora> GetEditoraAsync(int id);
        Task<IEnumerable<Editora>> GetEditorasAsync();
    }
}
