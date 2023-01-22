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
    class Person : INotifyPropertyChanged
    {
        public static List<Person> AllPersons { get; set; } = new List<Person>();

        private string _Jmeno, _Prijmeni;
        private DateTime _Narozeni;

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
            Person q = new Person() { Jmeno = p.Jmeno, Prijmeni= p.Prijmeni, Narozeni=p.Narozeni };
            return q;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Person p;

        public MainWindow()
        {
            InitializeComponent();
            
            DataContext =
            p = new Person()
            {
                Narozeni = DateTime.Now
            };
            
        }

        private void BtShow_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression expr = tbPrijmeni.GetBindingExpression(TextBox.TextProperty);
            expr?.UpdateSource();

            MessageBox.Show(p.ToString() + "\n" + 
                expr.ResolvedSourcePropertyName + ": vynucená akutalizace");
            Person.AllPersons.Add(Person.PersonCopy(p));
            lv.ItemsSource = null;
            lv.ItemsSource = Person.AllPersons;
        }

        // Změna dat, vzhledem k bindingu, stačí i k aktualizaci grafické podoby formuláře
        private void BtClear_Click(object sender, RoutedEventArgs e)
        {
            p.Jmeno = p.Prijmeni = string.Empty;
            p.Narozeni = DateTime.Now;
        }

        private void btDefault_Click(object sender, RoutedEventArgs e)
        {
            p.Jmeno = "Jan";
            p.Prijmeni = "Novák";
            p.Narozeni = new DateTime(1990, 1, 1);
        }
    }


}
