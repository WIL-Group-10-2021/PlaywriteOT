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
        public string DBString = "https://playwriteot-9e0fb-default-rtdb.europe-west1.firebasedatabase.app/";
        public readonly FirebaseClient _firebaseClient;

        public FirebaseService()
        {
            _firebaseClient = new FirebaseClient(DBString);
        }


        public async Task<User> FindUser(string email, string passw)
        {
            try
            {
                var dbUser = (await _firebaseClient.Child("Users")
                    .OnceAsync<User>())
                    .Where(u => u.Object.Email == email)
                    .Select(x => new User
                    {
                        FirstName = x.Object.FirstName,
                        LastName = x.Object.LastName,
                        Email = x.Object.Email,
                        Admin = x.Object.Admin,
                        UPassword = x.Object.UPassword,
                        USalt = x.Object.USalt,

                    }).FirstOrDefault();    //finds user and converts to DBUser        
                return dbUser;
            }
            catch (Exception)
            {

                return new User();
            }

        }

        public async Task<bool> CreateUser(User newDBUser)
        {
            try
            {
                var dbUser = (await _firebaseClient.Child("Users").OnceAsync<User>()).Where(u => u.Object.Email == newDBUser.Email).FirstOrDefault();    //finds user  

                if (dbUser.Object.Email != null) //checks for duplicate emails
                {
                    return false;   //if user already exists 
                }

                await _firebaseClient.Child("Users").PostAsync(newDBUser);  //posts new User
                return true;
            }
            catch (Exception e)
            {
                string mes = e.Message;
                string mes1 = mes;

                throw;
            }

        }

        public async Task<bool> RemoveSubscription(string email) //deleted email from mailing list
        {
            try
            {
                await _firebaseClient.Child("Newsletter").OrderBy("email").EqualTo(email).DeleteAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddSubscription(string email) //adds email to mailing list
        {
            var fo = await _firebaseClient.Child("Newsletter").PostAsync(email);  //posts new email
            if (fo.Key != null) //if posted successfully
            {
                return true;
            }
            return false;

        }

        public async Task<bool> UpdateUser(User updateUser)
        {
            //find current user
            //get current user
            //
            try
            {
                await _firebaseClient.Child("Users").OrderBy("email").EqualTo(updateUser.Email).PutAsync(updateUser);
                return true;
            }
            catch (Exception)
            {

                return false;
            }



        }
    }
}
