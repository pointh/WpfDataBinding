﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfDataBinding
{
    class Person : INotifyPropertyChanged
    {
        string _Jmeno, _Prijmeni;
        DateTime _Narozeni;

        public string Jmeno {
            get {
                return _Jmeno;
            }
            set {
                _Jmeno = value;
                OnPropertyChanged("Jmeno");
            }
        }
        public string Prijmeni {
            get
            {
                return _Prijmeni;
            }
            set {
                _Prijmeni = value;
                OnPropertyChanged("Prijmeni");
            }
        }
        public DateTime Narozeni {
            get
            {
                return _Narozeni;
            }
            set
            {
                _Narozeni = value;
                OnPropertyChanged("Narozeni");
            }
        }
        public string Status
        {
            get
            {
                return Jmeno + " " + Prijmeni + " " + Narozeni.ToString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Jmeno + " " + Prijmeni + " " + Narozeni.ToShortDateString();
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null) // jestli někdo poslouchá ...
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Person p = new Person()
        {
            Jmeno = "Jan",
            Prijmeni = "Vyčítal",
            Narozeni = new DateTime(2000, 11, 28)
        };

        public MainWindow()
        {
            InitializeComponent();
            DataContext = p;
        }

        private void BtShow_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression expr = tbPrijmeni.GetBindingExpression(TextBox.TextProperty);
            expr?.UpdateSource();

            MessageBox.Show(p.ToString());
        }

        private void BtClear_Click(object sender, RoutedEventArgs e)
        {
            p.Jmeno = p.Prijmeni = "";
            p.Narozeni = new DateTime(1900, 1, 1);
        }
    }


}
