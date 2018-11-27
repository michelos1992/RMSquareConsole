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
            var firstEl = tempVal.FirstOrDefault();
            var lastEl = tempVal.Last();

            int currHourEl = 0;
            int count = 0;
            int countDay = DateTime.DaysInMonth(tempVal.FirstOrDefault().Date.Year, tempVal.FirstOrDefault().Date.Month);
            int nextHour = firstEl.Date.Hour;
            int totalF = 0, totalI = 0;
            double min = tempVal.FirstOrDefault().TempValue;
            double max = tempVal.FirstOrDefault().TempValue;

            double suma = 0;

            date = tempVal.FirstOrDefault().Date;

            nextHour = CheckHourNext(nextHour);

            foreach (var item in tempVal)
            {
                if (date.Day <= countDay && date.Day <= item.Date.Day)
                {
                    int currentHour = item.Date.Hour;

                    if (currentHour < nextHour && date.Day == item.Date.Day || (currentHour == 23 && nextHour == 0))
                    {
                        suma += (item.TempValue * item.TempValue);
                        currHourEl = item.Date.Hour;
                        count++;

                        if (min != item.TempValue)
                        {
                            totalF += 1;
                        }
                        if (max != item.TempValue)
                        {
                            totalI += 1;
                        }

                        min = CheckMin(min, item.TempValue);
                        max = CheckMax(max, item.TempValue);
                        
                        date = item.Date.Date;
                    }
                    else
                    {
                        godz.Add(new Data()
                        {
                            DateD = date,
                            Hours = currHourEl,
                            Average = suma,
                            Count = count,
                            Max=max,
                            Min=min,
                            TotalFalls=totalF,
                            TotalIncrease=totalI
                        });

                        count = 0;
                        suma = 0;
                        totalF = 0;
                        totalI = 0;
                        suma += (item.TempValue * item.TempValue);
                        date = item.Date.Date;
                        min = 0;
                        max = 0;

                        nextHour = CheckHourNext(nextHour);
                    }
                }
                else
                {
                    if((date.Day == 30 || date.Day == 31) && item.Date.Day == 1)
                    {
                        godz.Add(new Data()
                        {
                            DateD = date,
                            Hours = currHourEl,
                            Average = suma,
                            Count = count,
                            Max = max,
                            Min = min,
                            TotalFalls = totalF,
                            TotalIncrease = totalI
                        });
                    }

                    nextHour = CheckHourNext(nextHour);

                    date = item.Date.Date;
                    countDay = DateTime.DaysInMonth(item.Date.Year, item.Date.Month);
                }

                if (item.Equals(lastEl))
                {
                    godz.Add(new Data()
                    {
                        DateD = date,
                        Hours = currHourEl,
                        Average = suma,
                        Count = count,
                        Max = max,
                        Min = min,
                        TotalFalls = totalF,
                        TotalIncrease = totalI
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
                    Average = (Math.Sqrt(item.Average)) / item.Count,
                    Count=item.Count,
                    Max = item.Max,
                    Min = item.Min,
                    TotalFalls=item.TotalFalls,
                    TotalIncrease=item.TotalIncrease
                });
                
            }
        }

        public void WriteFile()
        {
            string path = "D:\\Dropbox\\moje\\asdf\\Projekt-rekuratacja\\ConsoleApp1\\bin\\Debug\\export.json";
            
            string json = JsonConvert.SerializeObject(RMSquareList, Formatting.Indented);
            
            File.WriteAllText(path, json);
        }

        public int CheckHourNext(int _nextHour)
        {
            if (_nextHour < 23)
            {
                _nextHour += 1;
                return _nextHour;
            }
            else
            {
                _nextHour = 0;
                return _nextHour;
            }
        }

        public double CheckMin(double fmin, double _min)
        {
            double mmin = fmin;
            if (mmin > _min)
            {
                mmin = _min;
                return mmin;
            }
            else
            {
                return mmin;
            }
        }
        public double CheckMax(double fmax, double _max)
        {
            double mmax = fmax;
            if (mmax < _max)
            {
                mmax = _max;
                return mmax;
            }
            else
            {
                return mmax;
            }
        }

    }
}
