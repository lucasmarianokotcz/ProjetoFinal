using ProvaFacil.Context;
using ProvaFacil.Models;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ProvaFacil.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly Contexto db = new Contexto();

        // Método que autentica o usuário logado.
        private void Autenticacao(UsuarioLogin user)
        {
            // Seleciona o único usuário com o e-mail informado para em seguida usar o seu nome para dar boas-vindas.
            var usuario = db.Usuario.Single(u => u.Email == user.Email);

            // Identidade do usuário
            var identity = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.Sid, usuario.Cod_usuario.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Nome),
                },
            "ApplicationCookie");

            // Contexto da autenticação
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            // SignIn (autenticação de login)
            authManager.SignIn(identity);
        }

        // Método que faz o Hash da senha do usuário.
        private static string Hash(string input)
        {
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Concat(hash.Select(b => b.ToString("x2")));
        }

        // Método que pega o RedirectUrl de acordo com sucesso ou falha do login do usuário.
        private string GetRedirectUrl(string returnUrl)
        {
            // Caso o usuário tenha feito login
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Prova");
            }

            // Caso o usuário tenha um login inválido (volta para a mesma URL atual (action Login))
            return returnUrl;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            // Verifica se o usuário está autenticado. Se estiver, desloga.
            if (User.Identity.IsAuthenticated)
            {
                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignOut("ApplicationCookie");
                return RedirectToAction("Index");
            }

            // Usuário tem a ReturnUrl de acordo com sua tentativa de login (se logou ou não logou)
            var usuario = new UsuarioLogin
            {
                ReturnUrl = returnUrl
            };

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsuarioLogin usuarioLogin)
        {
            // Valida os campos do form
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Persiste o banco procurando pelo usuário digitado
            var user = db.Usuario.SingleOrDefault(u => u.Email == usuarioLogin.Email);

            // Se usuário não for encontrado, mostra erro e retorna para a mesma página (login)
            if (user == null)
            {
                ModelState.AddModelError("", "E-mail ou senha inválidos");
                return View();
            }

            // Faz o hash de senha.
            usuarioLogin.Senha = Hash(usuarioLogin.Senha);

            // Se o usuário digitado coincidir com a senha digitada, fazer login
            if (usuarioLogin.Email == user.Email && usuarioLogin.Senha == Encoding.UTF8.GetString(user.Senha))
            {
                // Autentica o usuário.
                Autenticacao(usuarioLogin);

                // Redireciona o usuário para a action Index
                return Redirect(GetRedirectUrl(usuarioLogin.ReturnUrl));
            }

            // Autenticação falhou
            ModelState.AddModelError("", "E-mail ou senha inválidos");
            return View();
        }

        [HttpGet]
        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(Usuario usuario)
        {
            // Verifica se os campos estão válidos.
            if (ModelState.IsValid)
            {
                // Busca se já existe um usuário cadastrado com o mesmo e-mail.                
                var user = db.Usuario.SingleOrDefault(u => u.Email == usuario.Email);

                // Se já existe um usuário.
                if (user != null)
                {
                    ModelState.AddModelError("", "E-mail informado já existe.");
                    return View();
                }

                // Faz o hash de senha.
                usuario.Senha = Encoding.ASCII.GetBytes(Hash(usuario.SenhaHash));

                // Insere o usuário no banco.
                db.Usuario.Add(usuario);
                db.SaveChanges();

                // Autentica o usuário recém-cadastrado.
                UsuarioLogin usuarioLogin = new UsuarioLogin
                {
                    Email = usuario.Email,
                    Senha = Encoding.UTF8.GetString(usuario.Senha)
                };
                Autenticacao(usuarioLogin);

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}