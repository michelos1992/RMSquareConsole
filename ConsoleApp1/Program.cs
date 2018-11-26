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
                Console.WriteLine("Podaj nazwę pliku:");
                nameFile = "Temperatura.json"; // Console.ReadLine();

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

            #region kodzik
            //DateTime date = new DateTime();
            //
            //List<TemperatureS> temperaValues2 = new List<TemperatureS>();
            //List<TemperatureValue> tempVal = new List<TemperatureValue>();
            //List<Data> godz = new List<Data>();
            //List<Data> RMSquare = new List<Data>();

            //StreamReader r = new StreamReader("Temperatura.json");
            //string json = r.ReadToEnd();

            //dynamic jsonObject = JsonConvert.DeserializeObject(json);

            //var dl = tempVal.FirstOrDefault();

            //var data = jsonObject.Data;
            //var ddd = JsonConvert.DeserializeObject<Dictionary<string, double>>(Convert.ToString(data));

            //int hhh=0;
            //int count = 0;
            //int countDay = DateTime.DaysInMonth(tempVal.FirstOrDefault().Date.Year, tempVal.FirstOrDefault().Date.Month);
            //int hh = dl.Date.Hour;

            //double suma = 0;

            //string path = "D:\\Dropbox\\moje\\asdf\\Projekt-rekuratacja\\ConsoleApp1\\bin\\Debug\\export.json";

            //foreach (var item in ddd)
            //{
            //    temperaValues2.Add(new TemperatureS()
            //    {
            //        Date = item.Key,
            //        TempValue = item.Value,
            //    });
            //}

            //foreach (var item in temperaValues2)
            //{
            //    tempVal.Add(new TemperatureValue()
            //    {
            //        Date = DateTime.ParseExact(item.Date, "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
            //                           System.Globalization.CultureInfo.InvariantCulture),
            //        TempValue = item.TempValue,
            //    });
            //}


            //date = tempVal.FirstOrDefault().Date;

            //if (hh < 23)
            //{
            //    hh += 1;
            //}
            //else
            //{
            //    hh = 0;
            //}

            //foreach (var item in tempVal)
            //{
            //    if (item.Date.Day <= countDay)
            //    {
            //        int hhhh = item.Date.Hour;
            //        int hhpl = item.Date.Hour + 1;
            //        if (hhhh < hh && date.Day == item.Date.Day || (hhhh==23 && hh==0))
            //        {
            //            suma += (item.TempValue * item.TempValue);
            //            hhh = item.Date.Hour;
            //            count++;

            //            date = item.Date.Date;
            //        }
            //        else
            //        {
            //            godz.Add(new Data()
            //            {
            //                DateD = date,
            //                Hours = hhh,
            //                TempValue = suma,
            //                Count = count
            //            });

            //            count = 0;
            //            suma = 0;
            //            suma += (item.TempValue * item.TempValue);
            //            date = item.Date.Date;
            //            if (hh < 23)
            //            {
            //                hh += 1;
            //            }
            //            else
            //            {
            //                hh = 0;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (hh < 23)
            //        {
            //            hh += 1;
            //        }
            //        else
            //        {
            //            hh = 0;
            //        }
            //        countDay = DateTime.DaysInMonth(item.Date.Year, item.Date.Month);
            //    }
            //}

            //foreach (var item in godz)
            //{
            //    RMSquare.Add(new Data()
            //    {
            //        DateD = item.DateD.Date,
            //        Hours = item.Hours,
            //        TempValue = (Math.Sqrt(item.TempValue)) / item.Count,
            //    });
            //}

            //var json2 = new JavaScriptSerializer().Serialize(RMSquare);


            //File.WriteAllText(path, json2);
            #endregion

            Console.ReadLine();
        }
        
    }
}