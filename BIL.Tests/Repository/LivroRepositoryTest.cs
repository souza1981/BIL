using BIL.Data;
using BIL.Data.Entidades;
using BIL.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BIL.Tests.Repository
{
    public class LivroRepositoryTest
    {
        [Fact]
        public async Task Deve_Criar_Usuario()
        {
            //arrange
            var options = new DbContextOptionsBuilder<BILContext>()
                .UseInMemoryDatabase(databaseName: "Create_Livro")
                .Options;

            var novoLivro = new Livro()
            {
                Titulo = "Titulo do Livro",
                Descricao = "Descricao do Livro",
                Editora = null,
                QuantidadePaginas = 20
            };

            Livro livroCriado = null;
            //act
            
            using (var context = new BILContext(options))
            {
                var livroRepository = new LivroRepository(context);
                livroCriado = await livroRepository.CreateLivroAsync(novoLivro);
            }

            //assert
            Assert.NotNull(livroCriado);

            using (var context = new BILContext(options))
            {
                var livroCount = await context.Livros.CountAsync();
                Assert.Equal(1, livroCount);
            }
        }

        [Fact]
        public async Task Deve_Consultar_Usuario()
        {
            //arrange

            var options = new DbContextOptionsBuilder<BILContext>()
                .UseInMemoryDatabase(databaseName: "Create_Livro")
                .Options;

            var livroASerBuscado = new Livro()
            {
                Titulo = "Titulo do Livro",
                Descricao = "Descricao do Livro",
                Editora = null,
                QuantidadePaginas = 20
            };

            using (var context = new BILContext(options))
            {
                context.Livros.Add(livroASerBuscado);
                context.SaveChanges();
            }

            //act
            Livro livroBuscado = null;
            using (var context = new BILContext(options))
            {
                var livroRepository = new LivroRepository(context);
                livroBuscado = await livroRepository.GetLivroAsync(livroASerBuscado.Id);
                Assert.NotNull(livroBuscado);
            }

            //assert
            Assert.NotNull(livroBuscado);
            Assert.Equal(livroBuscado.Id, livroASerBuscado.Id);
        }

        

    }
}
