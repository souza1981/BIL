using BIL.Data.Repository.Interface;
using BIL.Logica.Manager.Interface;
using BIL.Logica.Mapper;
using BIL.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Logica.Manager
{
    public class LivroManager : ILivroManager
    {
        private ILivroRepository _livroRepository;
        public LivroManager(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }
        public async Task<LivroDto> CreateLivroAsync(LivroDto livro)
        {
            var livroDB = LivroMapper.ToDb(livro);
            var livroCriado = await _livroRepository.CreateLivroAsync(livroDB);
            return LivroMapper.ToDto(livroCriado);
        }

        public async Task<LivroDto> GetLivroAsync(int id)
        {
            var livroDB = await _livroRepository.GetLivroAsync(id);
            return LivroMapper.ToDto(livroDB);
        }

        public async Task<IEnumerable<LivroDto>> GetLivrosAsync()
        {
            var livrosDB = await _livroRepository.GetLivrosAsync();
            var livrosDto = new List<LivroDto>();

            foreach (var livroDB in livrosDB)
            {
                livrosDto.Add(LivroMapper.ToDto(livroDB));
            }

            return livrosDto;
            
            
        }
    }
}
