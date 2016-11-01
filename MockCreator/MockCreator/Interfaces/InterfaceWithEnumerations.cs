using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ObjectCreatorTest.Interfaces
{
    public interface InterfaceWithEnumerations
    {
        IEnumerable<object> Objects { get; }

        IEnumerable<ICloneable> Cloneables { get; }

        object[] ObjectArrays { get; }

        ICloneable[] CloneableArray { get; }

        ObservableCollection<object> ObservableCollectionObjects { get; }

        ObservableCollection<ICloneable> ObservableCollectionCloneables { get; }
    }
}
