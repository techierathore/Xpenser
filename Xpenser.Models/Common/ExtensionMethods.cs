using System;
namespace Xpenser.Models
{
    public static class DateExtensions
    {


        public static DateTime RoundTime(this DateTime dt)
        {
            /*
            var date = new DateTime(2010, 02, 05, 10, 35, 25, 450);
            //result  2010/02/05 10:35:25
            var roundedUp = date.RoundUp(TimeSpan.FromMinutes(15));
            // result 2010/02/05 10:45:00
            var roundedDown = date.RoundDown(TimeSpan.FromMinutes(15));
            // result 2010/02/05 10:30:00
            var roundedToNearest = date.RoundToNearest(TimeSpan.FromMinutes(15));
            // result 2010/02/05 10:30:00
            */
            return dt.RoundToNearest(TimeSpan.FromMinutes(15));
        }

        public static DateTime RoundUp(this DateTime dt, TimeSpan d)
        {
            var modTicks = dt.Ticks % d.Ticks;
            var delta = modTicks != 0 ? d.Ticks - modTicks : 0;
            return new DateTime(dt.Ticks + delta, dt.Kind);
        }

        public static DateTime RoundDown(this DateTime dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            return new DateTime(dt.Ticks - delta, dt.Kind);
        }

        public static DateTime RoundToNearest(this DateTime dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            bool roundUp = delta > d.Ticks / 2;
            var offset = roundUp ? d.Ticks : 0;
            return new DateTime(dt.Ticks + offset - delta, dt.Kind);
        }

        public static int Age(this DateTime dateOfBirth)
        {
            if (DateTime.Today.Month < dateOfBirth.Month ||
            DateTime.Today.Month == dateOfBirth.Month &&
             DateTime.Today.Day < dateOfBirth.Day)
            {
                return DateTime.Today.Year - dateOfBirth.Year - 1;
            }
            else
                return DateTime.Today.Year - dateOfBirth.Year;
        }

        public static bool IsParticularDay(this DateTime dt, DayOfWeek day)
        {
            bool bResult = false;
            if (dt.DayOfWeek == day)
            { bResult = true; }
            return bResult;
        }
        public static Boolean IsBetween(this DateTime dt, DateTime startDate, DateTime endDate, Boolean compareTime = false)
        {
            return compareTime ?
               dt >= startDate && dt <= endDate :
               dt.Date >= startDate.Date && dt.Date <= endDate.Date;
        }
    }

    public static class TimeSpanExtensions
    {
        public static TimeSpan RoundToNearestMinutes(this TimeSpan input, int minutes)
        {
            var totalMinutes = (int)(input + new TimeSpan(0, minutes / 2, 0)).TotalMinutes;

            return new TimeSpan(0, totalMinutes - totalMinutes % minutes, 0);
        }
    }
}
