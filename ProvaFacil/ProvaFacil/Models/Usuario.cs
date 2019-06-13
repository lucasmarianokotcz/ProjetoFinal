using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProvaFacil.Models
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cod_usuario { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Máximo de {1} caracteres.")]
        [Column(TypeName = "VARCHAR")]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(60, ErrorMessage = "Máximo de {1} caracteres.")]
        [Column(TypeName = "VARCHAR")]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [MaxLength(40)]
        [Column(TypeName = "BINARY")]
        public byte[] Senha { get; set; }

        [NotMapped]
        [Required]
        [Display(Name = "Senha")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Mínimo de {2} e máximo de {1} caracteres.")]        
        public string SenhaHash { get; set; }

        // Relacionamento usuario_cabecalho
        public ICollection<Cabecalho> Cabecalho { get; set; }
    }
}