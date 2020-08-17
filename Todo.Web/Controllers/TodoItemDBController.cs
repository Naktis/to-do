using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Todo.Business.Models;
using Todo.Web.ViewModels;

namespace Todo.Web.Controllers
{
    public class TodoItemDBController : Controller
    {
        private readonly Business.Data.AppContext _context;
        private readonly IMapper mapper;

        public TodoItemDBController(Business.Data.AppContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: TodoItemDB
        public async Task<IActionResult> Index()
        {
            var todoContext = _context.TodoItems.Include(t => t.Category);
            var todoItems = await todoContext.ToListAsync();
            return View(mapper.Map<IEnumerable<TodoItemViewModel>>(todoItems));
        }

        // GET: TodoItemDB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItems
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(mapper.Map<TodoItemViewModel>(todoItem));
        }

        // GET: TodoItemDB/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name");
            return View();
        }

        // POST: TodoItemDB/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,DeadLineDate,CreationDate,Priority,Status,CategoryID")] TodoItemDao todoItem)
        {
            if (ModelState.IsValid)
            {
                todoItem.CreationDate = DateTime.UtcNow;
                _context.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", todoItem.CategoryID);
            return View(mapper.Map<TodoItemViewModel>(todoItem));
        }

        // GET: TodoItemDB/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", todoItem.CategoryID);
            return View(mapper.Map<TodoItemViewModel>(todoItem));
        }

        // POST: TodoItemDB/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,DeadLineDate,CreationDate,Priority,Status,CategoryID")] TodoItemDao todoItem)
        {
            if (id != todoItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoItemExists(todoItem.ID))
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
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", todoItem.CategoryID);
            return View(mapper.Map<TodoItemViewModel>(todoItem));
        }

        // GET: TodoItemDB/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItems
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
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
            var todoItem = await _context.TodoItems.FindAsync(id);
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoItemExists(int id)
        {
            return _context.TodoItems.Any(e => e.ID == id);
        }
    }
}
