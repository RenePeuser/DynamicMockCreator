using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DesignTimeCreator
{
    public class BindingBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public interface IMainWindowViewModel : INotifyPropertyChanged
    {
        int IntValue { get; }

        double DoubleValue { get; }

        decimal DecimalValue { get; }

        bool BoolValue { get; }

        IPerson Person { get; set; }

        IEnumerable<IPerson> Persons { get; }

        IPerson[] PersonArray { get; }

        ObservableCollection<Person> PersonOC { get; set; }
    }

    public class MainWindowViewModel : BindingBase
    {
        public int IntValue { get; set; }

        public double DoubleValue { get; set; }

        public decimal DecimalValue { get; set; }

        public bool BoolValue { get; set; }

        public IPerson Person { get; set; }

        public IEnumerable<IPerson> Persons { get; set; }

        public IPerson[] PersonArray { get; set; }

        public ObservableCollection<Person> PersonOC { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }

    public interface IPerson
    {
        string Name { get; set; }

        int Age { get; set; }
    }
}