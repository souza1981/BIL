using BIL.Data;
using BIL.Data.Entidades;
using BIL.Data.Repository;
using BIL.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace BIL.Tests.Repository
{
    public class BaseRepositoryTest : RepositoryTest
    {
        [Fact]
        public async Task Deve_Criar_Livro()
        {
            //arrange
            var options = BuildInMemoryDatabase("Criar_Livro");
            Livro novoLivro = CriarNovoLivro();

            Livro livroCriado = null;
            //act

            using (var context = new BILContext(options))
            {
                var baseRepository = new BaseRepository<Livro>(context);
                livroCriado = await baseRepository.CreateAsync(novoLivro);
            }

            //assert
            Assert.NotNull(livroCriado);

            using (var context = new BILContext(options))
            {
                var livroCount = await context.Livros.CountAsync();
                Assert.Equal(1, livroCount);
            }
        }

        private static Livro CriarNovoLivro()
        {
            return new Livro()
            {
                Titulo = "Titulo do Livro",
                Descricao = "Descricao do Livro",
                Editora = null,
                QuantidadePaginas = 20
            };
        }

        [Fact]
        public async Task Deve_Consultar_Um_Livro()
        {
            //arrange

            var options = BuildInMemoryDatabase("Consultar_Um_Livro");

            var livroASerBuscado = CriarNovoLivro();

            using (var context = new BILContext(options))
            {
                context.Livros.Add(livroASerBuscado);
                context.SaveChanges();
            }

            //act
            Livro livroBuscado = null;
            using (var context = new BILContext(options))
            {
                var baseRepository = new BaseRepository<Livro>(context);
                livroBuscado = await baseRepository.GetAsync(livroASerBuscado.Id);
                Assert.NotNull(livroBuscado);
            }

            //assert
            Assert.NotNull(livroBuscado);
            Assert.Equal(livroBuscado.Id, livroASerBuscado.Id);
        }

        [Fact]

        public async Task Deve_Consultar_Livros_OrdenandosPorIdDescentente()
        {
            var options = BuildInMemoryDatabase("Consultar_Livros_OrdenandosPorIdDescentente");

            var livro1 = CriarNovoLivro();
            var livro2 = CriarNovoLivro();

            using (var context = new BILContext(options))
            {
                context.Livros.Add(livro1);
                context.Livros.Add(livro2);
                context.SaveChanges();
            }

            IEnumerable<Livro> livros = null;
            Func<IQueryable<Livro>, IOrderedQueryable<Livro>> orderByFunc = x => x.OrderByDescending(q => q.Id);
            using (var context = new BILContext(options))
            {
                var baseRepo = new BaseRepository<Livro>(context);
                livros = await baseRepo.GetAsync(orderBy: orderByFunc);
            }

            Assert.NotNull(livros);
            Assert.Equal(2, livros.Count());
            Assert.Equal(2, livros.First().Id);
            Assert.Equal(1, livros.Skip(1).First().Id);
        }

        [Fact]
        public async Task Deve_Consultar_Todos_Os_Livros()
        {
            var options = BuildInMemoryDatabase("Consultar_Todos_Os_Livros");

            var livro1 = CriarNovoLivro();
            var livro2 = CriarNovoLivro();

            using (var context = new BILContext(options))
            {
                context.Livros.Add(livro1);
                context.Livros.Add(livro2);
                context.SaveChanges();

                var baseRepository = new BaseRepository<Livro>(context);
                var livros = await baseRepository.GetAsync();

                Assert.NotNull(livros);
                Assert.Equal(2, livros.Count());
            }



        }


    }
}
