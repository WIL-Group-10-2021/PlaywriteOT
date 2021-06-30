using PlaywriteOT.Models;
using PlaywriteOT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PlaywriteOT.Utilities
{
    public class AuthHold
    {

        private AuthHold()
        {

        }

        public static AuthHold instance = null;
        public static AuthHold Instance  //singleton 
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


        public async Task<bool> LoginUser(string email, string passw)
        {
            dbUser = await fireServ.FindUser(email, passw);             //find user from firebase
            if ( dbUser.Email == null)                                        //if user doesnt exists
            {
                return false;  //if no user
            }

            using var hmac = new HMACSHA512(dbUser.USalt);                                              //feeds user salt to HMAC
            byte[] enteredPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(passw));                       //computes hash of inputted password


            try                                                                                         //incase there is a length mismatch in password comparisson
            {
                for (int i = 0; i < dbUser.UPassword.Length; i++)                                       //checks entered byte array against stored
                {
                    if (enteredPass[i] != dbUser.UPassword[i])
                    {
                        return false;                                                                   //if incorrect password
                    }
                }

                currentUser = new UserVM                                                                //creates new VM User for local use
                {
                    FName = dbUser.FirstName,
                    LName = dbUser.LastName,
                    Email = dbUser.Email,
                    Admin = dbUser.Admin
                };

                return true;

            }
            catch (Exception e)
            {
                return false; //if incorrect password
            }
        }
        public async Task<bool> RegisterUser(UserVM newUser, string passw) //registers new user
        {

            try //creates new user
            {
                using var hmac = new HMACSHA512();
                byte[] bytesPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(passw));
                byte[] bytesSalt = hmac.Key;

                User newDBUser = new User
                {
                    FirstName = newUser.FName,
                    LastName = newUser.LName,
                    Email = newUser.Email,
                    UPassword = bytesPass,
                    USalt = bytesSalt,
                    Admin = true
                };

                return await fireServ.CreateUser(newDBUser); //returns true if successfull         
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
/*        public async Task<bool> UpdateUserDetails(UserVM updatedUser)
        {
            return await fireServ.UpdateUser(updatedUser);
        }*/
    }
}
