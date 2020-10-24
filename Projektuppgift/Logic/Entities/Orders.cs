using Logic.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    public class Orders
    {
        public List<Orders> newOrder = new List<Orders>();
        public Dictionary<string, Orders> dicOrder;

        public string DescriptionOfProb { get; set; }

        public  bool StatusOnOrder { get; set; }

        public Orders(string desc, bool status, string addMechanic, bool brakes, bool engine, bool window, bool tires)
        {

        }

        // en metod för att skapa och spara en ny order. 

        public void CreateNewOrder()
        {
            Mechanic mechanic = new Mechanic();
            Componets componets = new Componets();
            bool creatingNewOrder = true;

            while (creatingNewOrder)
            {
                string descripton = DescriptionOfProb;
                bool status = StatusOnOrder;
                string addMechanic = mechanic.MechanicName;

                bool brakesIsTheProblem = false;
                bool engineIsTheProblem = false;
                bool windowIsTheProblem = false;
                bool tiresIsTheProblem = false;

                if (componets.Brakes)
                {
                    brakesIsTheProblem = componets.Brakes;
                }

                if (componets.Engine)
                {
                    engineIsTheProblem = componets.Engine;
                }

                if (componets.Tire)
                {
                    tiresIsTheProblem = componets.Tire;
                }
                if (componets.Window)
                {
                    windowIsTheProblem = componets.Window;
                }



                newOrder.Add(new Orders(descripton, status, addMechanic, tiresIsTheProblem, engineIsTheProblem, windowIsTheProblem, brakesIsTheProblem));

                creatingNewOrder = false;
            }

        }

        




    }
}
