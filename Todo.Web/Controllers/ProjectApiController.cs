using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.ApiClient;

namespace Todo.Web.Controllers
{
    public class ProjectApiController : Controller
    {
        private readonly ClientsClass api;

        public ProjectApiController(ClientsClass api)
        {
            this.api = api;
        }
        // GET: ProjectApi
        public async Task<IActionResult> Index()
        {
            return View(await api.ApiProjectsGetAsync());
        }
    }
}
