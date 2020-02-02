using BIL.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIL.Shared
{
    class EditoraDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<Livro> Livros { get; set; }
    }
}
