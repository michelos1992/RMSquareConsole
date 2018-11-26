using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            string nameFile;
            try
            {
                //Console.WriteLine("Podaj nazwę pliku:");
                nameFile = "Temperatura.json"; //Console.ReadLine(); 

                RMSquare TemperValues = new RMSquare();
                TemperValues.ReadFile(nameFile);
                TemperValues.DeserializeJson();
                TemperValues.RMSFirstPart();

                TemperValues.WriteFile();

                Console.WriteLine("Plik zapisany");
            }catch(Exception ex)
            {
                Console.WriteLine("Cos poszlo nie tak. Rodzaj błędu: " + ex.Message);
            }
            
            Console.ReadLine();
        }
        
    }
}