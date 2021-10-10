using PlaywriteOT_v3.Models;
using PlaywriteOT_v3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PlaywriteOT_v3.Utilities
{
    public class AuthHold
    {
        // empty constructor
        private AuthHold()
        {

        }

        public static AuthHold instance = null;

        public static AuthHold Instance //singleton
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthHold();
                }
                return instance;
            }
        }

        public FirebaseService fireServ = new FirebaseService();

        public User dbUser { get; set; }
        public UserVM currentUser { get; set; }

        /// <summary>
        /// Searches firebase for specified email through FirebaseService, 
        /// and validates specified password if user was found. 
        /// If user found, local UserVm object is populated.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="passw"></param>
        /// <returns> A <strong>boolean</strong> depending on successful and validated login </returns>
        public async Task<bool> LoginUser(string email, string passw)
        {
            dbUser = await fireServ.FindUser(email); //find user from firebase
            if (dbUser.Email == null) //if user doesnt exists
            {
                return false;  //if no user
            }

            //feeds user salt to HMAC
            using var hmac = new HMACSHA512(dbUser.USalt);
            //computes hash of inputted password
            byte[] enteredPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(passw));

            //incase there is a length mismatch in password comparisson
            try
            {
                //checks entered byte array against stored
                for (int i = 0; i < dbUser.UPassword.Length; i++)
                {
                    if (enteredPass[i] != dbUser.UPassword[i])
                    {
                        //if incorrect password
                        return false;
                    }
                }

                //creates new VM User for local use
                currentUser = new UserVM
                {
                    FName = dbUser.FirstName,
                    LName = dbUser.LastName,
                    Email = dbUser.Email,
                    Admin = dbUser.Admin
                };

                return true;

            }
            catch (Exception)
            {
                //if incorrect password
                return false;
            }
        }

        //registers new user
        public async Task<bool> RegisterUser(UserVM newUser, string passw)
        {

            try //creates new user
            {
                using var hmac = new HMACSHA512();
                //creates new hash based on user password
                byte[] bytesPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(passw));
                //gets salt
                byte[] bytesSalt = hmac.Key; 

                User newDBUser = new User
                {
                    FirstName = newUser.FName,
                    LastName = newUser.LName,
                    Email = newUser.Email,
                    UPassword = bytesPass,
                    USalt = bytesSalt,
                    Admin = true
                }; //creates firebase user object

                //returns true if successful
                return await fireServ.CreateUser(newDBUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        public async Task<bool> SubscribeToNews(string email)
        {
            return await fireServ.AddSubscription(email);
        }
        public async Task<bool> UnsubscribeToNews(string email)
        {
            return await fireServ.RemoveSubscription(email);
        }
        /*
        public async Task<bool> UpdateUserDetails(UserVM updatedUser)
        {
            return await fireServ.UpdateUser(updatedUser);
        }
        */
    }
}
