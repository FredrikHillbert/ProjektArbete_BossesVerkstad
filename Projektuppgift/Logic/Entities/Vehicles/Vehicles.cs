using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Vehicles
{
   public abstract class Vehicles
    {

        public  string ModellName { get; set; }

        public int Matare { get; set; }

        public string RegNummer { get; set; }

        public DateTime RegDate { get; set; }

        public string Fuel { get; set; }

    }
}
