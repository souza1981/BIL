using System;
using System.Collections.Generic;
using System.Text;

namespace BIL.Data.Entidades
{
    public class Editora : EntidadeBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public List<Livro> Livros { get; set; }
    }
}
