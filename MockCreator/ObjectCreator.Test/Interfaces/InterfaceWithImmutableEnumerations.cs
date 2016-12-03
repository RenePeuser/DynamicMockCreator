using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCreatorTest.Interfaces
{
    public interface InterfaceWithImmutableEnumerations
    {
        ImmutableArray<string> ImmutableArray { get; }

        ImmutableDictionary<string, int> ImmutableDictionary { get; }

        ImmutableHashSet<string> ImmutableHashSet { get; }

        ImmutableList<string> ImmutableList { get; }
    }
}
