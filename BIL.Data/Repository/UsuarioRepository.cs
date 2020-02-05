using BIL.Data.Entidades;
using BIL.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Data.Repository
{
    class UsuarioRepository : IUsuarioRepository
    {
        private IBaseRepository<Usuario> _baseRepository;

        public UsuarioRepository(IBaseRepository<Usuario> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task<Usuario> CreateUsuarioAsync(Usuario usuario)
        {
            return await _baseRepository.CreateAsync(usuario);
        }

        public async Task<Usuario> GetUsuarioAsync(string id)
        {
            return await _baseRepository.GetAsync(id);
        }
    }
}
