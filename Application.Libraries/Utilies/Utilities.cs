using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Libraries.Utilies
{
    public class Utilities : IUtilities
    {
        public (int hours, int minutes) ExtractTime(short value)
        {
            var x = (value).ToString("D4").Insert(2, ".").Split(".");
            int hours = int.Parse(x[0]), minutes = int.Parse(x[1]);
            return (hours, minutes);
        }

        public TimeOfDay ToTimeOfDay(short value)
        {
            var (hour, minute) = ExtractTime(value);
            var x = new TimeOfDay(hour, minute, 0, 0);
            return x;
        }

        public string GetPostRemarks()
        {
            return $"Posted by TIMS {DateTime.Now.ToString()}";
        } 
    }
}
