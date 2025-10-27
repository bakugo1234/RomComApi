using System;

namespace RomCom.Common.ServiceInstallers.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class TransientServiceAttribute : Attribute
    {
    }
}

