using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Nowicki_PGS
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public List<Osoba> list;
        public Window1() 
        {
            InitializeComponent();                                                                      //Inicjalizacja komponentow
            list = new List<Osoba>();                                                                   //Nasza lista
            this.ListView1.ItemsSource = list;                                                         //Komponent lista przyjmuje wartosci z naszej utworzonej listy osob
        }

        private void wykonaj(object sender, RoutedEventArgs e)
        {
            
            Osoba osoba = new Osoba();                                                                  //Obiekt klasy Oosba
            osoba.Imie = this.imie.Text;                                                                //Pobranie wartosci z pol tekstowych i podstawienie do naszych zmiennych
            osoba.Nazwisko = this.nazw.Text;
            osoba.Adres = this.addr.Text;
            osoba.Telefon = this.tel.Text;
            list.Add(osoba);                                                                           //Dodanie osoby do listy

            strona_tel.CanSelectNextPage = !string.IsNullOrEmpty(tel.Text) ? true : false;              //petla warunkowa if sprawiajaca ze jest moziwosc zapisania osoby pod warunkiem, że pole z nr telefonu zostanie uzupelnione (nie puste)
            zapisz.IsEnabled = false;
        }

        private void czyTekst(object sender, TextChangedEventArgs e)
        {

            strona_imie.CanSelectNextPage = !string.IsNullOrEmpty(imie.Text) ? true : false;            //Sprawdzenie czy pola zostaly uzupelnione. Jezeli tak to jest mozliwosc przejscia dalej.
            strona_nazw.CanSelectNextPage = !string.IsNullOrEmpty(nazw.Text) ? true : false;
            strona_addr.CanSelectNextPage = !string.IsNullOrEmpty(addr.Text) ? true : false;
            zapisz.IsEnabled = !string.IsNullOrEmpty(tel.Text) ? true : false;
        }

        private void czyTekst2(object sender, DependencyPropertyChangedEventArgs e)
        {

            this.imie.Text = "";                                                                        //Czyszczenie pol tekstowych
            this.nazw.Text = "";
            this.addr.Text = "";
            this.tel.Text = "";
            strona_tel.CanSelectNextPage = false;
        }
    }
    
}
