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
    [Route("api/race")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private Dal dal = new Dal();
        // GET: api/Race
        [HttpGet]
        public IEnumerable<NewRace> Get()
        {
            List<NewRace> retour = new List<NewRace>();
            List<Race> races = dal.GetAllRace();
            foreach (Race r in races)
            {
                retour.Add(new NewRace(r));
            }
            return retour;
        }

        // GET: api/Race/5
        [HttpGet("{id}", Name = "GetRaceId")]
        public NewRace Get(int id)
        {
            return new NewRace(dal.GetRaceById(id));
        }

        [Route("[action]")]
        [HttpGet]
        public NewRace GenerateNewRace()
        {
            Race r = new Race();
            r.InitNewRace();
            NewRace newRace = new NewRace(r); 
            return newRace;
        }

        // POST: api/Race
        [HttpPost]
        public void Post([FromBody] NewRace newRace)
        {
            dal.AddRace(newRace);

        }

        [Route("[action]")]
        [HttpPost]
        public void UpdateRace(NewRace newRace)
        {
            dal.MajRace(newRace);
        }

        /*// PUT: api/Race/5
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
