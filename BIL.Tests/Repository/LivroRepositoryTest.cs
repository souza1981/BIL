using BIL.Data.Entidades;
using BIL.Data.Repository;
using BIL.Data.Repository.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BIL.Tests.Repository
{
    public class LivroRepositoryTest : RepositoryTest
    {
        [Fact]
        public async Task Deve_Criar_livro()
        {
            var novoLivro = CriarNovoLivro(1);

            var mockBaseRepository = new Mock<IBaseRepository<Livro>>();

            mockBaseRepository.Setup(m => m.CreateAsync(It.IsAny<Livro>()))
                .ReturnsAsync(novoLivro);

            var livroRepository = new LivroRepository(mockBaseRepository.Object);

            var livroCriado = await livroRepository.CreateLivroAsync(novoLivro);

            Assert.NotNull(livroCriado);
            Assert.Equal(1, livroCriado.Id);
            mockBaseRepository.Verify(l => l.CreateAsync(It.IsAny<Livro>()), Times.Once);
        }

        private static Livro CriarNovoLivro(int id = 0)
        {
            if (id == 0)
            {
                return new Livro()
                {
                    Titulo = "Titulo do Livro",
                    Descricao = "Descricao do Livro",
                    Editora = null,
                    QuantidadePaginas = 20
                };
            } else
            {
                return new Livro()
                {
                    Titulo = "Titulo do Livro",
                    Descricao = "Descricao do Livro",
                    Editora = null,
                    Id = id,
                    QuantidadePaginas = 20
                };
            }
        }

        [Fact]
        public async Task Deve_Consultar_Um_Livro()
        {
            var livroParaConsultar = CriarNovoLivro(1);

            var mockBaseRepository = new Mock<IBaseRepository<Livro>>();
            mockBaseRepository.Setup(m => m.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(livroParaConsultar);

            var livroRepository = new LivroRepository(mockBaseRepository.Object);

            var livroConsultado = await livroRepository.GetLivroAsync(livroParaConsultar.Id);

            Assert.NotNull(livroConsultado);
            Assert.Equal(1, livroConsultado.Id);

            mockBaseRepository.Verify(l => l.GetAsync(It.IsAny<int>()), Times.Once);

        }
        [Fact]
        public async Task Deve_Consultar_Dois_Livros()
        {
            var livro1 = CriarNovoLivro(1);
            var livro2 = CriarNovoLivro(2);
            List<Livro> livros = new List<Livro>();
            livros.Add(livro1);
            livros.Add(livro2);

            var mockBaseRepository = new Mock<IBaseRepository<Livro>>();

            mockBaseRepository.Setup(l => l.GetAsync(null,null,""))
                .ReturnsAsync(livros);

            var livroRepository = new LivroRepository(mockBaseRepository.Object);

            IEnumerable<Livro> livrosConsultados = await livroRepository.GetLivrosAsync();

            Assert.NotNull(livrosConsultados);
            Assert.Equal(2, livros.Count);
            mockBaseRepository.Verify(l => l.GetAsync(null, null, ""), Times.Once);

        }

    }
}
