using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaFacil.Models
{
    [Table("OPCAO")]
    public class Opcao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cod_opcao { get; set; }

        [Display(Name = "Descrição da opção")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Descricao_opcao { get; set; }

        [NotMapped]
        [Display(Name = "Descrição da opção")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Descricao_opcaoMult1 { get; set; }

        [NotMapped]
        [Display(Name = "Descrição da opção")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Descricao_opcaoMult2 { get; set; }

        [NotMapped]
        [Display(Name = "Descrição da opção")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Descricao_opcaoMult3 { get; set; }

        [NotMapped]
        [Display(Name = "Descrição da opção")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Descricao_opcaoMult4 { get; set; }

        [NotMapped]
        [Display(Name = "Descrição da opção")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Descricao_opcaoMult5 { get; set; }

        [NotMapped]
        [Display(Name = "Descrição da opção")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Descricao_opcaoVF1 { get; set; }

        [NotMapped]
        [Display(Name = "Descrição da opção")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Descricao_opcaoVF2 { get; set; }

        [NotMapped]
        [Display(Name = "Descrição da opção")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Descricao_opcaoVF3 { get; set; }

        [NotMapped]
        [Display(Name = "Descrição da opção")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Descricao_opcaoVF4 { get; set; }

        [NotMapped]
        [Display(Name = "Descrição da opção")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Descricao_opcaoVF5 { get; set; }

        public bool Opcao_correta { get; set; }

        [NotMapped]
        public string Opcao_correta_escolhidaMult { get; set; }

        [NotMapped]
        public bool? Opcao_correta_escolhidaVF1 { get; set; }

        [NotMapped]
        public bool? Opcao_correta_escolhidaVF2 { get; set; }

        [NotMapped]
        public bool? Opcao_correta_escolhidaVF3 { get; set; }

        [NotMapped]
        public bool? Opcao_correta_escolhidaVF4 { get; set; }

        [NotMapped]
        public bool? Opcao_correta_escolhidaVF5 { get; set; }

        // Relacionamento questao_opcao
        public Questao Questao { get; set; }
        public int Cod_questao { get; set; }
    }
}