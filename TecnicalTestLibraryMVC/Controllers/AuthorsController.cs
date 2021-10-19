using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TecnicalTestLibraryMVC.Models;

namespace TecnicalTestLibraryMVC.Controllers
{
    public class AuthorsController : Controller
    {
        // GET: AuthorsController
        public ActionResult<AuthorViewModel> Index(string searchString)
        {
            IEnumerable<AuthorViewModel> authors = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392/api/");

                Task<HttpResponseMessage> responseTask = client.GetAsync("authors");

                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    Task<List<AuthorViewModel>> readTask = result.Content.ReadAsAsync<List<AuthorViewModel>>();

                    readTask.Wait();

                    authors = readTask.Result;
                }
                else
                {
                    authors = Enumerable.Empty<AuthorViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Contact the administrator.");
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                authors = authors.Where(p => p.FullName.StartsWith(searchString, StringComparison.CurrentCultureIgnoreCase) || p.CityOrigin.StartsWith(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            return View(authors);
        }

        // GET: AuthorsController/Details/5
        public ActionResult<AuthorViewModel> Details(int id)
        {
            AuthorViewModel author = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392/");

                Task<HttpResponseMessage> response = client.GetAsync($"api/authors/{id}");

                response.Wait();

                HttpResponseMessage result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    Task<AuthorViewModel> readAuthor = result.Content.ReadAsAsync<AuthorViewModel>();

                    readAuthor.Wait();

                    author = readAuthor.Result;
                }
            }
            return View(author);
        }

        // GET: AuthorsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorViewModel authors)
        {
            Uri uri = new Uri("https://localhost:44392/api/authors");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;

                Task<HttpResponseMessage> createAuthor = client.PostAsJsonAsync("authors", authors);

                createAuthor.Wait();

                HttpResponseMessage result = createAuthor.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        // GET: AuthorsController/Edit/5
        public ActionResult Edit(int id)
        {
            AuthorViewModel author = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392/");

                Task<HttpResponseMessage> response = client.GetAsync($"api/authors/{id}");

                response.Wait();

                HttpResponseMessage result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    Task<AuthorViewModel> readAuthor = result.Content.ReadAsAsync<AuthorViewModel>();

                    readAuthor.Wait();

                    author = readAuthor.Result;
                }
            }

            return View(author);
        }

        // POST: AuthorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<AuthorViewModel> Edit(AuthorViewModel authorViewModel)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392/api/");

                Task<HttpResponseMessage> responseTask = client.PutAsJsonAsync("authors", authorViewModel);

                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    Task<AuthorViewModel> readTask = result.Content.ReadAsAsync<AuthorViewModel>();

                    readTask.Wait();

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(authorViewModel);
        }

        // GET: AuthorsController/Delete/5
        public ActionResult Delete(int id)
        {
            AuthorViewModel author = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392/");

                Task<HttpResponseMessage> response = client.GetAsync($"api/authors/{id}/");

                response.Wait();

                HttpResponseMessage result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    Task<AuthorViewModel> readAuthor = result.Content.ReadAsAsync<AuthorViewModel>();

                    readAuthor.Wait();

                    author = readAuthor.Result;
                }
            }

            return View(author);
        }

        // POST: AuthorsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, AuthorViewModel authors)
        {
            AuthorViewModel author = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392/");

                Task<HttpResponseMessage> responseTask = client.DeleteAsync($"api/authors/{id}");

                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    Task<AuthorViewModel> readTask = result.Content.ReadAsAsync<AuthorViewModel>();

                    readTask.Wait();

                    author = readTask.Result;
                }
            }

            return View(author);
        }
    }
}
