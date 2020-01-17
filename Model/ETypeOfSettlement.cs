using System.ComponentModel;

namespace EventReporting.Model
{
    public enum ETypeOfSettlement : byte
    {
        [Description("Village")]
        Village = 1,

        [Description("Neighborhood")]
        Neighborhood = 2,

        [Description("Hamlet")]
        Hamlet = 3
    }
}
