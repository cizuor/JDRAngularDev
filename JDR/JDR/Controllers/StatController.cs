using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDR.Model;
using JDR.Model.Statistique;
using JDR.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JDR.Controllers
{
    [Route("api/stat")]
    [ApiController]
    public class StatController : ControllerBase
    {

        private Dal dal = new Dal();
        // GET: api/Stat
        [HttpGet]
        public IEnumerable<NewStat> Get()
        {
            List<NewStat> retour = new List<NewStat>();
            foreach(Stat s in dal.GetAllStat())
            {
                retour.Add(new NewStat(s));
            }
            return retour;

        }
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Mini> GetLight()
        {
            List<Mini> retour = new List<Mini>();
            foreach (Stat s in dal.GetAllStatLimited())
            {
                retour.Add(new Mini {Id = s.Id,Nom = s.Nom });
            }
            return retour;

        }


        // GET: api/Stat/5
        [HttpGet("{id}", Name = "Get")]
        public NewStat Get(int id)
        {

            return new NewStat( dal.GetStatById(id));
        }

        //https://localhost:44302/api/stat/GetUtilByIdStat/13
        [Route("[action]/{id}")]
        [HttpGet]
        public IEnumerable<NewStatUtil> GetUtilByIdStat(int id)
        {
            Stat s = dal.GetStatById(id);
            List<NewStatUtil> retour = new List<NewStatUtil>();
            foreach (StatUtil su in s.StatUtils)
            {
                retour.Add(new NewStatUtil(su));
            }
            return retour;
        }

        // POST: api/Stat
        [HttpPost]
        public void Post( NewStat newStat)
        {

            Stat stat = new Stat
            {
                Nom = newStat.Nom,
                Definition = newStat.Definition,
                Stats = newStat.Stats,
                Type = newStat.Type
            };
            foreach(NewStatUtil su in newStat.StatUtils)
            {
                StatUtil statUtil = new StatUtil {Valeur = su.Valeur };
                Stat statUse = dal.GetStatById(su.StatUtile);
                statUtil.StatUtile = statUse;
                if(stat.StatUtils == null)
                {
                    stat.StatUtils = new List<StatUtil>();
                }
                stat.StatUtils.Add(statUtil);

            }
            dal.AddStat(stat);
        }


        [Route("[action]")]
        [HttpPost]
        public void UpdateStat(NewStat newStat)
        {
            dal.MajStat(newStat);
            //dal.AddStat(stat);
        }




        /*// PUT: api/Stat/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
