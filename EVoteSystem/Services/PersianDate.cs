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
            string date = $"{year}/{month}/{day)}";
            if (addTime)
            {
                int hh = pc.GetHour(dt);
                int mm = pc.GetMinute(dt);
                string time = $"{hh} {mm}";
                date += " " + time;
            }

            return date;
        }

        public static DateTime PersianToDateTime()
        {
            
        }
        
    }
}