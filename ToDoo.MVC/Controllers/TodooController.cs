using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoo.MVC.Infra;

namespace ToDoo.MVC.Controllers
{
    public class TodooController : Controller
    {
        private readonly TodooRestClient restClient;

        public TodooController()
        {
            this.restClient = new TodooRestClient();
        }
        // GET: TodooController
        public ActionResult Index()
        {
            var model = this.restClient.GetAll();

            return View(model);
        }

        // GET: TodooController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = this.restClient.GetById(id);
            return View(model);
        }

        // GET: TodooController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodooController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TodooModel model)
        {
            try
            {
                this.restClient.Save(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodooController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = this.restClient.GetById(id);
            return View(model);
        }

        // POST: TodooController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, TodooModel model)
        {
            try
            {
                this.restClient.Update(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodooController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = this.restClient.GetById(id);
            return View(model);
            }

        // POST: TodooController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, TodooModel model)
        {
            try
            {
                this.restClient.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
