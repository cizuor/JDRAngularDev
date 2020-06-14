using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDR.Model;
using JDR.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JDR.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private Dal dal = new Dal();
        // GET: api/User
        [HttpGet]
        public IEnumerable<NewUser> Get()
        {
            List<NewUser> retour = new List<NewUser>();
            foreach (User u in dal.GetAllUser())
            {
                retour.Add(new NewUser(u));
            }
            return retour;
        }

        // GET: api/User/5
        [HttpGet("{mail}/{pass}", Name = "GetUser")]
        public NewUser Get(String mail,String pass)
        {
            User u = dal.Autentifie(mail, pass);
            if(u != null)
            {
                return new NewUser(u);
            }
            return new NewUser {Id = -1};
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] NewUser newUser)
        {
            dal.AddUser(newUser);
        }

        // PUT: api/User/5
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
