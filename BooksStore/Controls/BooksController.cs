using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Models;
using BooksStore.Models.Repositories;
using BooksStore.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Controls
{
    public class BooksController : Controller
    {
        private IBookRepository<Book> bookRepository;

        private IBookRepository<Auther> autherRepository;

        public IHostingEnvironment hosting { get; }

        public BooksController( IBookRepository<Book> bookRepository , IBookRepository<Auther> autherRepository,
            IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.autherRepository = autherRepository;
            this.hosting = hosting;
        }


        // GET: Books
        public ActionResult Index()
        {
            var books=bookRepository.list();
            return View(books);
        }

        // GET: Books/Details/5
        public ActionResult Details(int id)
        {
            var books = bookRepository.find(id);
            return View(books);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
           
            return View(getAllAuthors());
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AutherBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = UploadFie(model.file) ?? string.Empty;


                    if (model.Autherid == 0)
                    {

                        ViewBag.Message = "please select An auther from list";
                       
                        return View(getAllAuthors());

                    }
                    var auther = autherRepository.find(model.Autherid);
                    Book book = new Book
                    {
                        id = model.Bookid,
                        title = model.title,
                        description = model.description,
                        Auther = auther,
                        imageUrl=fileName



                    };
                    bookRepository.add(book);

                    return RedirectToAction(nameof(Index));

                }
                catch
                {
                    return View();
                }
            }
            else
            { 

                ModelState.AddModelError("", "please fill all attrbute required");
                return View(getAllAuthors());
            }
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
            Book book = bookRepository.find(id);
            var autherId = book.Auther == null ? book.Auther.id = 0 : book.Auther.id;
            AutherBookViewModel autherBookViewModel = new AutherBookViewModel
            {
                Bookid = book.id,
                title = book.title,
                description = book.description,
                Autherid = autherId,
                authers = autherRepository.list().ToList(),
                imageUrl=book.imageUrl

            };



            return View(autherBookViewModel);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AutherBookViewModel autherBookViewModel)
        {
            try
            {
                string fileName = UploadFie(autherBookViewModel.file, autherBookViewModel.imageUrl);
            


                var auther = autherRepository.find(autherBookViewModel.Autherid);
                Book book = new Book
                {
                    id=autherBookViewModel.Bookid,
                    title = autherBookViewModel.title,
                    description = autherBookViewModel.description,
                    Auther = auther,
                    imageUrl=fileName



                };
                bookRepository.update(autherBookViewModel.Bookid, book);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.find(id);
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult confirmDelete(int id)
        {
            try
            {
                bookRepository.delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        List<Auther> fillSelectList()
        {
            var authers = autherRepository.list().ToList();
            authers.Insert(0, new Auther { id = 0, fullName = "--- Please Select Auther---" });

            return (authers);

        }
        AutherBookViewModel getAllAuthors()
        {


            var viewModel = new AutherBookViewModel
            {
                authers = fillSelectList()
            };
            return (viewModel);
        }
        string UploadFie(IFormFile file)
        {
            if (file  != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));
               
                return file.FileName;

            }
            return null; 

        }


        string UploadFie(IFormFile file , string imageUrl)
        {
            if (file != null)
            {
                // new Path
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string NewfullPath = Path.Combine(uploads, file.FileName);
                //old Path
                string oldFullPath = Path.Combine(uploads, imageUrl);


                if (oldFullPath != NewfullPath)
                {
                    //delete old path
                    System.IO.File.Delete(oldFullPath);

                    //save new  path
                    file.CopyTo(new FileStream(NewfullPath, FileMode.Create));

                }

                return file.FileName;
            }
            return imageUrl;
        }

        
       public  ActionResult SearchBooks (string term)
        {
           var result= bookRepository.Search(term);
           
            return View("Index", result);
            
        }
    }
}