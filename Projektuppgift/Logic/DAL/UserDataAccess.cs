using Logic.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Logic.DAL
{
    /// <summary>
    /// HEj freddWard!
    /// HEJSAN HEJSAN HEJSAN
    /// hello
    /// </summary>
    public class UserDataAccess
    {
        private const string path = @"DAL\User.json";

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {

            string jsonString = File.ReadAllText(path);
            List<User> users = JsonSerializer.Deserialize<List<User>>(jsonString);

            
            return users;
            
        }

        //public List<User> CreateUsers()
        //{
        //    var newUser = new User();

        //    string jsonString = JsonSerializer.Serialize(newUser);
        //    File.WriteAllText(path, jsonString);



           

        //}






    }
}
