using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Todo.Data.Models;
using Todo.Web.ViewModels;
using Todo.Business.Services;

namespace Todo.Web.Controllers
{
    public class TodoItemDBController : Controller
    {
        private readonly IMapper mapper;
        private readonly IDataProviderAsync<TodoItemVo> provider;
        private readonly Data.Context.AppContext context;

        public TodoItemDBController(Data.Context.AppContext context, IMapper mapper, IDataProviderAsync<TodoItemVo> provider)
        {
            this.provider = provider;
            this.mapper = mapper;
            this.context = context;
        }


        // GET: TodoItemDB
        public async Task<IActionResult> Index()
        {
            var todoItems = await provider.GetAll();
            return View(mapper.Map<IEnumerable<TodoItemViewModel>>(todoItems));
        }

        // GET: TodoItemDB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await provider.Get((int)id);

            if (todoItem == null)
            {
                return NotFound();
            }
            return View(mapper.Map<TodoItemViewModel>(todoItem));
        }

        // GET: TodoItemDB/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(context.Categories, "ID", "Name");
            return View();
        }

        // POST: TodoItemDB/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,DeadLineDate,CreationDate,Priority,Status,CategoryID")] TodoItemViewModel todoItem)
        {
            if (ModelState.IsValid)
            {
                var todoItemVo = mapper.Map<TodoItemVo>(todoItem);
                todoItemVo.CreationDate = DateTime.UtcNow;
                await provider.Add(todoItemVo);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(context.Categories, "ID", "Name", todoItem.CategoryID);
            return View(todoItem);
        }

        // GET: TodoItemDB/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItemVo = await provider.Get((int)id);
            if (todoItemVo == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(context.Categories, "ID", "Name", todoItemVo.CategoryID);
            return View(mapper.Map<TodoItemViewModel>(todoItemVo));
        }

        // POST: TodoItemDB/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,DeadLineDate,CreationDate,Priority,Status,CategoryID")] TodoItemViewModel todoItem)
        {
            if (id != todoItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var todoItemVo = mapper.Map<TodoItemVo>(todoItem);
                    await provider.Edit(todoItemVo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!provider.Exists(todoItem.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(context.Categories, "ID", "Name", todoItem.CategoryID);
            return View(todoItem);
        }

        // GET: TodoItemDB/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await provider.Get((int)id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return View(mapper.Map<TodoItemViewModel>(todoItem));
        }

        // POST: TodoItemDB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await provider.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
