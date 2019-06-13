using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProvaFacil.Models
{
    public class Prova
    {
        public int Id { get; set; }
        public Assunto Assunto { get; set; }
        public Cabecalho Cabecalho { get; set; }
        public Colegio Colegio { get; set; }
        public Materia Materia { get; set; }
        public Opcao Opcao { get; set; }
        public Questao Questao { get; set; }
        public Turma Turma { get; set; }

        // Lista de questões
        public List<Questao> ListaQuestoes { get; set; }

        public HttpPostedFileBase ImagemEscolhida { get; set; }
    }
}