using BIL.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BIL.Data
{
    public class BILContext : DbContext
    {
        public BILContext([NotNullAttribute] DbContextOptions options) : base(options)
        {

        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Editora> Editoras { get; set; }


    }
}
