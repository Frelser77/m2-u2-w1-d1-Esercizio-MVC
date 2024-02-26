using MVCTest.Models;
using System;
using System.Web.Mvc;

namespace MVCTest.Controllers
{
    public class PagamentiController : Controller
    {
        private Pagamento pagamentoDataAccess = new Pagamento();

        // GET: Pagamenti
        public ActionResult Index()
        {
            var pagamenti = pagamentoDataAccess.GetAllPagamenti();
            return View(pagamenti);
        }

        // GET: Pagamenti/Details/5
        public ActionResult Details(int id)
        {
            var pagamento = pagamentoDataAccess.GetPagamentoById(id);
            if (pagamento == null)
            {
                return HttpNotFound();
            }
            return View(pagamento);
        }

        // GET: Pagamenti/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pagamenti/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pagamento pagamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pagamentoDataAccess.AddPagamento(pagamento);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Si è verificato un problema durante il salvataggio del pagamento: " + ex.Message);
            }

            return View(pagamento);
        }

        // GET: Pagamenti/Edit/5
        public ActionResult Edit(int id)
        {
            var pagamento = pagamentoDataAccess.GetPagamentoById(id);
            if (pagamento == null)
            {
                return HttpNotFound();
            }
            return View(pagamento);
        }

        // POST: Pagamenti/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pagamento pagamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pagamentoDataAccess.UpdatePagamento(pagamento);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Si è verificato un problema durante l'aggiornamento del pagamento: " + ex.Message);
            }

            return View(pagamento);
        }

        // GET: Pagamenti/Delete/5
        public ActionResult Delete(int id)
        {
            var pagamento = pagamentoDataAccess.GetPagamentoById(id);
            if (pagamento == null)
            {
                return HttpNotFound();
            }
            return View(pagamento);
        }

        // POST: Pagamenti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                pagamentoDataAccess.DeletePagamento(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Si è verificato un problema durante l'eliminazione del pagamento: " + ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
