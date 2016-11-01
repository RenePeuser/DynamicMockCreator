using System;
using System.ComponentModel;

namespace MockCreatorTest.DataClasses
{
    public class ClassWithInterfaces
    {
        public ClassWithInterfaces(
            ICloneable cloneable,
            INotifyPropertyChanged notifyPropertyChanged,
            IComparable comparable,
            IConvertible convertible,
            IDataErrorInfo dataErrorInfo,
            IFormattable formattable)
        {
            Cloneable = cloneable;
            NotifyPropertyChanged = notifyPropertyChanged;
            Comparable = comparable;
            Convertible = convertible;
            DataErrorInfo = dataErrorInfo;
            Formattable = formattable;
        }

        public ICloneable Cloneable { get; }
        public INotifyPropertyChanged NotifyPropertyChanged { get; }
        public IComparable Comparable { get; }
        public IConvertible Convertible { get; }
        public IDataErrorInfo DataErrorInfo { get; }
        public IFormattable Formattable { get; }
    }
}