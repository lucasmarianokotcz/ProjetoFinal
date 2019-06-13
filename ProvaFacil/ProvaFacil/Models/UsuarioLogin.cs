using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProvaFacil.Models
{
    public class UsuarioLogin
    {
        [EmailAddress]
        [Display(Name = "Endereço de e-mail")]
        [Required(ErrorMessage = "Entre com o endereço de e-mail.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Entre com a senha.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}