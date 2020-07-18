using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;
using app.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    public class TodoItemsController : Controller
    {

        private readonly ITodoItemProvider todoItemProvider;

        public TodoItemsController(ITodoItemProvider todoItemProvider)
        {
            this.todoItemProvider = todoItemProvider;
        }

        // GET: TodoItemsController
        public ActionResult Index()
        {
            return View(todoItemProvider.GetAll());
        }

        // GET: TodoItemsController/Details/5
        public ActionResult Details(int id)
        {
            return View(todoItemProvider.Get(id));
        }

        // GET: TodoItemsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoItemsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TodoItem todoItem)
        {
            try
            {
                todoItemProvider.Add(todoItem);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoItemsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(todoItemProvider.Get(id));
        }

        // POST: TodoItemsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TodoItem todoItem)
        {
            try
            {
                todoItemProvider.Edit(id, todoItem);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoItemsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(todoItemProvider.Get(id));
        }

        // POST: TodoItemsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TodoItem todoItem)
        {
            try
            {
                todoItemProvider.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
