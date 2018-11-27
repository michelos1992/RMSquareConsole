using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSquareConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            string nameFile;
            try
            {
                Console.WriteLine("Podaj nazwę pliku:");
                nameFile = Console.ReadLine(); //"Temperatura.json";

                RMSquare TemperValues = new RMSquare();
                TemperValues.ReadFile(nameFile);
                TemperValues.DeserializeJson();
                TemperValues.RMSFirstPart();

                TemperValues.WriteFile();

                Console.WriteLine("Plik zapisany");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cos poszlo nie tak. Rodzaj błędu: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}
