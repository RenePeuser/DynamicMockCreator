using System;
using System.Collections.Generic;

namespace MockCreator.Interfaces
{
    public interface IInterfaceWithInterfaces
    {
        ICloneable Cloneable { get; }

        IList<string> IList { get; }

        IPrimitivePropertyInterface PrimitivePropertyInterface { get; }
    }
}