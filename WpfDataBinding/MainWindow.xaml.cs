using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Xaml.Behaviors;
using System.Threading;
using System.Windows.Threading;
using System.Globalization;
using System.Collections.Generic;

namespace WpfDataBinding
{
    public class Person : INotifyPropertyChanged
    {
        public static List<Person> AllPersons { get; set; } = new List<Person>();

        private string _Jmeno, _Prijmeni;
        private DateTime _Narozeni;
        public Guid _ID {get; set;}

        public event PropertyChangedEventHandler PropertyChanged;

        // Při každé změně dat chceme akutalizovat status bar
        public string Jmeno
        {
            get => _Jmeno;
            set
            {
                _Jmeno = value;
                OnPropertyChanged("Jmeno");
                OnPropertyChanged("Status");
            }
        }
        public string Prijmeni
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
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Person p;

        public double TextBoxLabelFontSize { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            
            DataContext =
            p = new Person()
            {
                Narozeni = DateTime.Now
            };
            TextBoxLabelFontSize = this.textBlockJmeno.FontSize;
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            //BindingExpression expr = tbPrijmeni.GetBindingExpression(TextBox.TextProperty);
            //expr?.UpdateSource();

            Person q = Person.AllPersons.Find(t => t._ID == p._ID);
            int qIndex = Person.AllPersons.IndexOf(q);

            if ( q != null)
            {
                Person.AllPersons[qIndex] = Person.PersonCopy(p);
            }
            else
            {
                Person.AllPersons.Add(Person.PersonCopy(p));
                Person.Clear(p);
            }

            lv.ItemsSource = null;
            lv.ItemsSource = Person.AllPersons;
        }

        // Změna dat, vzhledem k bindingu, stačí i k aktualizaci grafické podoby formuláře
        private void BtClear_Click(object sender, RoutedEventArgs e)
        {
            Person.Clear(p);
        }

        private void btDefault_Click(object sender, RoutedEventArgs e)
        {
            Person.Clear(p);
            p.Jmeno = "Jan";
            p.Prijmeni = "Novák";
            p.Narozeni = new DateTime(1990, 1, 1);
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Person toDelete = ((Button)sender).DataContext as Person;
            Person.AllPersons.Remove(toDelete);

            lv.ItemsSource = null;
            lv.ItemsSource = Person.AllPersons;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            p.Jmeno = (((Button)sender).DataContext as Person).Jmeno;
            p.Prijmeni = (((Button)sender).DataContext as Person).Prijmeni;
            p.Narozeni = (((Button)sender).DataContext as Person).Narozeni;
            p._ID = (((Button)sender).DataContext as Person)._ID;
        }
    }


}
