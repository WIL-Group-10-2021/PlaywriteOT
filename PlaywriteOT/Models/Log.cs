using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaywriteOT.Models
{
    public class Log
    {
        public string LogLevel { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogLocation { get; set; }
        public string LogMessage { get; set; }

        public Log(string location, string message)
        {
            LogLevel = "INFO";
            LogDateTime = DateTime.Now; //local time
            LogLocation = location;
            LogMessage = message;
        }

        override
            public string ToString() //format to store log in text file
        {
            return (LogLevel + " - " + LogDateTime + " - " + LogLocation + " - " + LogMessage);
        }




        //optimization

        /*public void Info(string location) //info log
        {
            LogLevel = "INFO";
            AddDefaultData(location);
            
            *//*LogDateTime = DateTime.Now; //local time
            LogLocation = location;*//*
        }*/

        /* public void Error(string location) //error log
         {
             LogLevel = "ERROR";
             AddDefaultData(location);
         }

         public void Warn(string location) //warn log
         {
             LogLevel = "WARN";
             AddDefaultData(location);
         }

         public void Debug(string location) //debug log
         {
             LogLevel = "WARN";
             AddDefaultData(location);
         }*/

        //default info - adds the date 
        /*public void AddDefaultData(string location)
        {
            LogDateTime = DateTime.Now; //local time
            LogLocation = location;
        }*/


    }
}