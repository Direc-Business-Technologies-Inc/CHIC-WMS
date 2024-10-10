using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Libraries.Repositories
{
    public interface IUtilities
    {
        public (int hours, int minutes) ExtractTime(short value);
        string GetPostRemarks();
        public TimeOfDay ToTimeOfDay(short value);
    }
}
