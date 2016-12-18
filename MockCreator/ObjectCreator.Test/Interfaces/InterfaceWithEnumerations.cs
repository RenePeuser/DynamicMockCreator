using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ObjectCreatorTest.Interfaces
{
    public interface MyInterfaceWithEnumerations
    {
        IEnumerable<object> Objects { get; }

        IEnumerable<ICloneable> Cloneable { get; }

        object[] ObjectArrays { get; }

        ICloneable[] CloneableArray { get; }

        ObservableCollection<object> ObservableCollectionObjects { get; }

        ObservableCollection<ICloneable> ObservableCollectionCloneable { get; }
    }
}
