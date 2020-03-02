using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Timers;
using System.Threading;
using System.Windows.Threading;

namespace WpfDataBinding
{
    class Person : INotifyPropertyChanged
    {
        
        System.Threading.Timer timer;
        string _Jmeno, _Prijmeni;
        DateTime _Narozeni;

        
        public Person()
        {
            timer = new System.Threading.Timer(
                (a)=> { Jmeno += "A"; }, 
                null, 
                0, 
                1000);
        }

        public string Jmeno {
            get {
                return _Jmeno;
            }
            set {
                _Jmeno = value;
                OnPropertyChanged("Jmeno");
                OnPropertyChanged("Status");
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
                OnPropertyChanged("Status");
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
                OnPropertyChanged("Status");
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
