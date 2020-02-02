using BIL.Data.Entidades;
using BIL.Logica.Mapper;
using BIL.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BIL.Tests.Mapper
{
    public class LivroMapperTest
    {
        [Fact]
        public void Deve_Mapear_Livro_Para_LivroDto()
        {
            //arrange
            var livro = new Livro()
            {
                Id = 1,
                Titulo = "Titulo do livro",
                Descricao = "Descrição do livro",
                Editora = null,
                QuantidadePaginas = 20
            };

            //act
            var livroDto = LivroMapper.ToDto(livro);

            //assert
            Assert.NotNull(livroDto);
            Assert.Equal(livro.Id, livroDto.Id);
            Assert.Equal(livro.Titulo, livroDto.Titulo);
            Assert.Equal(livro.Descricao, livroDto.Descricao);
            Assert.Equal(livro.Editora, livroDto.Editora);
            Assert.Equal(livro.QuantidadePaginas, livroDto.QuantidadePaginas);

        }

        [Fact]
        public void Deve_Mapear_LivroDto_Para_Livro()
        {
            //arrange
            var livroDto = new LivroDto()
            {
                Id = 1,
                Titulo = "Titulo do Livro",
                Descricao = "Descricao do Livro",
                Editora = null,
                QuantidadePaginas = 20
            };

            //act
            var livro = LivroMapper.ToDb(livroDto);

            //Assert
            Assert.NotNull(livro);
            Assert.Equal(livro.Id, livroDto.Id);
            Assert.Equal(livro.Titulo, livroDto.Titulo);
            Assert.Equal(livro.Descricao, livroDto.Descricao);
            Assert.Equal(livro.Editora, livroDto.Editora);
            Assert.Equal(livro.QuantidadePaginas, livroDto.QuantidadePaginas);

        }
    }
}
