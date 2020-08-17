﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Business.Services;
using Todo.Business.Models;

namespace Todo.Web.Controllers
{
    public class CategoryController : Controller
{
        private readonly IDataProvider<CategoryDao> categoryProvider;

        public CategoryController(IDataProvider<CategoryDao> categoryProvider)
        {
            this.categoryProvider = categoryProvider;
        }

        // GET: Category
        public ActionResult Index()
        {
            return View(categoryProvider.GetAll());
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View(categoryProvider.Get(id));
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryDao category)
        {
            try
            {
                categoryProvider.Add(category);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View(categoryProvider.Get(id));
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryDao category)
        {
            try
            {
                categoryProvider.Edit(category);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View(categoryProvider.Get(id));
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CategoryDao category)
        {
            try
            {
                categoryProvider.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }
    }
}
