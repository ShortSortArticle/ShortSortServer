using Bll;
using Bll.KodFunction;
using Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Models;

namespace WebApi.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ArticalController : ApiController
    {

        // GET: api/Artical
        //public object Get()
        //{
        //    return BllArtical.getAllArticals();
        //}

        // GET: api/Artical/5
        public List<DtoArtical> Get(int id)
        {//ArtAlbum.tsx
            return HeadFunction.ShowAllArtToCat(id);
        }

        [HttpPost]
        // POST: api/Artical
        public KeyValuePair<List<ArtWithCat>, List<ArtWithCat>> Post([FromBody] ArtToSearch value)
        {//SearchArt.tsx
             return HeadFunction.ShowArtByName(value.Name, value.CatsId);
        }

        // PUT: api/Artical/5
        public void Put([FromBody] ArtWithCat value)
        {//searchArt.tsx
            try
            {
                HeadFunction.AddPointsToCat(value.Id, value.CatId);
            }
            catch { };
        }


        // DELETE: api/Artical/5
        //public string Delete(int id)
        //{

        //}
    }
}
