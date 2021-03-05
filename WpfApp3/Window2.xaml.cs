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
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        User ktos;
        MainWindow glowne_okno;
        public Window2()
        {
            InitializeComponent();
            ktos = null;
        }

        public Window2(MainWindow glowne_okno)
        {
            InitializeComponent();
            this.glowne_okno = glowne_okno;

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Phonebook phonebook = MainWindow.phonebook;
            int index = ((MainWindow)Application.Current.MainWindow).lista.SelectedIndex;
            if(((MainWindow)Application.Current.MainWindow).edytuj_dodaj==true)
            {
                ((MainWindow)Application.Current.MainWindow).edytuj_dodaj = false;
                phonebook.Dodaj(txt_imie.Text, txt_nazwisko.Text, txt_nr.Text);
            }
            else
            {
                phonebook.Ksiazka[index].imie = txt_imie.Text;
                phonebook.Ksiazka[index].nazwisko = txt_nazwisko.Text;
                phonebook.Ksiazka[index].nr = txt_nr.Text;
            }
            glowne_okno.zaktualizuj_wyswietlanie();
            this.Close();
            
        }

        private void txt_nazwisko_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_nr_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_imie_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }


    }
}
