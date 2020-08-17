using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Todo.Data.Context;
using Todo.Data.Models;
using Todo.Web.ViewModels;

namespace Todo.Web.Controllers
{
    public class TodoItemTagDBController : Controller
    {
        private readonly Data.Context.AppContext _context;
        private readonly IMapper mapper;

        public TodoItemTagDBController(Data.Context.AppContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: TodoItemTagDB
        public async Task<IActionResult> Index()
        {
            var todoContext = _context.TodoItemTag.Include(t => t.Tag).Include(t => t.TodoItem);
            var itemTags = await todoContext.ToListAsync();
            return View(mapper.Map<IEnumerable<TodoItemTagViewModel>>(itemTags));
        }

        // GET: TodoItemTagDB/Details/5
        public async Task<IActionResult> Details(int? todoItemID, int? tagID)
        {
            if (todoItemID == null || tagID == null)
            {
                return NotFound();
            }

            var todoItemTag = await _context.TodoItemTag
                .Include(s => s.TodoItem)
                .Include(s => s.Tag)
                .FirstOrDefaultAsync(m => m.TodoItemID == todoItemID && m.TagID == tagID);
            if (todoItemTag == null)
            {
                return NotFound();
            }

            return View(mapper.Map<TodoItemTagViewModel>(todoItemTag));
        }

        // GET: TodoItemTagDB/Create
        public IActionResult Create()
        {
            ViewData["TagID"] = new SelectList(_context.Tags, "ID", "Name");
            ViewData["TodoItemID"] = new SelectList(_context.TodoItems, "ID", "Name");
            return View();
        }

        // POST: TodoItemTagDB/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TodoItemID,TagID")] TodoItemTagDao todoItemTag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoItemTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TagID"] = new SelectList(_context.Tags, "ID", "ID", todoItemTag.TagID);
            ViewData["TodoItemID"] = new SelectList(_context.TodoItems, "ID", "Name", todoItemTag.TodoItemID);
            return View(mapper.Map<TodoItemTagViewModel>(todoItemTag));
        }

        // GET: TodoItemTagDB/Edit/5
        public async Task<IActionResult> Edit(int? todoItemID, int? tagID)
        {
            if (todoItemID == null || tagID == null)
            {
                return NotFound();
            }

            var todoItemTag = await _context.TodoItemTag.FindAsync(todoItemID, tagID);
            if (todoItemTag == null)
            {
                return NotFound();
            }
            ViewData["TodoItemID"] = new SelectList(_context.TodoItems, "ID", "Name", todoItemTag.TodoItemID);
            ViewData["TagID"] = new SelectList(_context.Tags, "ID", "Name", todoItemTag.TagID);
            ViewData["OldTodoItemID"] = todoItemID;
            ViewData["OldTagID"] = tagID;
            return View(mapper.Map<TodoItemTagViewModel>(todoItemTag));
        }

        // POST: TodoItemTagAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? oldTodoItemID, int? oldTagID, [Bind("TodoItemID,TagID")] TodoItemTagDao todoItemTag)
        {
            if (todoItemTag == null)
            {
                return NotFound();
            }
            var oldTodoItemTag = await _context.TodoItemTag.FindAsync(oldTodoItemID, oldTagID);
            if (oldTodoItemTag == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Remove(oldTodoItemTag);
                    _context.Add(todoItemTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoItemTagExists(todoItemTag.TodoItemID))
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
            ViewData["TodoItemID"] = new SelectList(_context.TodoItems, "ID", "Name", todoItemTag.TodoItemID);
            ViewData["TagID"] = new SelectList(_context.Tags, "ID", "Name", todoItemTag.TagID);
            return View(mapper.Map<TodoItemTagViewModel>(todoItemTag));
        }

        // GET: TodoItemTagDB/Delete/5
        public async Task<IActionResult> Delete(int? todoItemID, int? tagID)
        {
            if (todoItemID == null || tagID == null)
            {
                return NotFound();
            }

            var todoItemTag = await _context.TodoItemTag
                .Include(s => s.TodoItem)
                .Include(s => s.Tag)
                .FirstOrDefaultAsync(m => m.TodoItemID == todoItemID && m.TagID == tagID);
            if (todoItemTag == null)
            {
                return NotFound();
            }

            return View(mapper.Map<TodoItemTagViewModel>(todoItemTag));
        }

        // POST: TodoItemTagAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("TodoItemID,TagID")] TodoItemTagDao todoItemTag)
        {
            _context.TodoItemTag.Remove(todoItemTag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoItemTagExists(int id)
        {
            return _context.TodoItemTag.Any(e => e.TodoItemID == id);
        }
    }
}
