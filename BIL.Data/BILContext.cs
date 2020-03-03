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
        public BILContext(DbContextOptions<BILContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ADMIN");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Editora> Editoras { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }


    }
}
