using Logic.DAL;
using Logic.Entities;
using Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Services
{
   public class UserService: ILogicUser
    {
        string
        firstName = String.Empty,
        lastName = String.Empty,
        username = String.Empty,
        password = String.Empty;

        DateTime birthDay,
         dateOfEmp;

        bool
        engine = false,
        tire = false,
        brakes = false,
        kaross = false,
        window = false;
        public void SetUser(string user)
        {
            var key = from x in ActivClasses.loginListUser
                      where x.Username == user
                      select x.UserID;
                      key.ToList();

            foreach (var users in key)
            {
                ActivClasses.UserKey = users;
            }
        }
        public List<Mechanic> GetMechanic(string id)
        {

            foreach (var item in ActivClasses.mechanicDictionary[id])
            {
                firstName = item.FirstNameOfMechanic;
                lastName = item.LastNameOfMechanic;
                birthDay = item.BirthdayOfMechanic;
                dateOfEmp = item.DateOfEmploymentOfMechanic;
                engine = item.Engine;
                tire = item.Tire;
                brakes = item.Brakes;
                kaross = item.Kaross;
                window = item.Window;
            }
            List<Mechanic> DeklareraLista = new List<Mechanic>();
            DeklareraLista.Add(new Mechanic
            {
                FirstNameOfMechanic = firstName,
                LastNameOfMechanic = lastName,
                BirthdayOfMechanic = birthDay,
                DateOfEmploymentOfMechanic = dateOfEmp,
                Engine = engine,
                Tire = tire,
                Brakes = brakes,
                Kaross = kaross,
                Window = window,
            });
            return DeklareraLista;
        }
        public void Changes
        (
        bool Engine,
        bool Tire,
        bool Window,
        bool Brakes,
        bool Kaross)
        {
            foreach (var item in ActivClasses.mechanicDictionary[ActivClasses.UserKey])
            {
                item.Engine = Engine;
                item.Tire = Tire;
                item.Brakes = Brakes;
                item.Kaross = Kaross;
                item.Window = Window;
            }
            
        }
    }
}
