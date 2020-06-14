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
    [Route("api/classe")]
    [ApiController]
    public class ClasseController : ControllerBase
    {
        private Dal dal = new Dal();
        // GET: api/Classe
        [HttpGet]
        public IEnumerable<NewClasse> Get()
        {

            List<NewClasse> retour = new List<NewClasse>();
            List<Classe> classes = dal.GetAllClasse();
            foreach(Classe c in classes)
            {
                retour.Add(new NewClasse(c));
            }
            return retour;
        }

        // GET: api/Classe/5
        [HttpGet("{id}", Name = "GetClasse")]
        public NewClasse Get(int id)
        {

            Classe c = dal.GetClasseById(id);
            
            return new NewClasse(c);
        }

        [Route("[action]")]
        [HttpGet]
        public NewClasse GenerateNewClasse()
        {
            Classe c = new Classe();
            c.InitNewClasse();
            NewClasse newClasse = new NewClasse(c);
            return newClasse;
        }


        // POST: api/Classe
        [HttpPost]
        public void Post([FromBody] NewClasse newClasse)
        {
            dal.AddClasse(newClasse);
        }

        [Route("[action]")]
        [HttpPost]
        public void UpdateClasse([FromBody] NewClasse newClasse)
        {
            dal.MajClasse(newClasse);
        }


        /*  // PUT: api/Classe/5
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
