using Bangazon_TaskTracker.DAL;
using Bangazon_TaskTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bangazon_TaskTracker.Controllers
{
    [RoutePrefix ("api/Task")]
    public class TaskController : ApiController
    {
        TaskRepository Repo = new TaskRepository();

        // GET api/Task
        public IEnumerable<Task> Get()
        {
            return Repo.GetAll();
        }

        // POST api/Task
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Task value)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            } else
            {
                Repo.AddTask(value);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
        }

        // PUT api/Task/#
        [HttpPut, Route("{id}")]
        public HttpResponseMessage Put(int id, [FromBody]Task value)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            } else
            {
                Repo.UpdateTask(value);
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
        }
    
        // DELETE api/Task/#
        [HttpDelete, Route("{id}")]  
        public HttpResponseMessage Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            } else
            {
                Repo.RemoveTask(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }            
        }
    }
}