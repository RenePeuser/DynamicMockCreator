namespace ObjectCreator.Helper
{
    public enum ObjectCreatorMode
    {
        None,
        WithProperties,
        WithMethods,
        All
    }

    public class ObjectCreatorModeClass
    {
        public ObjectCreatorModeClass(bool setupProperties, bool setupMethods, bool setupEnumerations)
        {
            SetupProperties = setupProperties;
            SetupMethods = setupMethods;
            SetupEnumerations = setupEnumerations;
        }

        public bool SetupProperties { get; }
        public bool SetupMethods { get; }
        public bool SetupEnumerations { get; }
    }
}
