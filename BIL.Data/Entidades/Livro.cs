using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BIL.Data.Entidades
{
    public class Livro : EntidadeBase
    {
        [Key]
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public int QuantidadePaginas { get; set; }

        public Editora Editora { get; set;}

    }
}

