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
    public class TodoItemTagDBController : Controller
    {
        private readonly ITodoItemTagServiceAsync provider;
        private readonly IMapper mapper;
        private readonly IDataServiceAsync<TagVo> tagProvider;
        private readonly IDataServiceAsync<TodoItemVo> todoItemProvider;

        public TodoItemTagDBController(IMapper mapper, ITodoItemTagServiceAsync provider, IDataServiceAsync<TagVo> tagProvider, IDataServiceAsync<TodoItemVo> todoItemProvider)
        {
            this.provider = provider;
            this.mapper = mapper;
            this.tagProvider = tagProvider;
            this.todoItemProvider = todoItemProvider;
        }

        // GET: TodoItemTagDB
        public async Task<IActionResult> Index()
        {
            var itemTags = await provider.GetAll();
            return View(mapper.Map<IEnumerable<TodoItemTagViewModel>>(itemTags));
        }

        // GET: TodoItemTagDB/Details/5
        public async Task<IActionResult> Details(int? todoItemID, int? tagID)
        {
            if (todoItemID == null || tagID == null)
            {
                return NotFound();
            }

            var todoItemTag = await provider.Get((int)tagID, (int)todoItemID);

            if (todoItemTag == null)
            {
                return NotFound();
            }

            return View(mapper.Map<TodoItemTagViewModel>(todoItemTag));
        }

        // GET: TodoItemTagDB/Create
        public IActionResult Create()
        {
            ViewData["TagID"] = new SelectList(tagProvider.GetEnum(), "ID", "Name");
            ViewData["TodoItemID"] = new SelectList(todoItemProvider.GetEnum(), "ID", "Name");
            return View();
        }

        // POST: TodoItemTagDB/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TodoItemID,TagID")] TodoItemTagViewModel todoItemTag)
        {
            if (ModelState.IsValid)
            {
                var todoItemTagVo = mapper.Map<TodoItemTagVo>(todoItemTag);
                await provider.Add(todoItemTagVo);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TagID"] = new SelectList(tagProvider.GetEnum(), "ID", "ID", todoItemTag.TagID);
            ViewData["TodoItemID"] = new SelectList(todoItemProvider.GetEnum(), "ID", "Name", todoItemTag.TodoItemID);
            return View(todoItemTag);
        }

        // GET: TodoItemTagDB/Edit/5
        public async Task<IActionResult> Edit(int? todoItemID, int? tagID)
        {
            if (todoItemID == null || tagID == null)
            {
                return NotFound();
            }

            var todoItemTag = await provider.Get((int)tagID, (int)todoItemID);
            if (todoItemTag == null)
            {
                return NotFound();
            }
            ViewData["TodoItemID"] = new SelectList(todoItemProvider.GetEnum(), "ID", "Name", todoItemTag.TodoItemID);
            ViewData["TagID"] = new SelectList(tagProvider.GetEnum(), "ID", "Name", todoItemTag.TagID);
            ViewData["OldTodoItemID"] = todoItemID;
            ViewData["OldTagID"] = tagID;
            return View(mapper.Map<TodoItemTagViewModel>(todoItemTag));
        }

        // POST: TodoItemTagAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? oldTodoItemID, int? oldTagID, [Bind("TodoItemID,TagID")] TodoItemTagViewModel todoItemTag)
        {
            if (todoItemTag == null)
            {
                return NotFound();
            }
            var oldTodoItemTag = await provider.Get((int)oldTagID, (int)oldTodoItemID);
            if (oldTodoItemTag == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await provider.Edit(mapper.Map<TodoItemTagVo>(oldTodoItemTag), mapper.Map<TodoItemTagVo>(todoItemTag));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!provider.Exists(todoItemTag.TagID, todoItemTag.TodoItemID))
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
            ViewData["TodoItemID"] = new SelectList(todoItemProvider.GetEnum(), "ID", "Name", todoItemTag.TodoItemID);
            ViewData["TagID"] = new SelectList(tagProvider.GetEnum(), "ID", "Name", todoItemTag.TagID);
            return View(todoItemTag);
        }

        // GET: TodoItemTagDB/Delete/5
        public async Task<IActionResult> Delete(int? todoItemID, int? tagID)
        {
            if (todoItemID == null || tagID == null)
            {
                return NotFound();
            }

            var todoItemTag = await provider.Get((int)tagID, (int)todoItemID);
            
            if (todoItemTag == null)
            {
                return NotFound();
            }

            return View(mapper.Map<TodoItemTagViewModel>(todoItemTag));
        }

        // POST: TodoItemTagAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("TodoItemID,TagID")] TodoItemTagViewModel todoItemTag)
        {
            var todoItemTagVo = mapper.Map<TodoItemTagVo>(todoItemTag);
            await provider.Delete(todoItemTagVo);
            return RedirectToAction(nameof(Index));
        }
    }
}
