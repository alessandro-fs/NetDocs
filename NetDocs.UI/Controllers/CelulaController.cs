using AutoMapper;
using NetDocs.BL;
using NetDocs.Model.Entities;
using NetDocs.UI.ViewModels;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using NetDocs.UI.Extensions;
using System.Net;
using MvcSiteMapProvider.Web.Mvc.Filters;

namespace NetDocs.UI.Controllers
{
    public class CelulaController : Controller
    {

        private readonly CelulaBL _celulaBL = new CelulaBL();
        // GET: Celula
        public ActionResult Index(Celula celula, string ordenacao, int? pagina, FormCollection collection)
        {
            try
            {
                //var _celulaViewModel = Mapper.Map<IEnumerable<Celula>, IEnumerable<CelulaViewModel>>(_celulaBL.GetAll());

                //CARREGANDO DADOS
                celula.Nome = String.IsNullOrEmpty(Request.QueryString["FiltrarNome"]) ? String.Empty : Request.QueryString["FiltrarNome"];
                var _celulas = from c in _celulaBL.GetAll().Where(c => c.Nome.Contains(celula.Nome))
                               select c;

                //ORDENAÇÃO DOS DADOS
                ordenacao = (String.IsNullOrEmpty(ordenacao) ? "Nome_Asc" : ordenacao);
                switch (ordenacao)
                {
                    case ("Nome_Desc"):
                        _celulas = _celulas.OrderByDescending(c => c.Nome);
                        break;
                    default:
                        _celulas = _celulas.OrderBy(c => c.Nome);
                        break;

                }

                //PAGINAÇÃO            
                //TODO: PEGAR VALOR DO PARâMETRO
                int _tamanhoPagina = 5;
                pagina = pagina == null ? 1 : pagina;
                pagina = pagina < 1 ? 1 : pagina;
                pagina = _tamanhoPagina >= _celulas.Count() ? 1 : pagina;

                int _numeroPagina = (pagina ?? 1);
                IPagedList model = _celulas.ToPagedList(_numeroPagina, _tamanhoPagina);
                _numeroPagina = model.PageNumber;

                //VIEWBAGS            
                ViewBag.OrdemPor = (ordenacao == "Nome_Asc" || String.IsNullOrEmpty(ordenacao) ? "Nome_Desc" : "Nome_Asc");
                ViewBag.Ordenacao = ordenacao;
                ViewBag.PaginaAtual = _numeroPagina;
                ViewBag.NomeCorrente = celula.Nome;
                ViewBag.NomeAplicacao = "NetDocs® 1997-2015";

                ViewBag.TotalRegistros = UtilExtensions.GetPageInfo(_celulas.Count(), model);

                return View(_celulas.ToPagedList(_numeroPagina, _tamanhoPagina));
            }
            catch(Exception ex)
            {
                this.AddNotification(@Resources.Resource1.FalhaOperacao + " - " + ex.Message, NotificationType.ERROR);
                return View();
            }
        }

        // GET: Celula/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                ViewBag.Ordenacao = Request.QueryString["Ordenacao"]; ;
                ViewBag.PaginaAtual = Request.QueryString["Pagina"]; ;
                ViewBag.NomeCorrente = Request.QueryString["FiltrarNome"];
            }
            var _celula = _celulaBL.GetById(id);
            var _celulaViewModel = Mapper.Map<Celula, CelulaViewModel>(_celula);
            return View(_celulaViewModel);
        }

        // GET: Celula/Create
        public ActionResult Create()
        {
            //---
            //TODO: ALTERAR PARA USUÁRIO LOGADO
            var _model = new CelulaViewModel();
            _model.UsuarioCadastro = "MASTER";
            return View(_model);
        }

        // POST: Celula/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public ActionResult CreatePost([Bind(Include = "Nome,DataCadastro,UsuarioCadastro")] CelulaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _cliente = Mapper.Map<CelulaViewModel, Celula>(model);
                    _celulaBL.Add(_cliente);
                    this.AddNotification(@Resources.Resource1.RegistroIncluido, NotificationType.SUCCESS);
                    return RedirectToAction("Index");
                }
                else
                {
                    this.AddNotification(@Resources.Resource1.FalhaOperacao, NotificationType.ERROR);
                    return View(model);
                }
            }
            catch(Exception ex)
            {
                //TODO: SALVAR LOG NO BANCO DE DADOS
                this.AddNotification(@Resources.Resource1.FalhaOperacao + " - " + ex.Message, NotificationType.ERROR);
                return View();
            }
        }

        // GET: Celula/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                ViewBag.Ordenacao = Request.QueryString["Ordenacao"]; ;
                ViewBag.PaginaAtual = Request.QueryString["Pagina"]; ;
                ViewBag.NomeCorrente = Request.QueryString["FiltrarNome"];
            }
            var _celula = _celulaBL.GetById(id);            
            var _celulaViewModel = Mapper.Map<Celula, CelulaViewModel>(_celula);
            //---
            //ALTERAR PARA USUÁRIO LOGADO
            _celulaViewModel.UsuarioAlteracao = "MASTER";
            _celulaViewModel.DataAlteracao = DateTime.Now;
            return View(_celulaViewModel);
        }

        // POST: Celula/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CelulaViewModel model)
        {
            try
            {
                model.DataAlteracao = DateTime.Now;
                if (ModelState.IsValid)
                {
                    var _celula = Mapper.Map<CelulaViewModel, Celula>(model);                    
                    _celulaBL.Update(_celula);
                    this.AddNotification(@Resources.Resource1.RegistroAlterado, NotificationType.SUCCESS);
                    return RedirectToAction("Index", "Celula", new { Pagina = Request.QueryString["Pagina"], Ordenacao = Request.QueryString["Ordenacao"], FiltrarNome = Request.QueryString["FiltrarNome"] });
                }
                else
                {
                    return View(model);
                }                
            }
            catch(Exception ex)
            {
                this.AddNotification(@Resources.Resource1.FalhaOperacao + " - " + ex.Message, NotificationType.ERROR);
                return View(model);
            }
        }

        // POST: Celula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                Celula cliente = _celulaBL.GetById(id);
                _celulaBL.Remove(cliente);
                this.AddNotification(@Resources.Resource1.RegistroExcluido, NotificationType.SUCCESS);

                return RedirectToAction("Index", "Celula", UtilExtensions.GetRoutValues("Index", "Celula", collection));
            }
            catch(Exception ex)
            {
                this.AddNotification(@Resources.Resource1.FalhaOperacao + " - " + ex.Message, NotificationType.ERROR);
                return View("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _celulaBL.Dispose();
            }
            base.Dispose(disposing);
        }        
       
    }
}
