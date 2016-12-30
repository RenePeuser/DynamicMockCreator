using System.Windows.Media.Media3D;

namespace ObjectCreator.Helper
{
    //public enum ObjectCreationStrategy
    //{
    //    None,
    //    SetupProperties,
    //    SetupMethods,
    //    SetupEnumerations,
    //    All
    //}

    public struct ObjectCreationStrategy
    {
        public ObjectCreationStrategy(bool setupProperties, bool setupMethods, bool setupEnumerations, int enumerationCount)
        {
            SetupProperties = setupProperties;
            SetupMethods = setupMethods;
            SetupEnumerations = setupEnumerations;
            EnumerationCount = enumerationCount;
        }

        public bool SetupProperties { get; }
        public bool SetupMethods { get; }
        public bool SetupEnumerations { get; }
        public int EnumerationCount { get; }
    }
}
