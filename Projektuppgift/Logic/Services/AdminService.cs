using Logic.DAL;
using Logic.Entities;
using Logic.Interface;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace Logic.Services
{
    public class AdminService : ILogic
    {
        string
        firstName = String.Empty,
        lastName = String.Empty,
        username = String.Empty,
        password = String.Empty,
        orderDescription = String.Empty,
        typeOfVehicle = String.Empty,
        ModelName = String.Empty,
        Regnum = String.Empty,
        matare = String.Empty,
        regDate = String.Empty,
        specificQ1 = String.Empty,
        specificQ2 = String.Empty,
        typeOfFuel = String.Empty,
        assignedMechanic = String.Empty;

        DateTime birthDay,
                 dateOfEmp;

        bool
        engine = false,
        tire = false,
        brakes = false,
        kaross = false,
        window = false,
        status = false;



        public bool ActivUser(string id)
        {
            if (ActivClasses.mechanicDictionary.ContainsKey(id)) {return true;}
            else{ return false;}
        }
        public bool ActivOrder(string id)
        {
            if (ActivClasses.orderDictionary.ContainsKey(id)) { return true; }
            else { return false; }
        }

        public void DeleteMechanic(string id)
        {
            ActivClasses.mechanicDictionary.Remove(id);
            int index = ActivClasses.loginListUser.FindIndex(x => x.UserID == id);
            if(index != -1)
            ActivClasses.loginListUser.RemoveAt(index);
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
        public List<string> GetKey()
        {
            string savekey = "";
            List<string> DeklareraLista = new List<string>();

            Dictionary<string, List<Mechanic>>.KeyCollection keys = ActivClasses.mechanicDictionary.Keys;
            foreach (string key in keys)
            {
                savekey = key;
                foreach (var item in ActivClasses.mechanicDictionary[key])
                {
                    string add = ($"ID: {savekey}" +
                        $"\nName: {item.FirstNameOfMechanic} {item.LastNameOfMechanic}");
                    DeklareraLista.Add(add);
                }
            }

            return (DeklareraLista);
        }
        public List<string> GetOnlyKey()
        {
            string savekey = string.Empty;
            List<string> DeklareraLista = new List<string>();

            Dictionary<string, List<Mechanic>>.KeyCollection keys = ActivClasses.mechanicDictionary.Keys;
            foreach (string key in keys)
            {
                savekey = key;
                foreach (var item in ActivClasses.mechanicDictionary[key])
                {
                    DeklareraLista.Add(savekey);
                }
            }

            return (DeklareraLista);
        }
        public List<string> GetActivUser()
        {
            List<string> DeklareraLista = new List<string>();

            foreach (var item in ActivClasses.loginListUser)
            {
                string add = ($"ID: {item.UserID}" +
                    $"\nName: {item.Username}" +
                    $"\nPassword: {item.Password}");
                DeklareraLista.Add(add);
            }
            return (DeklareraLista);
        }
        public List<string> GetVehicles()
        {
            List<string> showVehicles = new List<string>();

            foreach (var item in ActivClasses.ListOfVehicles)
            {

                showVehicles.Add(item);

            }
            return showVehicles;
        }
        public bool Login(string username, string password)
        {
            return ActivClasses.loginListAdmin.Exists(user => user.Username.Equals(username) && user.Password.Equals(password));
        }
        public bool LoginUser(string username, string password)
        {
            return ActivClasses.loginListUser.Exists(user => user.Username.Equals(username) && user.Password.Equals(password));
        }
        public void NewMechanic(string id, List<Mechanic> listOfMechanic)
        {
            ActivClasses.mechanicDictionary.Add(id, listOfMechanic);
        }

        public void NewUser(string username, string password, string id)
        {

            if (ActivClasses.mechanicDictionary.ContainsKey(id))//----------------------------------------------------------------------Ändra från lista till mechanic!
            {


                foreach (var item in ActivClasses.mechanicDictionary[id])
                {
                    item.Username = username;
                    item.Password = password;
                    item.UserID = id;
                }
            }
            ActivClasses.loginListUser.Add(new User { Username = username, Password = password, UserID = id });
        }
        public void DeletLogin(string id)
        {
            if (ActivClasses.mechanicDictionary.ContainsKey(id))//-------------------------Kollar efter användare finns
            {
                foreach (var item in ActivClasses.mechanicDictionary[id])
                {
                    item.Username = string.Empty;
                    item.Password = string.Empty;
                    item.UserID = string.Empty;
                }
                int index = ActivClasses.loginListUser.FindIndex(x => x.UserID == id);
                ActivClasses.loginListUser.RemoveAt(index);    
            }
        }
        public bool ValidLogin(string username, string password, string password2, string id)
        {
            if (Regex.IsMatch(username, @"^([\w-.]+)@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.)|(([\w-]+.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(]?)$") && password == password2 && id != null)
            {
                if (ActivClasses.loginListUser.Exists(x => x.Username.Equals(username))) { return false; }
                else { return true; }     
            }
            else { return false;}
        }

        public bool ValidMechanic(string firstName, string lastName, string dateOfBirth, string dateOfEmp, string id)
        {
            if (firstName != String.Empty && lastName != String.Empty && dateOfBirth != String.Empty && dateOfEmp != String.Empty && id != String.Empty)
            {
                if (Regex.IsMatch(dateOfBirth, @"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$") && Regex.IsMatch(dateOfEmp, @"^^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$"))
                {
                    string input = dateOfEmp;
                    DateTime dateTimeOfbirth = DateTime.Parse(input);
                    input = dateOfBirth;
                    DateTime dateTimeOfEmployment = DateTime.Parse(input);
                    return true;
                }
                else{ return false;}
            }
            else { return false; }
        }

        public bool ValidOrder(string orderDescription, string vehicle, string mechanic,
                               string modellName, string regNumber, string matare, string regDate, string typeOfFuel, string specificQOne, string specificQTwo)
        {
            if (orderDescription != String.Empty && vehicle != String.Empty && mechanic != String.Empty &&
                modellName != String.Empty && regNumber != String.Empty && matare != String.Empty && typeOfFuel != String.Empty && specificQOne != String.Empty && specificQTwo != String.Empty)
            {
                return true;
            }
   
            else { return false; }
        }
        // gör en metod för varje
        public List<string> GetMechanicForTheJob(string value)
        {
            List<string> listaAvKeys = GetOnlyKey();

            List<string> newListOfMechanics = new List<string>();

            for (int i = 0; i < listaAvKeys.Count; i++)
            {
                string key = listaAvKeys[i];

                foreach (var item in ActivClasses.mechanicDictionary[key])
                {

                    if (value == "Brakes" && item.Brakes == true && item.ActiveOrders < 2)
                    {
                        string Name = $"{item.UserID} {item.FirstNameOfMechanic} {item.LastNameOfMechanic}";

                        newListOfMechanics.Add(Name);

                    }
                    else if (value == "Tires" && item.Tire == true && item.ActiveOrders < 2)
                    {
                        string Name = $"{item.UserID} {item.FirstNameOfMechanic} {item.LastNameOfMechanic}";

                        newListOfMechanics.Add(Name);
                    }
                    else if (value == "Engine" && item.Engine == true && item.ActiveOrders < 2)
                    {
                        string Name = $"{item.UserID} {item.FirstNameOfMechanic} {item.LastNameOfMechanic}";

                        newListOfMechanics.Add(Name);
                    }
                    else if (value == "Window" && item.Window == true && item.ActiveOrders < 2)
                    {
                        string Name = $"{item.FirstNameOfMechanic} {item.LastNameOfMechanic}";

                        newListOfMechanics.Add(Name);
                    }
                    else if (value == "Kaross" && item.Kaross == true && item.ActiveOrders < 2)
                    {
                        string Name = $"{item.UserID} {item.FirstNameOfMechanic} {item.LastNameOfMechanic}";

                        newListOfMechanics.Add(Name);
                    }
                }
            }
            return newListOfMechanics;
        }

        public void NewOrder(string id, List<Orders> newOrder)
        {
            ActivClasses.orderDictionary.Add(id, newOrder);
        }
        public List<Orders> GetOrder(string id)
        {
            foreach (var item in ActivClasses.orderDictionary[id])
            {
                orderDescription = item.OrderDescription;
                typeOfVehicle = item.TypeOfVehicle;
                ModelName = item.ModelName;
                Regnum = item.RegNumber;
                matare = item.Matare;
                regDate = item.RegDate;
                specificQ1 = item.SpecificQuestionAboutVehicle1;
                specificQ2 = item.SpecificQuestionAboutVehicle2;
                engine = item.Engine;
                tire = item.Tire;
                brakes = item.Brakes;
                kaross = item.Kaross;
                window = item.BrokeWindow;
                typeOfFuel = item.Fuel;
                status = item.Status;
                assignedMechanic = item.Mechanic;

            }
            List<Orders> changedOrder = new List<Orders>();
            changedOrder.Add(new Orders
            {
                OrderDescription = orderDescription,
                TypeOfVehicle = typeOfVehicle,
                ModelName = ModelName,
                RegNumber = Regnum,
                Matare = matare,
                RegDate = regDate,
                SpecificQuestionAboutVehicle1 = specificQ1,
                SpecificQuestionAboutVehicle2 = specificQ2,
                Engine = engine,
                Tire = tire,
                Brakes = brakes,
                Kaross = kaross,
                BrokeWindow = window,
                Fuel = typeOfFuel,
                Status = status,
                Mechanic = assignedMechanic
            }) ;
            return changedOrder;
        }

        public List<string> GetOnlyOrdersKey()
        {
            string savekey = string.Empty;
            List<string> DeklareraLista = new List<string>();

            Dictionary<string, List<Orders>>.KeyCollection keys = ActivClasses.orderDictionary.Keys;
            foreach (string key in keys)
            {
                savekey = key;
                foreach (var item in ActivClasses.orderDictionary[key])
                {

                    DeklareraLista.Add(savekey);
                }
            }

            return (DeklareraLista);
        }

        public List<string> GetKeyForOrder()
        {
            string savekey = "";
            List<string> DeklareraLista = new List<string>();

            Dictionary<string, List<Orders>>.KeyCollection keys = ActivClasses.orderDictionary.Keys;
            foreach (string key in keys)
            {
                savekey = key;
                foreach (var item in ActivClasses.orderDictionary[key])
                {
                    string add = ($"ID: {savekey}" +
                        $"\nName: {item.OrderDescription} {item.TypeOfVehicle}");
                    DeklareraLista.Add(add);
                }
            }

            return (DeklareraLista);
        }








        public void GiveMechanicOrder(string name, List<Orders> newOrder)
        {
            List<string> listaAvKeys = GetOnlyKey();
            for (int i = 0; i < listaAvKeys.Count; i++)
            {
                string key = listaAvKeys[i];
                foreach (var item in ActivClasses.mechanicDictionary[key])
                {
                    if (name.Contains(item.LastNameOfMechanic))
                    {
                        item.ActiveOrders += 1;
                        try
                        {
                            ActivClasses.mechanicOrder.Add(key, newOrder);
                        }
                        catch (Exception)
                        {

                            ActivClasses.mechanicOrder2.Add(key, newOrder);
                        }
                    }
                }
            }

            

        }
    }
}


