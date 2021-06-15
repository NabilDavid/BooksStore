using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Models;
using BooksStore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Controls
{
    public class AutherController : Controller
    {
        private IBookRepository<Auther> autherRepository;

        public AutherController(IBookRepository<Auther> autherRepository )
        {
            this.autherRepository = autherRepository; ;

        }
        // GET: Auther
        public ActionResult Index()
        {
            var authers = autherRepository.list();
            return View(authers);
        }

        // GET: Auther/Details/5
        public ActionResult Details(int id)
        {
            var auther = autherRepository.find(id);

            return View(auther);
        }

        // GET: Auther/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auther/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Auther auther)
        {
            try
            {
                autherRepository.add(auther);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Auther/Edit/5
        public ActionResult Edit(int id)
        {
            var auther = autherRepository.find(id);

            return View(auther);
        }

        // POST: Auther/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Auther auther)
        {
            try
            {

                autherRepository.update(id, auther);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Auther/Delete/5
        public ActionResult Delete(int id)
        {
            var auther = autherRepository.find(id);
            return View(auther);
        }

        // POST: Auther/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Auther auther)
        {
            try
            {
                autherRepository.delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}