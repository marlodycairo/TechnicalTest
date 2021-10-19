using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TecnicalTestLibraryMVC.Models;

namespace TecnicalTestLibraryMVC.Controllers
{
    public class BooksController : Controller
    {
        // GET: BooksController
        public ActionResult<BookViewModel> Index(string searchString)
        {
            IEnumerable<BookViewModel> books = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392/api/");

                Task<HttpResponseMessage> responseTask = client.GetAsync("books");

                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    Task<List<BookViewModel>> readTask = result.Content.ReadAsAsync<List<BookViewModel>>();

                    readTask.Wait();

                    books = readTask.Result;
                }
                else
                {
                    books = Enumerable.Empty<BookViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Contact the administrator.");
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(p => p.Title.StartsWith(searchString, StringComparison.CurrentCultureIgnoreCase) || Convert.ToString(p.AuthorId).StartsWith(searchString, StringComparison.CurrentCultureIgnoreCase) || p.Date.ToShortDateString().StartsWith(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            return View(books);
        }

        // GET: BooksController/Details/5
        public ActionResult<BookViewModel> Details(int id)
        {
            BookViewModel book = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392/");

                Task<HttpResponseMessage> response = client.GetAsync($"api/books/{id}");

                response.Wait();

                HttpResponseMessage result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    Task<BookViewModel> readBook = result.Content.ReadAsAsync<BookViewModel>();

                    readBook.Wait();

                    book = readBook.Result;
                }
            }

            return View(book);
        }

        // GET: BooksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookViewModel books)
        {
            Uri uri = new Uri("https://localhost:44392/api/books");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;

                Task<HttpResponseMessage> createAuthor = client.PostAsJsonAsync("books", books);

                createAuthor.Wait();

                HttpResponseMessage result = createAuthor.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        // GET: BooksController/Edit/5
        public ActionResult Edit(int id)
        {
            BookViewModel book = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392/");

                Task<HttpResponseMessage> response = client.GetAsync($"api/books/{id}");

                response.Wait();

                HttpResponseMessage result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    Task<BookViewModel> readBook = result.Content.ReadAsAsync<BookViewModel>();

                    readBook.Wait();

                    book = readBook.Result;
                }
            }

            return View(book);
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<BookViewModel> Edit(BookViewModel bookViewModel)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392/api/");

                Task<HttpResponseMessage> responseTask = client.PutAsJsonAsync("books", bookViewModel);

                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    Task<BookViewModel> readTask = result.Content.ReadAsAsync<BookViewModel>();

                    readTask.Wait();

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(bookViewModel);
        }

        // GET: BooksController/Delete/5
        public ActionResult Delete(int id)
        {
            BookViewModel book = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392/");

                Task<HttpResponseMessage> response = client.GetAsync($"api/books/{id}/");

                response.Wait();

                HttpResponseMessage result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    Task<BookViewModel> readBook = result.Content.ReadAsAsync<BookViewModel>();

                    readBook.Wait();

                    book = readBook.Result;
                }
            }

            return View(book);
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, BookViewModel books)
        {
            BookViewModel book = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392/");

                Task<HttpResponseMessage> responseTask = client.DeleteAsync($"api/books/{id}");

                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    Task<BookViewModel> readTask = result.Content.ReadAsAsync<BookViewModel>();

                    readTask.Wait();

                    book = readTask.Result;
                }
            }

            return View();
        }
    }
}
