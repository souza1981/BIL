using BIL.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIL.Shared
{
    public class LivroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public int QuantidadePaginas { get; set; }

        public Editora Editora { get; set; }
    }
}
