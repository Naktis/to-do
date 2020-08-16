using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    public class TodoItemTagDBController : Controller
    {
        private readonly Data.AppContext _context;

        public TodoItemTagDBController(Data.AppContext context)
        {
            _context = context;
        }

        // GET: TodoItemTagDB
        public async Task<IActionResult> Index()
        {
            var todoContext = _context.TodoItemTag.Include(t => t.Tag).Include(t => t.TodoItem);
            return View(await todoContext.ToListAsync());
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

            return View(todoItemTag);
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
        public async Task<IActionResult> Create([Bind("TodoItemID,TagID")] TodoItemTag todoItemTag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoItemTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TagID"] = new SelectList(_context.Tags, "ID", "ID", todoItemTag.TagID);
            ViewData["TodoItemID"] = new SelectList(_context.TodoItems, "ID", "Name", todoItemTag.TodoItemID);
            return View(todoItemTag);
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
            return View(todoItemTag);
        }

        // POST: TodoItemTagAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? oldTodoItemID, int? oldTagID, [Bind("TodoItemID,TagID")] TodoItemTag todoItemTag)
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
            return View(todoItemTag);
        }

        // GET: TodoItemTagDB/Delete/5
        public async Task<IActionResult> Delete(int? todoItemID, int? tagID)
        {
            if (todoItemID == null || tagID == null)
            {
                return NotFound();
            }

            var studentTag = await _context.TodoItemTag
                .Include(s => s.TodoItem)
                .Include(s => s.Tag)
                .FirstOrDefaultAsync(m => m.TodoItemID == todoItemID && m.TagID == tagID);
            if (studentTag == null)
            {
                return NotFound();
            }

            return View(studentTag);
        }

        // POST: TodoItemTagAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("TodoItemID,TagID")] TodoItemTag studentTag)
        {
            _context.TodoItemTag.Remove(studentTag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoItemTagExists(int id)
        {
            return _context.TodoItemTag.Any(e => e.TodoItemID == id);
        }
    }
}
