using BIL.Data.Entidades;
using BIL.Data.Repository.Interface;
using BIL.Logica.Manager;
using BIL.Shared;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BIL.Tests.Manager
{
    public class LivroManagerTest
    {
        [Fact]
        public async Task Deve_Criar_Livro_Async()
        {
            //Arrange
            var mockLivro = new Livro()
            {
                Id = 1,
                Titulo = "Titulo do Livro",
                Descricao = "Descricao do Livro",
                Editora = null,
                QuantidadePaginas = 20

            };

            var mockLivroRepo = new Mock<ILivroRepository>();

            mockLivroRepo.Setup(l => l.CreateLivroAsync(It.IsAny<Livro>()))
                .ReturnsAsync(mockLivro);

            var livroManager = new LivroManager(mockLivroRepo.Object);

            //Act

            var livroParaTestar = new LivroDto()
            {
                Titulo = "Titulo do Livro",
                Descricao = "Descricao do Livro",
                Editora = null,
                QuantidadePaginas = 20
            };

            var livroCriado = await livroManager.CreateLivroAsync(livroParaTestar);

            //Assert

            Assert.Equal(1, livroCriado.Id);
            mockLivroRepo.Verify(l => l.CreateLivroAsync(It.IsAny<Livro>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Consultar_Um_Livro_Async()
        {
            var mockLivro = new Livro()
            {
                Id = 1,
                Titulo = "Titulo do livro",
                Descricao = "Descrição do livro",
                Editora = null,
                QuantidadePaginas = 20
            };

            var mockLivroRepo = new Mock<ILivroRepository>();

            mockLivroRepo.Setup(l => l.GetLivroAsync(It.Is<int>(l => l == 1)))
                .ReturnsAsync(mockLivro);

            var livroManager = new LivroManager(mockLivroRepo.Object);

            var livro = await livroManager.GetLivroAsync(1);

            Assert.NotNull(livro);
            Assert.Equal(1, livro.Id);
            mockLivroRepo.Verify(l => l.GetLivroAsync(It.Is<int>(l => l == 1)), Times.Once);

        }

        [Fact]
        public async Task Deve_Retornar_Varios_livros_Async()
        {
            var mockLivro1 = new Livro()
            {
                Id = 1,
                Titulo = "Titulo do livro 1",
                Descricao = "Descrição do livro 1",
                Editora = null,
                QuantidadePaginas = 10
            };

            var mockLivro2 = new Livro()
            {
                Id = 2,
                Titulo = "Titulo do livro 2",
                Descricao = "Descrição do livro 2",
                Editora = null,
                QuantidadePaginas = 20
            };

            var mockLivro3 = new Livro()
            {
                Id = 3,
                Titulo = "Titulo do livro 3",
                Descricao = "Descrição do livro 3",
                Editora = null,
                QuantidadePaginas = 30
            };

            List<Livro> livros = new List<Livro>();
            livros.Add(mockLivro1);
            livros.Add(mockLivro2);
            livros.Add(mockLivro3);

            var mockLivroRepo = new Mock<ILivroRepository>();
            mockLivroRepo.Setup(l => l.GetLivrosAsync())
                .ReturnsAsync(livros);

            var livroManager = new LivroManager(mockLivroRepo.Object);

            var livrosConsultados = await livroManager.GetLivrosAsync();

            Assert.NotNull(livrosConsultados);
            Assert.Equal(3, livrosConsultados.Count());
            mockLivroRepo.Verify(l => l.GetLivrosAsync(), Times.Once);








        }
    }
}
