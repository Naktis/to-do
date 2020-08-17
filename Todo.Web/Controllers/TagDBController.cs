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

namespace Todo.Web.Controllers
{
    public class TagDBController : Controller
    {
        private readonly Data.Context.AppContext _context;
        private readonly IMapper mapper;

        public TagDBController(Data.Context.AppContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: TagDB
        public async Task<IActionResult> Index()
        {
            var tags = await _context.Tags.ToListAsync();
            return View(mapper.Map<IEnumerable<TagViewModel>>(tags));
        }

        // GET: TagDB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(mapper.Map<TagViewModel>(tag));
        }

        // GET: TagDB/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TagDB/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] TagDao tag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mapper.Map<TagViewModel>(tag));
        }

        // GET: TagDB/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(mapper.Map<TagViewModel>(tag));
        }

        // POST: TagDB/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] TagDao tag)
        {
            if (id != tag.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.ID))
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
            return View(mapper.Map<TagViewModel>(tag));
        }

        // GET: TagDB/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(mapper.Map<TagViewModel>(tag));
        }

        // POST: TagDB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.ID == id);
        }
    }
}
