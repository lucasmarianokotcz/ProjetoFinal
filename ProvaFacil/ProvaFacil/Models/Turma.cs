using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaFacil.Models
{
    [Table("TURMA")]
    public class Turma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cod_turma { get; set; }

        [StringLength(5, ErrorMessage = "Manhã/Tarde/Noite")]
        [Column(TypeName = "CHAR")]
        public string Turno { get; set; }

        [Display(Name = "Nível de ensino")]
        [StringLength(20, ErrorMessage = "Máximo de {1} caracteres.")]
        [Column(TypeName = "VARCHAR")]
        public string Nivel { get; set; }

        // Relacionamento colegio_turma
        public Colegio Colegio { get; set; }
        public int Cod_colegio { get; set; }

        // Relacionamento turma_cabecalho
        public ICollection<Cabecalho> Cabecalho { get; set; }
    }
}