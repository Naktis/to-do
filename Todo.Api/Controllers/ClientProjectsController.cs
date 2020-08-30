using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Data;
using Todo.Api.Models;

namespace Todo.Api.Controllers
{
    [Route("api/Clients/{clientID}/Projects")]
    [ApiController]
    public class ClientProjectsController : ControllerBase
    {
        private readonly TodoApiContext _context;

        public ClientProjectsController(TodoApiContext context)
        {
            _context = context;
        }

        // Lists client projects
        // GET: api/Clients/1/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetClientProjects(int clientID)
        {
            return await _context.Projects.Where(m => m.ClientID == clientID).ToListAsync();
        }

        // POST: api/Clients/5/Projects?name=John
        [HttpPost]
        public async Task<ActionResult> PostClientProject(int clientID, string name)
        {
            Client client = await _context.Clients.FindAsync(clientID);

            if (client == null)
            {
                return NotFound();
            }

            Project project = new Project() { Name = name, ClientID = clientID, Client = client };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Clients/1/Projects/3
        [HttpDelete("{projectID}")]
        public async Task<ActionResult<Project>> DeleteClientProject(int projectID)
        {
            var project = await _context.Projects.FindAsync(projectID);

            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return project;
        }

        // PUT: api/Clients/1/Projects/3
        [HttpPut("{projectID}")]
        public async Task<ActionResult> PutEditProjectClient(int clientID, int projectID)
        {
            var project = await _context.Projects.FindAsync(projectID);

            if (project == null)
            {
                return NotFound();
            }

            project.ClientID = clientID;

            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
