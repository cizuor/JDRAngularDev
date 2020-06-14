using JDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.ViewModel
{
    public class NewUser
    {
        public int Id { get; set; }
        public String Pseudo { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }


        public NewUser()
        {
        }
        public NewUser(User user)
        {
            this.Id = user.Id;
            this.Pseudo = user.Pseudo;
            this.Email = user.Email;
            this.Password = "";
        }



    }
}
