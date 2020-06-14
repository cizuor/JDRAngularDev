using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDR.Model;
using JDR.Model.Personnage;
using JDR.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JDR.Controllers
{
    [Route("api/perso")]
    [ApiController]
    public class PersoController : ControllerBase
    {
        private Dal dal = new Dal();
        // GET: api/Perso
        [HttpGet("{id}")]
        public IEnumerable<NewPerso> Get(int id)
        {
            List<NewPerso> retour = new List<NewPerso>();
            foreach(Perso p in dal.GetPersoByUserId(id))
            {
                retour.Add(new NewPerso(p));
            }
            return retour;
        }

        // GET: api/Perso/5
        [HttpGet("{id}/{idp}", Name = "GetPersoById")]
        public NewPerso Get(int id, int idp)
        {
            Perso p = dal.GetPersoById(idp);
            List<Perso> listPerso = dal.GetPersoByUserId(id);
            if (listPerso.Contains(p))
            {
                return new NewPerso(p);
            }
            return null;
        }

        // POST: api/Perso
        [HttpPost]
        public void Post([FromBody] NewPerso newPerso)
        {
            dal.AddPerso(newPerso);
        }

        [Route("[action]")]
        [HttpPost]
        public void UpdatePerso([FromBody] NewPerso newPerso)
        {
            dal.MajPerso(newPerso);
        }

        [Route("[action]")]
        [HttpGet]
        public NewPerso GenerateNewPerso()
        {
            Perso p = new Perso();
            p.InitNewPerso();
            NewPerso newPerso = new NewPerso(p);
            return newPerso;
        }


        /*// PUT: api/Perso/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
