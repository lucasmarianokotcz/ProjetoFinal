using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaFacil.Models
{
    [Table("RESPOSTA")]
    public class Resposta
    {
        [Column(TypeName = "VARCHAR")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Resposta1 { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Resposta2 { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Resposta3 { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Resposta4 { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Resposta5 { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(150, ErrorMessage = "Máximo de {1} caracteres.")]
        public string Resposta_correta { get; set; }
    }
}