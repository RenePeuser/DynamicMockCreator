using System.Collections.Immutable;

namespace ObjectCreatorTest.Interfaces
{
    public interface IWithImmutableEnumerations
    {
        ImmutableArray<string> ImmutableArray { get; }

        ImmutableDictionary<string, int> ImmutableDictionary { get; }

        ImmutableHashSet<string> ImmutableHashSet { get; }

        ImmutableList<string> ImmutableList { get; }
    }
}
