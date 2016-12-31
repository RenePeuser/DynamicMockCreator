using System;
using System.Collections.Generic;

namespace ObjectCreatorTest.Interfaces
{
    public interface IInterfaceWithInterfaces
    {
        ICloneable Cloneable { get; }

        IList<string> IList { get; }

        IPrimitivePropertyInterface PrimitivePropertyInterface { get; }

        IEnumerable<IPrimitivePropertyInterface> Interfaces { get; }
    }
}