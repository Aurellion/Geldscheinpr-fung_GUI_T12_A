using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Geldscheinprüfung_GUI_T12_A
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_Bereche_Click(object sender, RoutedEventArgs e)
        {
            //Einlesen:            
            string seriennummer;
            bool eingabeFalsch = true;
            
                seriennummer = TB_Eingabe.Text.ToUpper();
                //Eingabeformat überprüfen
                bool BedingungZiffern = true;
                for (int i = 2; i < seriennummer.Length; i++)
                {
                    if (!Char.IsDigit(seriennummer[i]))
                    {
                        BedingungZiffern = false;
                    }
                }

                if (seriennummer.Length != 12)
                {
                    MessageBox.Show("Die eingegebene Seriennummer hat nicht die richtige Länge.\nDie Seriennummer hat das Format XX0000000000, wobei X für Buchstaben (A-Z) und 0 für Ziffern (0-9) steht.");                    
                    eingabeFalsch = true;                    
                }
                else if (!Char.IsLetter(seriennummer[0]) || !Char.IsLetter(seriennummer[1]))
                // ! Umkehrung von Wahrheitswerten
                {
                    MessageBox.Show("Die eingegebene Seriennummer muss an den ersten beiden Stellen Buchstaben enthalten.\nDie Seriennummer hat das Format XX0000000000, wobei X für Buchstaben (A-Z) und 0 für Ziffern (0-9) steht.");
                    eingabeFalsch = true;
                }
                else if (!BedingungZiffern)
                {
                    MessageBox.Show("Die eingegebene Seriennummer muss an den letzten zehn Stellen Ziffern enthalten.\nDie Seriennummer hat das Format XX0000000000, wobei X für Buchstaben (A-Z) und 0 für Ziffern (0-9) steht.");
                    eingabeFalsch = true;
                }
                else
                {
                    MessageBox.Show("Folgende Seriennummer wird überprüft: " + seriennummer);
                    eingabeFalsch = false;
                }
         
            if(eingabeFalsch==false)
            {
                string Prüfnummer = "";
                for (int i = 0; i < seriennummer.Length; i++)
                {
                    if (i < 2)
                    {
                        Prüfnummer += Convert.ToString(Convert.ToInt32(seriennummer[i]) - 64);
                    }
                    else
                    {
                        Prüfnummer += seriennummer[i];
                    }
                }

                int Quersumme = 0;
                for (int i = 0; i < Prüfnummer.Length - 1; i++)
                {
                    Quersumme += Convert.ToInt32(Prüfnummer[i].ToString());
                }                

                int Rest = Quersumme % 9;
                int Differenz = 7 - Rest;
                int Prüfziffer;

                if (Differenz == 0) Prüfziffer = 9;
                else if (Differenz == -1) Prüfziffer = 8;
                else Prüfziffer = Differenz;

                
                if (Prüfziffer == Convert.ToInt32(Convert.ToString(Prüfnummer[Prüfnummer.Length - 1])))
                {
                   TB_Ausgabe.Text="Seriennummer ist echt.";
                }
                else
                {
                   TB_Ausgabe.Text = "Seriennummer ist falsch.";
                }
            }
        }
    }
}
