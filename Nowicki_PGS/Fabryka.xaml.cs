using System;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Management;


namespace Nowicki_PGS
{
    /// <summary>
    /// Interaction logic for Fabryka.xaml
    /// </summary>
    public partial class Fabryka : Window, Iinstru
    {
        private Boolean checkCounter = false;
        private int speedClock;
        private int xPos=420;
        private int yPos = 50;
        private int counter = 100;
        private System.Windows.Threading.DispatcherTimer timerBall;
        private System.Windows.Threading.DispatcherTimer timerAct;
        private static Elipsa eli = new Elipsa();

        Thread threadBall;
        Thread threadAct;
        Random rand = new Random();
        Ellipse ellipse = null;

        public void Instruction()
        {
            MessageBox.Show("Witaj w fabryce \n Instrukcja: \n Start - uruchamia produkcje \n Slider Predkość - zmiana prędkości taśmy \n Wybór koloru - zmiana koloru produktu \n Lista - dostępne kolory \n Temperatura - temp. silników tasmy \n Reset - ustawia licznik na 100 \n Licznik = 0 wyłącza program \n Miłej zabawy!!");
        }

        public enum enumColor                                                       //lista  z kolorami
        {
            Aqua,
            Azure,
            Black,
            Blue,
            Brown,
            Coral,
            Cyan,
            DarkGreen,
            Fuchsia,
            Green,
            HotPink,
            Khaki,
            Olive,
            Purple,
            Red,
            Teal,
            Violet,
            Yellow
        }

        public Fabryka()
        {
            InitializeComponent();
            Instruction();

            timerBall = new System.Windows.Threading.DispatcherTimer();
            timerBall.Interval = TimeSpan.FromSeconds((0.01));                              //pierwszy timer, odpowiedzialny za predkosc kulki
            timerBall.Tick += timerBall_Tick;
            timerAct = new System.Windows.Threading.DispatcherTimer();
            timerAct.Interval = TimeSpan.FromSeconds((1));                                  //drugi timer, odpowiedzialny za wystepujace bledy i counter
            timerAct.Tick += timerAct_Tick;

            var uri = new Uri("https://zapodaj.net/images/3683f9d7cc63c.jpg");              //zaladowanie zdjecia
            var bitmap = new BitmapImage(uri);
            image.Source = bitmap;

            threadBall = new Thread(timerBall.Start);                                       //nowe watki, aby wywolanie np. nowego koloru, odczytu temp z systemu nie przerywalo pracy pierwszego timera
            threadAct = new Thread(timerAct.Start);

        }
        protected override void OnClosed(EventArgs e)                                       //wyjscie
        {
            base.OnClosed(e);
            timerAct.Stop();
            timerBall.Stop();
            this.Close();
            threadBall.Abort();
            threadAct.Abort();

        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                threadBall.Start();                                                 //start fabryki
                threadAct.Start();
            }
            catch(ThreadStateException z)
            {
                MessageBox.Show("Fabryka już pracuje");
            }
        }

        private void timerBall_Tick(object sender, EventArgs e)
        {
            
            PaintCanvas.Children.Remove(ellipse);
            ellipse = eli.CreateAnEllipse(20, 20);                                          //poruszanie sie kuleczki po ekranie
            PaintCanvas.Children.Add(ellipse);

            Canvas.SetLeft(ellipse, xPos);
            Canvas.SetTop(ellipse, yPos);

            if (xPos > 200 && yPos == 50)                                           //zmiana jej polozenia zgodnie z wygladem tasmy
                xPos -= 1; 
            if (xPos == 260 && yPos < 130)
                yPos += 2;
            if (yPos == 130 && xPos >= 260)
                xPos += 1;
            if (xPos == 420 && yPos >= 130)
                yPos += 2;
            if (yPos == 210 && xPos <= 420)
                xPos -= 1;  
            if (xPos == 265 && yPos >= 210)
                yPos += 2;
            if (yPos == 290 && xPos <= 420)
                xPos += 1;
            if (yPos>350 )
            { 
                xPos = 420;                                                             //reset polozenia
                yPos = 50;
            } 
        }

        private void timerAct_Tick(object sender, EventArgs e)
        {
                                                                                    //wystepowanie losowych powiadomień
            int zm = rand.Next(80);
            if (zm == 5)
                MessageBox.Show("Wiecej koloru Niebieskiego!");
            if (zm == 6)
                MessageBox.Show("Wiecej koloru Czerwonego!");
            if (zm == 7)
                MessageBox.Show("Wiecej koloru Zielonego!");
            if (zm == 8)
                MessageBox.Show("Taśma jedzie za szybko!");
            if (zm == 9)
                MessageBox.Show("Taśma jedzie za wolno!");


            if (checkCounter == true)                                               //reset coutera
            {
                counter = 100;
                checkCounter = false;
            }

            counter -= 1;                                                           //akcje coutera powiadamiajace o uplywajacym czasie
            label3.Content = counter;

            if (counter < 10 && counter % 2==0)
                button2.Background= System.Windows.Media.Brushes.Red ;
            else if (counter < 10 )
                button2.Background = System.Windows.Media.Brushes.Plum;
            else
                button2.Background = System.Windows.Media.Brushes.White;

            if (counter == 0)
                Environment.Exit(1);
        }


        public void changeOptions()
        {
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "Select * From Win32_Processor");    //pobieranie z systemu danych
                foreach (ManagementObject mj in mos.Get())
                {
                    speedClock = (Convert.ToInt32(mj["CurrentClockSpeed"]))/10;
                }
            }
            catch (ManagementException  e){ MessageBox.Show(e.Message); }

            foreach (enumColor kolor in Enum.GetValues(typeof(enumColor)))                                              //ustawienie koloru wybranego przez uzytkownika
            {

                ColorConverter converter = new ColorConverter();
                if (textBox.Text == kolor.ToString())
                    eli.fillBrush = new SolidColorBrush() { Color = (Color)converter.ConvertFrom(kolor.ToString()) };
            }
        }

        private void zatwierdz_Click(object sender, RoutedEventArgs e)
        {                                                                                                           //zmiana opcji, wyswietlenie temperatury, zmiana predkosci
            changeOptions();
            label2.Content = speedClock;
            timerBall.Interval = TimeSpan.FromSeconds((slider.Value));
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            checkCounter = true;                                                                            //reset coutera
        }

        private void lista_Click(object sender, RoutedEventArgs e)
        {                                                                                               //lista kolorow
            StringBuilder colorStr = new StringBuilder();
            colorStr.Append("Lista kolorów:\n");

            foreach (enumColor iColor in Enum.GetValues(typeof(enumColor)))
            {
                colorStr.Append(iColor.ToString()+"\n");
            }

            MessageBox.Show(colorStr.ToString());
        }

      
    }
}
