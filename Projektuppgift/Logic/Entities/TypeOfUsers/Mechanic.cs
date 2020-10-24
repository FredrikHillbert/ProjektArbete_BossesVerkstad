using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
   public class Mechanic : User
    {
        public string MechanicName { get; set; }

        public DateTime MechanicBirthDay { get; set; }

        public DateTime MechanicDateOfHire { get; set; }




        public Mechanic()
        {
            IsAdmin = false;
        }




    }
}
