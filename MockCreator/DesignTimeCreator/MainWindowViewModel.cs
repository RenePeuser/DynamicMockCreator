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
        IPerson Person { get; set; }

        IEnumerable<IPerson> Persons { get; }

        IPerson[] PersonArray { get; }

        ObservableCollection<Person> PersonOC { get; set; }
    }

    public class MainWindowViewModel : BindingBase
    {
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