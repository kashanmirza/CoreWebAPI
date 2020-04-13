using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI.Helpers
{
    public class DateFormatter
    {
        public static DateTime ConvertStringToDate(string date)
        {
            string[] dateParts = date.Split('/');
            int day = Convert.ToInt32(dateParts[0]);
            int month = Convert.ToInt32(dateParts[1]);
            int year = Convert.ToInt32(dateParts[2]);

            DateTime currentDate = new DateTime(year, month, day);

            return currentDate;
        }
    }
}
