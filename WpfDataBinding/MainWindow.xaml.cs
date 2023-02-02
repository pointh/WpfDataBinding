using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace WpfDataBinding
{

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
                Narozeni = DateTime.Now,
                TextBoxLabelFontSize = this.textBlockJmeno.FontSize,
            };
        }

        /* private void btSave_Click(object sender, RoutedEventArgs e)
        {
            Person? q = Person.AllPersons.Find(t => t._ID == p._ID);

            if ( q != null)
            {
                int qIndex = Person.AllPersons.IndexOf(q);
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
        */


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

        /*
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Person? toDelete = ((Button)sender).DataContext as Person;
            if (toDelete != null)
            {
                Person.AllPersons.Remove(toDelete);
            }

            lv.ItemsSource = null;
            lv.ItemsSource = Person.AllPersons;
        }
        */

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var context = ((Button)sender).DataContext as Person;

            if (context != null)
            {
                p.Jmeno = context?.Jmeno;
                p.Prijmeni = context?.Prijmeni;
                p.Narozeni = context.Narozeni;
                p._ID = context._ID;
            }
        }
    }


}
