using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model {

    public class PasswordHasher {
        public const int SALT_SIZE = 24;
        public const int HASH_SIZE = 24;
        public const int ITERATIONS = 10000;
        public string HashedPassword { get; set; }
        public string PasswordSalt { get; set; }

        public PasswordHasher() {
        }

        public void CreateHash(string password) {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

            byte[] salt = new byte[SALT_SIZE];

            //generate salt
            provider.GetBytes(salt);

            // generate object containing hash and salt
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, ITERATIONS);

            // set the properties
            HashedPassword = Convert.ToBase64String(pbkdf2.GetBytes(HASH_SIZE));
            PasswordSalt = Convert.ToBase64String(pbkdf2.Salt);
        }

        public string GetHash(string salt, string password) {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

            // convert salt to byte array
            byte[] byteSalt = Convert.FromBase64String(salt);

            // get the object containing hash using the provided salt and password
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, byteSalt, ITERATIONS);

            // return hashed password
            return Convert.ToBase64String(pbkdf2.GetBytes(HASH_SIZE));
        }
    }
}