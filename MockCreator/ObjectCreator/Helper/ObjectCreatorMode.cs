using System;

namespace ObjectCreator.Helper
{
    public struct ObjectCreationStrategy
    {
        public ObjectCreationStrategy(bool setupProperties, bool setupMethods, int enumerationCount, Type undefinedType)
        {
            SetupProperties = setupProperties;
            SetupMethods = setupMethods;
            EnumerationCount = enumerationCount;
            UndefinedType = undefinedType;
        }

        public bool SetupProperties { get; }
        public bool SetupMethods { get; }
        public int EnumerationCount { get; }
        public Type UndefinedType { get; }
    }
}
