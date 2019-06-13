using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProvaFacil.Models
{
    [Table("CABECALHO")]
    public class Cabecalho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cod_prova { get; set; }

        [Display(Name = "Data da prova")]
        [DataType(DataType.Date)]
        [Column(TypeName = "DATE")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Data_prova { get; set; }

        [Display(Name = "Valor da prova")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        [Range(0, 10.0)]
        public decimal Valor_prova { get; set; }

        [Column(TypeName = "VARBINARY(MAX)")]
        public byte[] Imagem_cabecalho { get; set; }

        // Relacionamento usuario_cabecalho
        public Usuario Usuario { get; set; }
        public int Cod_usuario { get; set; }

        // Relacionamento colegio_cabecalho
        public Colegio Colegio { get; set; }
        public int Cod_colegio { get; set; }

        // Relacionamento cabecalho_questao
        public ICollection<Questao> Questao { get; set; }

        // Relacionamento turma_cabecalho
        public Turma Turma { get; set; }
        public int Cod_turma { get; set; }
    }
}