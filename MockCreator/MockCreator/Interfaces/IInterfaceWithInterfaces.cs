using System;
using System.Collections.Generic;

namespace MockCreatorTest.Interfaces
{
    public interface IInterfaceWithInterfaces
    {
        ICloneable Cloneable { get; }

        IList<string> IList { get; }

        IPrimitivePropertyInterface PrimitivePropertyInterface { get; }
    }
}