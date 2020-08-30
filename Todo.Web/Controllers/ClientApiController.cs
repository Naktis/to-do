using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.ApiClient;

namespace Todo.Web.Controllers
{
    public class ClientApiController : Controller
    {
        private readonly ClientsClass clientApi;

        public ClientApiController(ClientsClass clientApi)
        {
            this.clientApi = clientApi;
        }
        // GET: ClientApi
        public async Task<IActionResult> Index()
        {
            return View(await clientApi.ApiClientsGetAsync());
        }
    }
}
