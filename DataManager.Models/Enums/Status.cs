using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Models.Enums
{
    public enum Status
    {
        [Description("Received - For Storage")] Received_ForStorage,
        [Description("In Storage – For Irradiation")] InStorage_ForIrradiation,
        [Description("For Irradiation")] ForIrradiation,
        [Description("At Irradiation")] AtIrradiation,
        [Description("Irradiated - For QA")] Irradiated_ForQA,
        [Description("Irradiated - In Storage - For QA")] Irradiated_InStorage_ForQA,
        [Description("For Dispatch")] ForDispatch,
        [Description("In Storage")] InStorage
    }
}
