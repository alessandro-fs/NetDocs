using AutoMapper;
using NetDocs.BL;
using NetDocs.Model.Entities;
using NetDocs.UI.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NetDocs.UI.Controllers
{
    public class SetorController : Controller
    {
        private readonly SetorBL _setorBL = new SetorBL();
        private readonly CelulaBL _celulaBL = new CelulaBL();

        // GET: Setor
        public ActionResult Index()
        {
            var _setorViewModel = Mapper.Map<IEnumerable<Setor>, IEnumerable<SetorViewModel>>(_setorBL.GetAll());
            return View(_setorViewModel);
        }

        // GET: Setor/Details/5
        public ActionResult Details(int id)
        {
            var _setor = _setorBL.GetById(id);
            var _setorViewModel = Mapper.Map<Setor, SetorViewModel>(_setor);
            return View(_setorViewModel);
        }

        // GET: Setor/Create
        public ActionResult Create()
        {
            ViewBag.CelulaId = new SelectList(_celulaBL.GetAll(), "CelulaId", "Nome");
            return View();
        }

        // POST: Setor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SetorViewModel setor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _setor = Mapper.Map<SetorViewModel, Setor>(setor);
                    _setorBL.Add(_setor);
                    return RedirectToAction("Index");
                }
                ViewBag.CelulaId = new SelectList(_celulaBL.GetAll(), "CelulaId", "Nome", setor.Celula.CelulaId);
                return View(setor);
            }
            catch
            {
                return View();
            }
        }

        // GET: Setor/Edit/5
        public ActionResult Edit(int id)
        {
            var _setor = _setorBL.GetById(id);
            var _setorViewModel = Mapper.Map<Setor, SetorViewModel>(_setor);

            ViewBag.CelulaId = new SelectList(_celulaBL.GetAll(), "CelulaId", "Nome", _setorViewModel.Celula.CelulaId);

            return View(_setorViewModel);
        }

        // POST: Setor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SetorViewModel setor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _setor = Mapper.Map<SetorViewModel, Setor>(setor);
                    _setorBL.Update(_setor);
                    return RedirectToAction("Index");
                }
                ViewBag.CelulaId = new SelectList(_celulaBL.GetAll(), "CelulaId", "Nome", setor.Celula.CelulaId);
                return View(setor);
            }
            catch
            {
                return View();
            }
        }

        // GET: Setor/Delete/5
        public ActionResult Delete(int id)
        {
            var _setor = _setorBL.GetById(id);
            var _setorViewModel = Mapper.Map<Setor, SetorViewModel>(_setor);
            return View(_setorViewModel);
        }

        // POST: Setor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var _setor = _setorBL.GetById(id);
                _setorBL.Remove(_setor);
                                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
