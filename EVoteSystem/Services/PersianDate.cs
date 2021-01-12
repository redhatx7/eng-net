using System;
using System.Globalization;

namespace EVoteSystem.Services
{
    public static class PersianDate
    {
        public static string GetCurrentYear()
        {
            PersianCalendar pc = new();
            string year = $"{pc.GetYear(DateTime.Now).ToString().Substring(2)} {(pc.GetYear(DateTime.Now) + 1).ToString().Substring(2)}";
            return year;
        }

        public static string DateTimeToPersian(DateTime dt, bool addTime = false)
        {
            PersianCalendar pc = new();
            int year = pc.GetYear(dt);
            int month = pc.GetMonth(dt);
            int day = pc.GetDayOfMonth(dt);
            string date = $"{year}/{month}/{day}";
            if (addTime)
            {
                int hh = pc.GetHour(dt);
                int mm = pc.GetMinute(dt);
                string time = $"{hh} {mm}";
                date += " " + time;
            }

            return date;
        }

        public static DateTime PersianToDateTime(string persianStr, char separator = '/')
        {
            PersianCalendar pc = new();
            persianStr = persianStr.TrimStart().TrimEnd();
            string[] parts = persianStr.Split(' ');
            string[] datePart = parts[0].Split(separator);
            if (datePart.Length != 3)
                return DateTime.MinValue;
            
            int year = int.Parse(datePart[0]);
            int month = int.Parse(datePart[1]);
            int day = int.Parse(datePart[2]);

            int hh = 0, mm = 0, ss = 0;
            if (parts.Length > 1)
            {
                string[] timeParts = parts[1].Split(':');
                hh = int.Parse(timeParts[0]);
                mm = int.Parse(timeParts[1]);
                ss = int.Parse(timeParts[2]);
            }
            return pc.ToDateTime(year, month, day, hh, mm, ss, 0);
        }
        
    }
}