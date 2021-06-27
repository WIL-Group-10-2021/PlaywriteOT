using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using PlaywriteOT.Models;

namespace PlaywriteOT.Services
{
    public class FirebaseService
    {
        public string DBString = "https://bookingsdb-cebee-default-rtdb.europe-west1.firebasedatabase.app/";
        public readonly FirebaseClient _firebaseClient;

        public FirebaseService()
        {
            _firebaseClient = new FirebaseClient(DBString);
        }


        public async Task<User> FindUser(string email, string passw)
        {
            var dbUser = await _firebaseClient.Child("Users").OrderBy("email").EqualTo(email).EqualTo(passw).OnceSingleAsync<User>();    //finds user         
            return dbUser;
        }

        public async Task<bool> CreateUser(User newDBUser)
        {
            var dbUser = await _firebaseClient.Child("Users").OrderBy("email").EqualTo(newDBUser.Email).EqualTo(newDBUser.Email).OnceSingleAsync<User>();    //finds user  
            
            if (dbUser != null) //checks for duplicate emails
            {
                return false;   //if user already exists 
            }


            FirebaseObject<User> fo =  await _firebaseClient.Child("Users").PostAsync(newDBUser);  //posts new User
            if (fo.Key != null) //if posted successfully
            {
                return true;
            }
            return false;
        }

        public async Task<FirebaseObject<User>> UpdateUser(UserVM newUser, string passw)
        {
            //find current user
            //get current user


            User newDBUser = new User
            {
                FirstName = newUser.FName,
                LastName = newUser.LName,
                Email = newUser.Email,
                UPassword = bytesPass,
                USalt = bytesSalt,
                URole = newUser.URole
            };

            FirebaseObject<User> fo = await _firebaseClient.Child("Users/").PostAsync(newDBUser);
            return fo;
        }
    }
}
