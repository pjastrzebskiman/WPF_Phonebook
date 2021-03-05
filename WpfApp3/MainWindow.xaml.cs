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
using System.IO;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace WpfApp3
{
   public class User
    {
        public string imie;
        public string nazwisko;
        public string nr;
        public User(string wimie, string wnazwisko, string wnr)
        {
            imie = wimie;
            nazwisko = wnazwisko;
            nr = wnr;
        }
    }


    public class Phonebook
    {
        public string sciezka;
        public Phonebook(string wsciezka)

        {
            sciezka = wsciezka;
        }

        public List<User> Ksiazka = new List<User>();

        public void Dodaj(string imie, string nazwisko, string nr)
        {
            Ksiazka.Add(new User(imie, nazwisko, nr));
        }

        public void Usun(int index)
        {
            Ksiazka.Remove(Ksiazka[index]);
        }

        public User pobierz(int index)
        {
            return Ksiazka[index];
        }

        public string Wyswietl(int indeks)
        {
            string listai = "";
            listai += Ksiazka[indeks].nr;
            return listai;
        }


            public int licznik()
            {
                int licznik = Ksiazka.Count;
                return licznik;
            }

        public void Zapisz()
        {
            using (StreamWriter sciezkadoksiazki = new StreamWriter(sciezka, false))
            {
                string listai = "";
                for (int i = 0; i < Ksiazka.Count; i++)
                {
                    listai += Ksiazka[i].imie + ";";
                    listai += Ksiazka[i].nazwisko + ";";
                    listai += Ksiazka[i].nr + ";" + "\r\n";
                }
                sciezkadoksiazki.Write(listai);
            }
        }

        public void Odczytaj_z_pliku_lepsza_wersja(string sciezka)
        {
            try { 
            using (StreamReader sciezkadoodczutuksiazki = new StreamReader(sciezka)
               )
            {

                string wczytane;
                string[] tablica_wczytanych;
                Ksiazka.Clear();

                while (!sciezkadoodczutuksiazki.EndOfStream)
                {
                    wczytane = sciezkadoodczutuksiazki.ReadLine();
                    tablica_wczytanych = wczytane.Split(';');
                    string wczytane_imie = "";
                    string wczytane_nazwisko = "";
                    string wczytany_nr = "";
                    wczytane_imie = tablica_wczytanych[0];
                    wczytane_nazwisko = tablica_wczytanych[1];
                    wczytany_nr = tablica_wczytanych[2];
                    this.Dodaj(wczytane_imie, wczytane_nazwisko, wczytany_nr);

                }

            }
            }
            catch(FileNotFoundException)
            {

                    File.Create(sciezka);
            }
        }

    }


    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static  string plik_poczatkowy="a.txt";
        public static string sciezka_glowna = Path.Combine(Environment.CurrentDirectory, @"dane\", plik_poczatkowy);
        public static Phonebook phonebook = new Phonebook(sciezka_glowna);
        public bool edytuj_dodaj = false;

        
        public MainWindow()
        {
            InitializeComponent();

         
                string sciezka = sciezka_glowna;
                phonebook.Odczytaj_z_pliku_lepsza_wersja(sciezka);
                belka.Items.Add(sciezka);
                zaktualizuj_wyswietlanie();

        }
      
        public void zaktualizuj_wyswietlanie()
        {
            lista.Items.Clear();
            for (int i = 0; i <= phonebook.licznik()-1; i++)
            {
                string uzytkownik = phonebook.pobierz(i).imie +" " + phonebook.pobierz(i).nazwisko;
                lista.Items.Add(uzytkownik);
            }
        }
        
        private void Btndodaj1_Click(object sender, RoutedEventArgs e)
        {

            string imie = txt_imie.Text;
           string nazwisko = txt_nazwisko.Text;
            string numer = txt_numer.Text;
            phonebook.Dodaj(imie, nazwisko, numer);//jedno okno
            zaktualizuj_wyswietlanie();
            txt_imie.Clear();
            txt_nazwisko.Clear();
            txt_numer.Clear();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lista.SelectedIndex > -1)

                wys_nr.Content = phonebook.Wyswietl(lista.SelectedIndex);

        }

        private void Btn_usun_Click(object sender, RoutedEventArgs e)
        {
            int indeks = lista.SelectedIndex;
            if (lista.SelectedIndex > -1)
            {
                lista.Items.RemoveAt(lista.SelectedIndex);
                phonebook.Usun(indeks);
            }

        }

        private void Btn_edytuj_Click(object sender, RoutedEventArgs e)
        {
            int indeks = lista.SelectedIndex;
            if (lista.SelectedIndex == -1)
            {
                return;
            }
            User user = phonebook.pobierz(lista.SelectedIndex);
            string imie = txt_imie.Text;
            string nazwisko = txt_nazwisko.Text;
            string numer = txt_numer.Text;
            user.imie = imie;
            user.nazwisko = nazwisko;
            user.nr = numer;
            zaktualizuj_wyswietlanie(); 
            txt_imie.Clear();
            txt_nazwisko.Clear();
            txt_numer.Clear();
          
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int indeks = lista.SelectedIndex;
            string imie = "", nazwisko = "", numer = "", zbiornik = "";
            string[] t_zbiornik;
            if (lista.SelectedIndex > -1)
            {
                zbiornik = lista.SelectedItem.ToString();
                t_zbiornik = zbiornik.Split(' ');

                // txt_imie.Text = lista.SelectedItem.ToString();
                for (int i = 0; i < t_zbiornik.Length - 1; i += 2)
                {
                    imie = t_zbiornik[i];
                    nazwisko = t_zbiornik[i + 1];
                }
                txt_imie.Text = imie;
                txt_nazwisko.Text = nazwisko;
                txt_numer.Text = wys_nr.Content.ToString();

            }

        }

        private void zapiszdopliku(object sender, RoutedEventArgs e)
        {

            phonebook.Zapisz();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
     
        }

        private void wybierzplik(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string sciezka = openFileDialog.FileName;
                sciezka_glowna = sciezka;
                phonebook.sciezka = sciezka_glowna;
                belka.Items.Clear();
                belka.Items.Add(sciezka);
            }
        }

        private void odczytajzpliku(object sender, RoutedEventArgs e)
        {
            phonebook.Odczytaj_z_pliku_lepsza_wersja(sciezka_glowna);
            zaktualizuj_wyswietlanie();
        }

        private void btn_dodaj_okno_Click(object sender, RoutedEventArgs e)
        {
            edytuj_dodaj = true;
            Window2 win2 = new Window2(this);
            win2.Show();
        }

        private void btn_edytuj_okno_Click(object sender, RoutedEventArgs e)
        {
            int indeks = lista.SelectedIndex;
            if (lista.SelectedIndex == -1)  return;
            Window2 win2 = new Window2(this);
            win2.txt_imie.Text = phonebook.Ksiazka[indeks].imie;
            win2.txt_nazwisko.Text = phonebook.Ksiazka[indeks].nazwisko;
            win2.txt_nr.Text = phonebook.Ksiazka[indeks].nr;
            win2.Show();

        }

        private void Txt_imie_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }


    
}
