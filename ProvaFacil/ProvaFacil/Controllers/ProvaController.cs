using ProvaFacil.Context;
using ProvaFacil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using Rotativa;

namespace ProvaFacil.Controllers
{
    public class ProvaController : Controller
    {
        private Contexto db = new Contexto();

        #region Métodos
        private void VerifTipoQuestao(Prova prova)
        {
            if (prova.Questao.Tipo_questao == "Múltipla escolha")
            {
                // Seta o número de linhas, já que não é uma questão dissertativa.
                prova.Questao.Numero_linhas = null;

                // Cria uma lista de opções de múltipla escolha para salvar no banco as opções digitadas.
                List<Opcao> opcoes = new List<Opcao>()
                        {
                            new Opcao() { Descricao_opcao = prova.Opcao.Descricao_opcaoMult1 },
                            new Opcao() { Descricao_opcao = prova.Opcao.Descricao_opcaoMult2 },
                            new Opcao() { Descricao_opcao = prova.Opcao.Descricao_opcaoMult3 },
                            new Opcao() { Descricao_opcao = prova.Opcao.Descricao_opcaoMult4 },
                            new Opcao() { Descricao_opcao = prova.Opcao.Descricao_opcaoMult5 },
                        };

                // Verifica qual é opção correta marcada pelo usuário para salvar e enviar ao banco.
                switch (prova.Opcao.Opcao_correta_escolhidaMult)
                {
                    case "1":
                        opcoes[0].Opcao_correta = true;
                        break;
                    case "2":
                        opcoes[1].Opcao_correta = true;
                        break;
                    case "3":
                        opcoes[2].Opcao_correta = true;
                        break;
                    case "4":
                        opcoes[3].Opcao_correta = true;
                        break;
                    case "5":
                        opcoes[4].Opcao_correta = true;
                        break;
                    default:
                        break;
                }
                db.Opcao.AddRange(opcoes);
            }

            else if (prova.Questao.Tipo_questao == "Verdadeiro/falso")
            {
                // Seta o número de linhas, já que não é uma questão dissertativa.
                prova.Questao.Numero_linhas = null;

                // Cria uma lista de opções de verdadeiro/falso para salvar no banco as opções digitadas.
                List<Opcao> opcoes = new List<Opcao>()
                        {
                            new Opcao() { Descricao_opcao = prova.Opcao.Descricao_opcaoVF1 },
                            new Opcao() { Descricao_opcao = prova.Opcao.Descricao_opcaoVF2 },
                            new Opcao() { Descricao_opcao = prova.Opcao.Descricao_opcaoVF3 },
                            new Opcao() { Descricao_opcao = prova.Opcao.Descricao_opcaoVF4 },
                            new Opcao() { Descricao_opcao = prova.Opcao.Descricao_opcaoVF5 },
                        };

                // Verifica quais opções estão marcadas entre Verdadeiro e Falso e salva para enviar ao banco.
                switch (prova.Opcao.Opcao_correta_escolhidaVF1)
                {
                    case true:
                        opcoes[0].Opcao_correta = true;
                        break;
                    case false:
                        opcoes[0].Opcao_correta = false;
                        break;
                    default:
                        break;
                }
                switch (prova.Opcao.Opcao_correta_escolhidaVF2)
                {
                    case true:
                        opcoes[1].Opcao_correta = true;
                        break;
                    case false:
                        opcoes[1].Opcao_correta = false;
                        break;
                    default:
                        break;
                }
                switch (prova.Opcao.Opcao_correta_escolhidaVF3)
                {
                    case true:
                        opcoes[2].Opcao_correta = true;
                        break;
                    case false:
                        opcoes[2].Opcao_correta = false;
                        break;
                    default:
                        break;
                }
                switch (prova.Opcao.Opcao_correta_escolhidaVF4)
                {
                    case true:
                        opcoes[3].Opcao_correta = true;
                        break;
                    case false:
                        opcoes[3].Opcao_correta = false;
                        break;
                    default:
                        break;
                }
                switch (prova.Opcao.Opcao_correta_escolhidaVF5)
                {
                    case true:
                        opcoes[4].Opcao_correta = true;
                        break;
                    case false:
                        opcoes[4].Opcao_correta = false;
                        break;
                    default:
                        break;
                }
                db.Opcao.AddRange(opcoes);
            }
        }

        private bool VerifSomaQuestoes(Prova prova, bool correto = true)
        {
            prova.ListaQuestoes = db.Questao.Where(q => q.Cod_prova == prova.Cabecalho.Cod_prova).ToList();
            decimal somaQuestoes = 0;
            if (prova.Questao.Valor_questao != null)
            {
                somaQuestoes = Convert.ToDecimal(prova.Questao.Valor_questao);
            }

            for (int i = 0; i < prova.ListaQuestoes.Count; i++)
            {
                if (prova.ListaQuestoes[i].Valor_questao != null)
                {
                    somaQuestoes += Convert.ToDecimal(prova.ListaQuestoes[i].Valor_questao);
                }
            }

            if (somaQuestoes > prova.Cabecalho.Valor_prova)
            {
                // Busca a lista de questões da prova inserida.
                prova.ListaQuestoes = db.Questao.Where(q => q.Cod_prova == prova.Cabecalho.Cod_prova).ToList();
                ViewBag.Sucesso = "ErroQuestoes";
                //ModelState.AddModelError("", "A soma do valor das questões não pode ser maior que o valor da prova.");
                correto = false;
            }
            return correto;
        }

        private int PegaUsuarioLogado()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return int.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);
        }

        private bool VerifId(int? id, bool existe = true)
        {
            // Se não for passado um ID para ver os detalhes, redireciona para a página da lista de provas.
            if (id == null)
            {
                existe = false;
                return existe;
            }

            // Se não for encontrado uma prova com o ID, redireciona para a página da lista de provas.
            var queryProva = db.Cabecalho.Find(id);
            if (queryProva == null)
            {
                existe = false;
                return existe;
            }

            // Se a prova encontrada não pertencer ao usuário logado, redireciona para a página da lista de provas.
            if (queryProva.Cod_usuario != PegaUsuarioLogado())
            {
                existe = false;
                return existe;
            }
            return existe;
        }

        private Prova ValoresProva(int? id)
        {
            // Instancia e atribui valores aos objetos da prova.
            Cabecalho cabecalho = db.Cabecalho.Find(id);
            Colegio colegio = db.Colegio.Find(cabecalho.Cod_colegio);
            Materia materia = db.Questao.Where(q => q.Cod_prova == cabecalho.Cod_prova).Select(m => m.Materia).FirstOrDefault();
            Assunto assunto = db.Questao.Where(q => q.Cod_prova == cabecalho.Cod_prova).Select(a => a.Assunto).FirstOrDefault();
            Turma turma = db.Turma.Find(cabecalho.Cod_turma);

            Prova prova = new Prova
            {
                Cabecalho = cabecalho,
                Colegio = colegio,
                Materia = materia,
                Assunto = assunto,
                Turma = turma
            };

            // Se a imagem de cabecalho existir, mostra ela.
            if (cabecalho.Imagem_cabecalho != null)
            {
                var base64 = Convert.ToBase64String(cabecalho.Imagem_cabecalho);
                var imagemCabecalho = string.Format("data:image/png;base64,{0}", base64);
                ViewBag.ImagemCabecalho = imagemCabecalho;
            }

            // Busca a lista de questões da prova inserida.
            prova.ListaQuestoes = db.Questao.Where(q => q.Cod_prova == prova.Cabecalho.Cod_prova).Include(o => o.Opcao).ToList();

            return prova;
        }
        #endregion

        #region Index
        // GET: Prova
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Nova
        // GET: Prova/Nova
        public ActionResult Nova()
        {
            return View();
        }

        // POST: Prova/Nova
        // Salvar questão
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Nova(Prova prova)
        {
            if (ModelState.IsValid)
            {
                if (prova.Questao.Valor_questao > prova.Cabecalho.Valor_prova)
                {
                    ModelState.AddModelError("", "O valor da questão não pode ser maior que o valor da prova.");
                    return View(prova);
                }

                // Pega o ID do usuário logado e o define no cabeçalho.
                var identity = (ClaimsIdentity)User.Identity;
                prova.Cabecalho.Cod_usuario = int.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);

                // Seta a imagem de cabeçalho.
                if (prova.ImagemEscolhida != null)
                {
                    prova.Cabecalho.Imagem_cabecalho = new byte[prova.ImagemEscolhida.ContentLength];
                    prova.ImagemEscolhida.InputStream.Read(prova.Cabecalho.Imagem_cabecalho, 0, prova.ImagemEscolhida.ContentLength);
                }

                // Busca pelo colégio digitado pelo usuário. Caso já exista, salva a prova com este colégio. Caso seja um novo colégio, adiciona um novo colégio ao banco.
                var queryColegio = db.Colegio.SingleOrDefault(c => c.Nome_colegio == prova.Colegio.Nome_colegio);
                if (queryColegio != null)
                {
                    prova.Cabecalho.Cod_colegio = queryColegio.Cod_colegio;
                    prova.Turma.Cod_colegio = queryColegio.Cod_colegio;
                }
                else
                {
                    db.Colegio.Add(prova.Colegio);
                }

                // Busca pela matéria digitada pelo usuário. Caso já exista, salva a prova com esta matéria. Caso seja uma nova matéria, adiciona uma nova matéria ao banco.
                var queryMateria = db.Materia.SingleOrDefault(m => m.Nome_materia == prova.Materia.Nome_materia);
                if (queryMateria != null)
                {
                    prova.Assunto.Cod_materia = queryMateria.Cod_materia;
                    prova.Questao.Cod_materia = queryMateria.Cod_materia;
                }
                else
                {
                    db.Materia.Add(prova.Materia);
                }

                // Busca pelo assunto digitado pelo usuário. Caso seja um novo assunto, adiciona um novo assunto ao banco.
                var queryAssunto = db.Assunto.SingleOrDefault(a => a.Nome_assunto == prova.Assunto.Nome_assunto);
                if (queryAssunto != null)
                {
                    prova.Questao.Cod_assunto = queryAssunto.Cod_assunto;
                }
                if (queryAssunto == null)
                {
                    db.Assunto.Add(prova.Assunto);
                }

                // Busca pela turma digitada pelo usuário (campos 'Turno' e 'Nível'). Caso não encontre uma turma daquele colégio, adiciona uma nova turma ao banco.
                var queryTurma = db.Turma.Where(t => t.Cod_colegio == prova.Cabecalho.Cod_colegio).SingleOrDefault(t => t.Turno == prova.Turma.Turno && t.Nivel == prova.Turma.Nivel);
                if (queryTurma != null)
                {
                    prova.Cabecalho.Turma = queryTurma;
                }
                else
                {
                    db.Turma.Add(prova.Turma);
                }

                db.Cabecalho.Add(prova.Cabecalho);
                prova.Questao.Cabecalho = prova.Cabecalho;
                prova.Questao.Cod_prova = prova.Cabecalho.Cod_prova;
                db.Questao.Add(prova.Questao);

                // Verifica o tipo de questão e associa as opções da questão.
                VerifTipoQuestao(prova);

                db.SaveChanges();
                prova.ListaQuestoes = db.Questao.Where(q => q.Cod_prova == prova.Cabecalho.Cod_prova).ToList();
                TempData["prova"] = prova;
                return RedirectToAction("Editar");
            }
            ModelState.AddModelError("", "Verifique campos inválidos ou em branco");
            return View(prova);
        }
        #endregion

        #region Editar
        // GET: Prova/Editar
        public ActionResult Editar()
        {
            Prova prova = new Prova();
            prova = TempData["prova"] as Prova;
            if (prova == null)
            {
                return RedirectToAction("Nova");
            }
            ViewBag.CodProva = prova.Cabecalho.Cod_prova;
            ViewBag.Sucesso = "Certo";
            TempData["editarProva"] = prova;
            if (prova.Cabecalho.Imagem_cabecalho != null)
            {
                var base64 = Convert.ToBase64String(prova.Cabecalho.Imagem_cabecalho);
                var imagemCabecalho = string.Format("data:image/png;base64,{0}", base64);
                ViewBag.ImagemCabecalho = imagemCabecalho;
            }

            return View(prova);
        }

        // POST: Prova/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Prova prova)
        {
            if (ModelState.IsValid)
            {
                // Código da prova atual.
                ViewBag.CodProva = prova.Cabecalho.Cod_prova;

                // Mostra a imagem do cabeçalho.
                var queryCabecalho = db.Cabecalho.SingleOrDefault(c => c.Cod_prova == prova.Cabecalho.Cod_prova);
                if (queryCabecalho.Imagem_cabecalho != null)
                {
                    var base64 = Convert.ToBase64String(queryCabecalho.Imagem_cabecalho);
                    var imagemCabecalho = string.Format("data:image/png;base64,{0}", base64);
                    ViewBag.ImagemCabecalho = imagemCabecalho;
                }

                // Busca a matéria e associa o código da matéria com o código da questão.
                var queryMateria = db.Materia.SingleOrDefault(m => m.Nome_materia == prova.Materia.Nome_materia);
                prova.Questao.Cod_materia = queryMateria.Cod_materia;

                // Busca o assunto e associa o código do assunto com o código da questão.
                var queryAssunto = db.Assunto.SingleOrDefault(a => a.Nome_assunto == prova.Assunto.Nome_assunto);
                prova.Questao.Cod_assunto = queryAssunto.Cod_assunto;

                // Verifica o tipo de questão e associa as opções da questão.
                VerifTipoQuestao(prova);

                // Associa o código do cabeçalho com o código da questão.
                prova.Questao.Cod_prova = prova.Cabecalho.Cod_prova;

                // Adiciona a questão e insere no banco.
                db.Questao.Add(prova.Questao);

                // Verifica se a soma do valor das questões é menor que o valor da prova.
                if (VerifSomaQuestoes(prova) == false)
                {
                    return View(prova);
                }

                db.SaveChanges();

                // Busca a lista de questões da prova inserida.
                prova.ListaQuestoes = db.Questao.Where(q => q.Cod_prova == prova.Cabecalho.Cod_prova).ToList();
                ViewBag.Sucesso = "Certo";
                return View(prova);
            }
            ModelState.AddModelError("", "Verifique campos inválidos ou em branco");
            return View(prova);
        }
        #endregion

        //#region Excluir
        //// GET: Prova/Excluir
        //public ActionResult Excluir(int? id)
        //{
        //    // Verifica o ID passado.
        //    if (VerifId(id) == false)
        //    {
        //        return RedirectToAction("Lista");
        //    }

        //    // Instancia e atribui valores aos objetos da prova.
        //    Cabecalho cabecalho = db.Cabecalho.Find(id);
        //    Colegio colegio = db.Colegio.Find(cabecalho.Cod_colegio);
        //    Materia materia = db.Questao.Where(q => q.Cod_prova == cabecalho.Cod_prova).Select(m => m.Materia).FirstOrDefault();
        //    Assunto assunto = db.Questao.Where(q => q.Cod_prova == cabecalho.Cod_prova).Select(a => a.Assunto).FirstOrDefault();
        //    Turma turma = db.Turma.Find(cabecalho.Cod_turma);

        //    Prova prova = new Prova
        //    {
        //        Cabecalho = cabecalho,
        //        Colegio = colegio,
        //        Materia = materia,
        //        Assunto = assunto,
        //        Turma = turma
        //    };

        //    return View(prova);
        //}

        //// POST: Prova/Excluir
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Excluir(int id)
        //{
        //    Cabecalho cabecalho = db.Cabecalho.Find(id);
        //    var questoes = db.Questao.Where(q => q.Cod_prova == cabecalho.Cod_prova);
        //    var opcoes = db.Questao.Where(q => q.Cod_prova == cabecalho.Cod_prova).SelectMany(o => o.Opcao);
        //    db.Opcao.RemoveRange(opcoes);
        //    db.Questao.RemoveRange(questoes);
        //    db.Cabecalho.Remove(cabecalho);
        //    db.SaveChanges();
        //    return RedirectToAction("Lista");
        //}
        //#endregion

        #region Lista
        // GET: Prova/Lista
        public ActionResult Lista()
        {
            int usuarioLogado = PegaUsuarioLogado();
            var provas = db.Cabecalho.Where(c => c.Cod_usuario == usuarioLogado).
                Include(c => c.Colegio).
                Include(t => t.Turma).
                Include(q => q.Questao.Select(m => m.Materia).Select(a => a.Assunto)).ToList();

            return View(provas);
        }
        #endregion

        #region Detalhes
        // GET: Prova/Detalhes
        public ActionResult Detalhes(int? id)
        {
            // Verifica o ID passado.
            if (VerifId(id) == false)
            {
                return RedirectToAction("Lista");
            }

            // Associa o ID da prova.
            Prova prova = new Prova() { Id = Convert.ToInt32(id) };

            return View(prova);
        }

        public PartialViewResult VisualizarPDF(int? id)
        {
            return PartialView("_VisualizarPDF", ValoresProva(id));
        }

        public ActionResult GerarPDF(int? id)
        {
            return new PartialViewAsPdf("_VisualizarPDF", ValoresProva(id))
            {
                FileName = "Prova.pdf"
            };
        }
        #endregion        
    }
}