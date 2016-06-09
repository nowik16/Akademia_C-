using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
namespace Nowicki_PGS
{

    public partial class MainWindow : Window, Iinstru
    {
      
        public Dictionary<String, String> dict = new Dictionary<String, String> { { "janK", "kowal" }, { "izaZ", "zaba" } };    //słownik z loginami i haslami uzytkowników

        public MainWindow()
        { 
            InitializeComponent();                                                                     
            var uri = new Uri("https://upload.wikimedia.org/wikipedia/commons/9/98/SafeWalletLogo.png");        //załadowane zdjecia
            var bitmap = new BitmapImage(uri);
            image.Source = bitmap;
            Instruction();
            
        }
        
        public void Instruction()
        {
            MessageBox.Show("Instrukcja: \n Logowanie do systemu \n Dostępne konta: \n L: janK H: kowal \n L: izaZ H: zaba");       //instrukcja, interfejs
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((dict[textBox.Text] == passwordBox.Password))               //sprawdzenie czy poprawnie wpisalismy login i haslo
                {
                    this.Hide();
                    Menu nowy = new Menu();                                     //gdy tak to tworzymy nowe okno
                    nowy.Show();
                    nowy.label1.Content = "Witaj: " + textBox.Text;             //tekst powitalny dla indywidualnego uzytkownika
                }
                else
                    MessageBox.Show("Błedny login lub hasło albo oba :)");
            }
            catch(KeyNotFoundException z) { MessageBox.Show("Błedny login lub hasło albo oba :)"); };
            
        }

        private void button_wyjdz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();                                                       //wyjscie z programu
        }
    }
}
