using System;

namespace MockCreatorTest.DataClasses
{
    public abstract class ClassAbstract
    {
        protected ClassAbstract(ICloneable cloneable, bool boolValue, object objectValue, DateTime dateTime,
            string stringValue)
        {
            Cloneable = cloneable;
            BoolValue = boolValue;
            ObjectValue = objectValue;
            DateTime = dateTime;
            StringValue = stringValue;
        }

        public ICloneable Cloneable { get; }
        public bool BoolValue { get; }
        public object ObjectValue { get; }
        public DateTime DateTime { get; }
        public string StringValue { get; }
    }
}