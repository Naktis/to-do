using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Todo.Data.Models;
using Todo.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Web.Controllers
{
    public class TodoItemController : Controller
    {
        private readonly IDataProvider<TodoItemDao> todoItemProvider;

        public TodoItemController(IDataProvider<TodoItemDao> todoItemProvider)
        {
            this.todoItemProvider = todoItemProvider;
        }

        // GET: TodoItem
        public ActionResult Index()
        {
            return View(todoItemProvider.GetAll());
        }

        // GET: TodoItem/Details/5
        public ActionResult Details(int id)
        {
            return View(todoItemProvider.Get(id));
        }

        // GET: TodoItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TodoItemDao todoItem)
        {
            try
            {
                todoItemProvider.Add(todoItem);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(todoItem);
            }
        }

        // GET: TodoItem/Edit/5
        public ActionResult Edit(int id)
        {
            return View(todoItemProvider.Get(id));
        }

        // POST: TodoItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TodoItemDao todoItem)
        {
            try
            {
                todoItemProvider.Edit(todoItem);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(todoItem);
            }
        }

        // GET: TodoItem/Delete/5
        public ActionResult Delete(int id)
        {
            return View(todoItemProvider.Get(id));
        }

        // POST: TodoItem/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TodoItemDao todoItem)
        {
            try
            {
                todoItemProvider.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(todoItem);
            }
        }
    }
}
