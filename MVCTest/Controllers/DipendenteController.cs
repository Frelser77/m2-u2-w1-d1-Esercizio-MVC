using MVCTest.Models;
using System;
using System.Web.Mvc;

namespace MVCTest.Controllers
{
    public class DipendenteController : Controller
    {

        private Dipendente dipendenteDataAccess = new Dipendente();

        // GET: Dipendente
        public ActionResult Index()
        {
            var dipendenti = dipendenteDataAccess.GetTuttiIDipendenti();
            return View(dipendenti);
        }

        // GET: Dipendente/Details/5
        public ActionResult Details(int id)
        {
            var dipendente = dipendenteDataAccess.GetDipendenteById(id);
            if (dipendente == null)
            {
                return HttpNotFound();
            }
            return View(dipendente);
        }

        // GET: Dipendente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dipendente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dipendente dipendente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dipendenteDataAccess.AddDipendente(dipendente);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Gestire l'eccezione e, se necessario, fornire feedback all'utente
                ModelState.AddModelError("", "Si è verificato un problema durante il salvataggio: " + ex.Message);
            }

            // Se siamo arrivati fino a qui, qualcosa non va: ri-mostrare il form con i dati inseriti
            return View(dipendente);
        }

        // GET: Dipendente/Edit/5
        public ActionResult Edit(int id)
        {
            var dipendente = dipendenteDataAccess.GetDipendenteById(id);
            if (dipendente == null)
            {
                return HttpNotFound();
            }
            return View(dipendente);
        }

        // POST: Dipendente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dipendente dipendente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dipendenteDataAccess.UpdateDipendente(dipendente);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Gestire l'eccezione e, se necessario, fornire feedback all'utente
                ModelState.AddModelError("", "Si è verificato un problema durante l'aggiornamento del dipendente: " + ex.Message);
            }

            // Se siamo arrivati fino a qui, qualcosa non va: ri-mostrare il form con i dati inseriti
            return View(dipendente);
        }

        // GET: Dipendente/Delete/5
        public ActionResult Delete(int id)
        {
            var dipendente = dipendenteDataAccess.GetDipendenteById(id);
            if (dipendente == null)
            {
                return HttpNotFound();
            }
            return View(dipendente);
        }

        // POST: Dipendente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                dipendenteDataAccess.DeleteDipendente(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Gestire l'eccezione e, se necessario, fornire feedback all'utente
                ModelState.AddModelError("", "Si è verificato un problema durante l'eliminazione del dipendente: " + ex.Message);
            }

            // Se siamo arrivati fino a qui, qualcosa non va: ri-mostrare il form con i dati inseriti
            var dipendente = dipendenteDataAccess.GetDipendenteById(id);
            return View("Delete", dipendente);
        }
    }
}