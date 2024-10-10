using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Models.Enums
{
    public enum ServiceType
    {
        [Description("Irradiation Only")] IrradiationOnly,
        [Description("Storage Only")] StorageOnly,
        [Description("Storage + Irradiation + Storage")] StorageIrradiateStorage,
    }
}
