using System;
using System.Windows;

namespace Nowicki_PGS
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window, Iinstru
    {
        
        public Menu()
        {
            InitializeComponent();
            Instruction();
        }
        
        public void Instruction()
        {
            MessageBox.Show("Instrukcja: \n Zarzadaj - uruchamia fabrykę \n Dodaj - formularz dodajacy osobę \n Wyjsc - wyłączenie programu");
        }

        private void dodaj_Click(object sender, RoutedEventArgs e)
        {
            Window1 dodaj = new Window1();                                                  //guzik dodaj nowa osobe, nowe okno
            dodaj.Show();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {                                                                                   //wylaczenie programu
            Environment.Exit(1);
        }

        private void startF_Click(object sender, RoutedEventArgs e)
        {
            Fabryka fab = new Fabryka();                                                //okno z fabryka
            fab.Show();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);                                                       //wylaczenie programu krzyzykiem
            App.Current.Shutdown();
        }
    }
}
