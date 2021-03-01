using Microsoft.EntityFrameworkCore;
using ProjectSchool_API.Models;
using System.Collections;
using System.Collections.Generic;

namespace ProjectSchool_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options) : base (options){}
        public DbSet<Aluno> Alunos {get; set;}
        public DbSet<Professor> Professores {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Professor>()
                .HasData(
                    new List<Professor>(){
                        new Professor() {
                            Id = 1,
                            Nome = "Vinicius"
                        },
                        new Professor() {
                            Id = 2,
                            Nome = "Paula"
                        },
                        new Professor() {
                            Id = 3,
                            Nome = "Luna"
                        },
                    }
                );

            builder.Entity<Aluno>()
                .HasData(
                    new List<Aluno>() {
                        new Aluno() {
                            Id = 1,
                            Nome = "Maria",
                            Sobrenome = "Jose",
                            DataNasc = "25/06/1934",
                            ProfessorId = 1
                        },
                        new Aluno() {
                            Id = 2,
                            Nome = "Joao",
                            Sobrenome = "Jose",
                            DataNasc = "25/06/1932",
                            ProfessorId = 2
                        },
                        new Aluno() {
                            Id = 3,
                            Nome = "Alex",
                            Sobrenome = "Ferraz",
                            DataNasc = "25/06/1989",
                            ProfessorId = 3
                        },
                    }
                );
        }
    }
}