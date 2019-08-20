using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cinema.Controllers
{
    public class PracaController : ApiController
    {
        // GET: api/Praca
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Praca/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Praca
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Praca/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Praca/5
        public void Delete(int id)
        {
        }
    }
}
