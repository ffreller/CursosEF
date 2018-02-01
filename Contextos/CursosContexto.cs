using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore;
using CursosEF.Models;

namespace CursosEF.Contextos
{
    public class CursosContexto:DbContext
    {
        public CursosContexto(DbContextOptions<CursosContexto>options):base(options)
        {}
        public DbSet<Area> Area   { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Turma> Turma { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Area>().ToTable("Area");
            modelbuilder.Entity<Curso>().ToTable("Curso");
            modelbuilder.Entity<Turma>().ToTable("Turma");
        }
        
    }
}