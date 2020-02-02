using BIL.Data.Entidades;
using BIL.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Logica.Mapper
{
    public class LivroMapper
    {
        public static LivroDto ToDto(Livro livro)
        {
            return new LivroDto()
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Descricao = livro.Descricao,
                Editora = livro.Editora,
                QuantidadePaginas = livro.QuantidadePaginas
            };

        }

        public static Livro ToDb(LivroDto livroDto)
        {
            return new Livro()
            {
                Id = livroDto.Id,
                Titulo = livroDto.Titulo,
                Descricao = livroDto.Descricao,
                Editora = livroDto.Editora,
                QuantidadePaginas = livroDto.QuantidadePaginas
            };
        }
    }
}
