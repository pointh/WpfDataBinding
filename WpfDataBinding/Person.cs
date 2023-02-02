using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace WpfDataBinding
{
    public class Person : INotifyPropertyChanged
    {
        public static ObservableCollection<Person> AllPersons { get; set; } = new ObservableCollection<Person>();

        private string? _Jmeno, _Prijmeni;
        private DateTime _Narozeni;
        public Guid _ID {get; set;}

        public event PropertyChangedEventHandler? PropertyChanged;

        // Při každé změně dat chceme akutalizovat status bar
        public string? Jmeno
        {
            get => _Jmeno;
            set
            {
                _Jmeno = value;
                OnPropertyChanged("Jmeno");
                OnPropertyChanged("Status");
                if (_Jmeno?.Length < 3)
                {
                    JmenoErrorMsg = "Jméno musí být delší než 2 znaky.";
                    JmenoErrorVisible = Visibility.Visible;
                }
                else
                {
                    JmenoErrorMsg = string.Empty;
                    JmenoErrorVisible = Visibility.Hidden;
                }
                OnPropertyChanged("JmenoErrorVisible");
                OnPropertyChanged("JmenoErrorMsg");
            }
        }

        public Visibility JmenoErrorVisible { get; set; }
        public string? JmenoErrorMsg { get; set; }
        
        public string? Prijmeni
        {
            get => _Prijmeni;
            set
            {
                _Prijmeni = value;
                OnPropertyChanged("Prijmeni");
                OnPropertyChanged("Status");
            }
        }
        public DateTime Narozeni
        {
            get => _Narozeni;
            set
            {
                _Narozeni = value;
                OnPropertyChanged("Narozeni");
                OnPropertyChanged("Status");
            }
        }

        public string Status
        {
            get => Jmeno + " " + Prijmeni + " " + Narozeni.ToString();
        }

        public override string ToString()
        {
            return Jmeno + " " + Prijmeni + " " + Narozeni.ToShortDateString();
        }

        // pomocná metoda pro informaci o změně v datech
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null) // jestli někdo poslouchá ...
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public static Person PersonCopy(Person p)
        {
            Person q = new Person() 
            { 
                Jmeno = p.Jmeno, 
                Prijmeni= p.Prijmeni, 
                Narozeni=p.Narozeni,
                _ID = Guid.NewGuid(),
            };

            return q;
        }

        public static void Clear(Person p)
        {
            p.Jmeno = p.Prijmeni = string.Empty;
            p.Narozeni = new DateTime(1990, 1, 1);
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        private double? textBoxLabelFontSize;

        public double? TextBoxLabelFontSize 
        { 
            get => textBoxLabelFontSize; 
            set => SetProperty(ref textBoxLabelFontSize, value); 
        }

        public ICommand SaveCommand { get; set; } = new RelayCommand<Person>
            (
            (p) => {
                if (p != null)
                {
                    Person? savedExistingPerson = Person.AllPersons.FirstOrDefault(t => t._ID == p._ID);

                    if (savedExistingPerson != null)
                    {
                        int indexOfExisting = Person.AllPersons.IndexOf(savedExistingPerson);
                        Person.AllPersons[indexOfExisting] = Person.PersonCopy(p);
                    }
                    else
                    {
                        Person.AllPersons.Add(Person.PersonCopy(p));
                        Person.Clear(p);
                    }
                }
            }
            );

        public ICommand DeleteCommand { get; set; } = new RelayCommand<Person>
            (
            (personToDelete) =>
            {
                if (personToDelete != null)
                {
                        Person.AllPersons.Remove(personToDelete);
                }
            }
            );

    }


}
