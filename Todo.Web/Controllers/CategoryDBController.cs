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
using Todo.Business.Services;
using Todo.Web.ViewModels;

namespace Todo.Web.Controllers
{
    public class CategoryDBController : Controller
    {
        private readonly IDataProviderAsync<CategoryVo> provider;
        private readonly IMapper mapper;

        public CategoryDBController(IMapper mapper, IDataProviderAsync<CategoryVo> provider)
        {
            this.provider = provider;
            this.mapper = mapper;
        }

        // GET: CategoryDB
        public async Task<IActionResult> Index()
        {
            var categories = await provider.GetAll();
            return View(mapper.Map<IEnumerable<CategoryViewModel>>(categories));
        }

        // GET: CategoryDB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await provider.Get((int)id);
            if (category == null)
            {
                return NotFound();
            }
            return View(mapper.Map<CategoryViewModel>(category));
        }

        // GET: CategoryDB/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryDB/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                var categoryVo = mapper.Map<CategoryVo>(category);
                await provider.Add(categoryVo);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: CategoryDB/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await provider.Get((int)id);
            if (category == null)
            {
                return NotFound();
            }
            return View(mapper.Map<CategoryViewModel>(category));
        }

        // POST: CategoryDB/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] CategoryViewModel category)
        {
            if (id != category.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var categoryVo = mapper.Map<CategoryVo>(category);
                    await provider.Edit(categoryVo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!provider.Exists(category.ID))
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
            return View(category);
        }

        // GET: CategoryDB/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await provider.Get((int)id);
            if (category == null)
            {
                return NotFound();
            }

            return View(mapper.Map<CategoryViewModel>(category));
        }

        // POST: CategoryDB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await provider.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
