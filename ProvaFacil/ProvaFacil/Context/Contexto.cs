using ProvaFacil.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ProvaFacil.Context
{
    public class Contexto : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Cabecalho> Cabecalho { get; set; }
        public DbSet<Colegio> Colegio { get; set; }
        public DbSet<Turma> Turma { get; set; }
        public DbSet<Assunto> Assunto { get; set; }
        public DbSet<Materia> Materia { get; set; }
        public DbSet<Questao> Questao { get; set; }
        public DbSet<Opcao> Opcao { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Definir o inicializador. Desabilite a linha caso mude a connectionstring.
            //Database.SetInitializer<Contexto>(null);
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Na criação da tabela Usuario, definir o campo Senha como NOT NULL.
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Senha)
                .IsRequired();

            // Na criação da tabela Opcao, definir o campo Opcao_correta como NULL.
            modelBuilder.Entity<Opcao>()
                .Property(o => o.Opcao_correta)
                .IsOptional();

            // Na criação da tabela Questao, definir o campo Valor_questao como NULL.
            modelBuilder.Entity<Questao>()
                .Property(q => q.Valor_questao)
                .IsOptional();

            // Na criação da tabela Cabecalho, definir o campo Data_prova como NULL.
            modelBuilder.Entity<Cabecalho>()
                .Property(c => c.Data_prova)
                .IsOptional();

            // Definir todos os campos do tipo DECIMAL com precisão de 3, 1 (2 casas antes da vírgula e 1 casa depois da vírgula).
            modelBuilder.Properties<decimal>()
                .Configure(c => c.HasPrecision(3, 1));
        }
    }
}