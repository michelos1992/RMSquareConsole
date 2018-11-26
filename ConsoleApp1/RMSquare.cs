using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ConsoleApp1
{
    public class RMSquare
    {
        DateTime date = new DateTime();

        List<TemperatureS> temperaValues2 = new List<TemperatureS>();
        List<TemperatureValue> tempVal = new List<TemperatureValue>();
        List<Data> godz = new List<Data>();
        List<Data> RMSquareList = new List<Data>();

        string json;

        public void ReadFile(string _nameFile)
        {
            StreamReader r = new StreamReader(_nameFile);
            json = r.ReadToEnd();
        }

        public void DeserializeJson()
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(json);
            var data = jsonObject.Data;
            var ddd = JsonConvert.DeserializeObject<Dictionary<string, double>>(Convert.ToString(data));

            foreach (var item in ddd)
            {
                temperaValues2.Add(new TemperatureS()
                {
                    Date = item.Key,
                    TempValue = item.Value,
                });
            }


            ChangeToDate(temperaValues2);
        }

        public void ChangeToDate(List<TemperatureS> _tempV)
        {
            foreach (var item in _tempV)
            {
                tempVal.Add(new TemperatureValue()
                {
                    Date = DateTime.ParseExact(item.Date, "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    TempValue = item.TempValue,
                });
            }
        }

        public void RMSFirstPart()
        {
            var dl = tempVal.FirstOrDefault();
            var lastEl = tempVal.Last();

            int hhh = 0;
            int count = 0;
            int countDay = DateTime.DaysInMonth(tempVal.FirstOrDefault().Date.Year, tempVal.FirstOrDefault().Date.Month);
            int hh = dl.Date.Hour;
            double min = tempVal.FirstOrDefault().TempValue;
            double max = tempVal.FirstOrDefault().TempValue;

            double suma = 0;

            date = tempVal.FirstOrDefault().Date;

            if (hh < 23)
            {
                hh += 1;
            }
            else
            {
                hh = 0;
            }

            foreach (var item in tempVal)
            {
                if (date.Day <= countDay && date.Day <= item.Date.Day)
                {
                    int hhhh = item.Date.Hour;
                    int hhpl = item.Date.Hour + 1;
                    if (hhhh < hh && date.Day == item.Date.Day || (hhhh == 23 && hh == 0))
                    {
                        suma += (item.TempValue * item.TempValue);
                        hhh = item.Date.Hour;
                        count++;

                        if (min > item.TempValue)
                        {
                            min = item.TempValue;
                        }
                        else
                        {
                            max = item.TempValue;
                        }

                        date = item.Date.Date;
                    }
                    else
                    {
                        godz.Add(new Data()
                        {
                            DateD = date,
                            Hours = hhh,
                            TempValue = suma,
                            Count = count,
                            Max=max,
                            Min=min,
                        });

                        count = 0;
                        suma = 0;
                        suma += (item.TempValue * item.TempValue);
                        date = item.Date.Date;

                        if (hh < 23)
                        {
                            hh += 1;
                        }
                        else
                        {
                            hh = 0;
                        }
                    }
                }
                else
                {
                    if((date.Day == 30 || date.Day == 31) && item.Date.Day == 1)
                    {
                        godz.Add(new Data()
                        {
                            DateD = date,
                            Hours = hhh,
                            TempValue = suma,
                            Count = count
                        });
                    }
                    if (hh < 23)
                    {
                        hh += 1;
                    }
                    else
                    {
                        hh = 0;
                    }
                    date = item.Date.Date;
                    countDay = DateTime.DaysInMonth(item.Date.Year, item.Date.Month);
                }
                if (item.Equals(lastEl))
                {
                    godz.Add(new Data()
                    {
                        DateD = date,
                        Hours = hhh,
                        TempValue = suma,
                        Count = count
                    });
                }
            }
            RMSSecondPart(godz);
        }
        public void RMSSecondPart(List<Data> _hours)
        {
            foreach (var item in _hours)
            {
                TimeSpan ts = new TimeSpan(item.Hours, 0, 0);
                item.DateD = item.DateD.Date + ts;

                RMSquareList.Add(new Data()
                {
                    DateD = item.DateD,                    
                    Hours = item.Hours,
                    TempValue = (Math.Sqrt(item.TempValue)) / item.Count,
                    Count=item.Count
                });
                
            }
        }

        public void WriteFile()
        {
            string path = "D:\\Dropbox\\moje\\asdf\\Projekt-rekuratacja\\ConsoleApp1\\bin\\Debug\\export.json";
            
            string json = JsonConvert.SerializeObject(RMSquareList, Formatting.Indented);
            
            File.WriteAllText(path, json);
        }
    }
}
