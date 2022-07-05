using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HandleCategoryController : ApiController
    {
        // GET: api/HandleCategory
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/HandleCategory/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/HandleCategory
        public void Post([FromBody]Dto.DtoCategory value)
        {
            Bll.BllCategory.updateCat(value);
        }

        // PUT: api/HandleCategory/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/HandleCategory/5
        //public void Delete(int id)
        //{
        //}
    }
}
