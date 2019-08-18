using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EventReporting.Model
{
    public enum ETypeOfSettlement : short
    {
        [Description("Village")]
        Village = 1,

        [Description("Neighborhood")]
        Neighborhood = 2,

        [Description("Hamlet")]
        Hamlet = 3
    }
}
