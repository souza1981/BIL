using BIL.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Data.Repository.Interface
{
    interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioAsync(string id);
        Task<Usuario> CreateUsuarioAsync(Usuario usuario);
    }
}
