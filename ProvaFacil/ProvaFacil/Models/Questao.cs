using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaFacil.Models
{
    [Table("QUESTAO")]
    public class Questao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cod_questao { get; set; }

        [Display(Name = "Tipo da questão")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(20, ErrorMessage = "Máximo de {1} caracteres.")]
        [Required]
        public string Tipo_questao { get; set; }

        [Display(Name = "Descrição da questão")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(1500, ErrorMessage = "Máximo de {1} caracteres.")]
        [Required]
        public string Descricao_questao { get; set; }

        [Display(Name = "Valor da questão")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        [Range(0, 10.0)]
        public decimal? Valor_questao { get; set; }

        [Display(Name = "Número de linhas")]
        [Column(TypeName = "TINYINT")]
        public byte? Numero_linhas { get; set; }

        // Relacionamento questao_opcao
        public ICollection<Opcao> Opcao { get; set; }

        // Relacionamento materia_questao
        public Materia Materia { get; set; }
        public int Cod_materia { get; set; }

        // Relacionamento cabecalho_questao
        public Cabecalho Cabecalho { get; set; }
        public int Cod_prova { get; set; }

        // Relacionamento assunto_questao
        public Assunto Assunto { get; set; }
        public int Cod_assunto { get; set; }
    }
}