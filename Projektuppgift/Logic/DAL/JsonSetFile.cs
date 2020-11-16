using Logic.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Logic.DAL
{
   public class JsonSetFile: JsonFile
    {
        public void SetJson()
        {
            SaveList(pathAdmin, ActivClasses.loginListAdmin);
            SaveList(pathUser, ActivClasses.loginListUser);
            SaveList(pathVehicles, ActivClasses.ListOfVehicles);
            SaveDictionary(pathMechanic, ActivClasses.mechanicDictionary);
            SaveDictionary(pathOrder, ActivClasses.orderDictionary);
        }
        private void SaveDictionary<T>(string path, Dictionary<string,T> dictonary  )
        {
            FileStream fileStream = File.OpenWrite(path);
            fileStream.SetLength(0);
            fileStream.Close();
            if (dictonary.Count != 0)
            {
                string Json = JsonSerializer.Serialize(dictonary);
                fileStream = File.OpenWrite(path);
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(Json);

                }
            }
            else{ File.Delete(path);}
        }
        private void SaveList<T>(string path,List<T> list )
        {
            FileStream fileStream = File.OpenWrite(path);
            fileStream.SetLength(0);
            fileStream.Close();

            if (list.Count != 0)
            {
                string Json = JsonSerializer.Serialize(list);
                fileStream = File.OpenWrite(path);
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(Json);
                }
            }
            else {File.Delete(path); }
        } 
    }
}

