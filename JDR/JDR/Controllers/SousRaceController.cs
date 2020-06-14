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
    [Route("api/sousRace")]
    [ApiController]
    public class SousRaceController : ControllerBase
    {
        private Dal dal = new Dal();
        // GET: api/SousRace
        [HttpGet]
        public IEnumerable<NewSousRace> Get()
        {
            List<NewSousRace> retour = new List<NewSousRace>();
            List<SousRace> sousRaces = dal.GetAllSousRace();
            foreach(SousRace sr in sousRaces)
            {
                retour.Add(new NewSousRace(sr));
            }


            return retour;
        }

        // GET: api/SousRace/5
        [HttpGet("{id}", Name = "GetSousRaceId")]
        public NewSousRace Get(int id)
        {
            return new NewSousRace(dal.GetSousRaceById(id));
        }

        [Route("[action]")]
        [HttpGet]
        public NewSousRace GenerateNewSousRace()
        {
            SousRace sr = new SousRace();
            sr.InitNewSousRace();
            NewSousRace newSousRace = new NewSousRace(sr);
            return newSousRace;
        }



        // POST: api/SousRace
        [HttpPost]
        public void Post([FromBody] NewSousRace newSousRace)
        {
            dal.AddSousRace(newSousRace);
        }

        [Route("[action]")]
        [HttpPost]
        public void UpdateSousRace([FromBody] NewSousRace newSousRace)
        {
            dal.MajSousRacee(newSousRace);
        }





        // PUT: api/SousRace/5
        /*[HttpPut("{id}")]
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
