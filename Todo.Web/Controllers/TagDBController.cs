using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Todo.Business.Services;
using Todo.Data.Models;
using Todo.Web.ViewModels;

namespace Todo.Web.Controllers
{
    public class TagDBController : Controller
    {
        private readonly IDataServiceAsync<TagVo> provider;
        private readonly IMapper mapper;

        public TagDBController(IMapper mapper, IDataServiceAsync<TagVo> provider)
        {
            this.provider = provider;
            this.mapper = mapper;
        }

        // GET: TagDB
        public async Task<IActionResult> Index()
        {
            var tags = await provider.GetAll();
            return View(mapper.Map<IEnumerable<TagViewModel>>(tags));
        }

        // GET: TagDB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await provider.Get((int)id);
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
        public async Task<IActionResult> Create([Bind("ID,Name")] TagViewModel tag)
        {
            if (ModelState.IsValid)
            {
                var tagVo = mapper.Map<TagVo>(tag);
                await provider.Add(tagVo);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: TagDB/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await provider.Get((int)id);
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] TagViewModel tag)
        {
            if (id != tag.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var tagVo = mapper.Map<TagVo>(tag);
                    await provider.Edit(tagVo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!provider.Exists(tag.ID))
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
            return View(tag);
        }

        // GET: TagDB/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await provider.Get((int)id);
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
            await provider.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
