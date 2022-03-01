using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;

namespace VirtualPatient_API.Controllers
{
    public class DialogueController : ApiController
    {
        // GET api/values
        public IEnumerable<Dialogue> Get()
        {
            using (DummyDBEntities dummy = new DummyDBEntities())
            {               
                return dummy.Dialogues.ToList(); 
            }
        }

        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            using (DummyDBEntities entities = new DummyDBEntities())
            {
                var entity = entities.Dialogues.FirstOrDefault(e => e.id == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Lab not found " + id);
                }
            }
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
