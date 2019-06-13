using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaFacil.Models
{
    [Table("COLEGIO")]
    public class Colegio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cod_colegio { get; set; }

        [Display(Name = "Nome do colégio")]
        [StringLength(80, ErrorMessage = "Máximo de {1} caracteres.")]
        [Column(TypeName = "VARCHAR")]
        public string Nome_colegio { get; set; }

        // Relacionamento colegio_cabecalho
        public ICollection<Cabecalho> Cabecalho { get; set; }

        // Relacionamento colegio_turma
        public ICollection<Turma> Turma { get; set; }
    }
}