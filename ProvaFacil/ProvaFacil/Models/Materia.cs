using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaFacil.Models
{
    [Table("MATERIA")]
    public class Materia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cod_materia { get; set; }

        [Display(Name = "Matéria")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(20, ErrorMessage = "Máximo de {1} caracteres.")]
        [Required]
        public string Nome_materia { get; set; }

        // Relacionamento materia_questao
        public ICollection<Questao> Questao { get; set; }

        // Relacionamento materia_assunto
        public ICollection<Assunto> Assunto { get; set; }
    }
}