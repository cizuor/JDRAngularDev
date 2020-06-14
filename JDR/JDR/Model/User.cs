using JDR.Model.Personnage;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Model
{
    public class User
    {
        public int Id { get; set; }
        public String Pseudo { get; set; }
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
        [DataType(DataType.Password)]
        public String Password { get; set; }
        public List<Perso> Persos { get; set; }
        private string salt = "mon sel pour le JDR pour crypter tout les mots de passe le mieux possible ";

        public User()
        {
        }

        private String Hashe(String password)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(salt);
            Console.WriteLine($"Salt: {Convert.ToBase64String(bytes)}");

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: bytes,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            Console.WriteLine($"Hashed: {hashed}");
            return hashed;
        }

        public User(string pseudo, string email, string password)
        {
            Pseudo = pseudo;
            Email = email;
            Password = Hashe(password);
        }


        public Boolean VerifMdp(String pass)
        {
            if (Hashe(pass) == this.Password)
            {
                return true;
            }
            return false;
        }
    }
}
