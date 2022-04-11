using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dart
{
    class Gra
    {

        public Gra(int legi, int gracze, int punkty = 501)
        {
            iloscLegow = legi;
            iloscGraczy = gracze;
            punktyDoWygrania = punkty;
        }
        public Gra()
        {

            iloscGraczy = odczytaniePoprawnej("Podaj ilosc graczy: ");

            iloscLegow = odczytaniePoprawnej("Podaj ilosc legow do wygranej: ");

            punktyDoWygrania = odczytaniePoprawnej("Podaj startowa ilosc licznika w legu: ");
            Console.Clear();
        }
        public Gra(bool pusty)
        {

        }
        public int iloscLegow { get; set; }
        public int iloscGraczy { get; set; }
        public int punktyDoWygrania { get; set; }

        public List<Zawodnik> gracze = new List<Zawodnik>();


        public int odczytaniePoprawnej(string tekst)
        {
            bool czyInt;
            int result;
            string s;
            do
            {

                Console.WriteLine(tekst);
                s = Console.ReadLine();
                czyInt = int.TryParse(s, out result);
                if (!czyInt)
                {
                    Console.WriteLine("Niepoprawna wartość. Spróbuj jeszcze raz!");
                    Console.WriteLine("Nacisnij dowolny przycisk........");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (!czyInt);

            return int.Parse(s);
        }
        public int odczytaniePoprawnej()
        {
            bool czyInt;
            int result;
            string s;
            do
            {
                s = Console.ReadLine();
                czyInt = int.TryParse(s, out result);
                if (!czyInt)
                {
                    Console.WriteLine("Niepoprawna wartość. Spróbuj jeszcze raz!");

                }
            } while (!czyInt);

            return int.Parse(s);
        }
        public int Rozgrywka()
        {
            do
            {
                for (int i = 0; i < iloscGraczy; i++)
                {
                    int indeks = i;
                    Console.WriteLine("Do wygrania potrzeba: " + iloscLegow + " legow");
                    Console.WriteLine("Obecnie:");
                    for (int j = 0; j < iloscGraczy; j++)
                    {

                        Console.Write((j + 1) + ".");
                        Console.WriteLine(gracze[j].ToString());
                    }
                    int w = wygrana();
                    if (w == iloscGraczy && !wygrMecz())
                    {
                        gracze[i].lotka += 3;
                        gracze[i].wszystkieLotki += 3;

                        Console.WriteLine("Ruch gracza: " + gracze[i].nazwa);
                        Console.WriteLine("Ile punktow: ");
                        int temp;

                        do
                        {
                            temp = odczytaniePoprawnej();
                        } while (!czyPoprawna(temp));


                        if (((gracze[i].punkty -= temp) < 0) || ((gracze[i].punkty == 0) && temp > 170) || (gracze[i].punkty == 1))
                        {
                            gracze[i].punkty += temp;

                            Console.WriteLine("Fura!");
                            Console.ReadKey();
                        }
                        else
                        {
                            if (gracze[i].punkty == 0)
                            {
                                Console.WriteLine("Ktora lotka: ");
                                int temp2 = odczytaniePoprawnej();
                                gracze[i].lotka = gracze[i].lotka - (3 - temp2);
                                gracze[i].wszystkieLotki = gracze[i].wszystkieLotki - (3 - temp2);
                            }
                            gracze[indeks].wszystkiePunkty += temp;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Lega wygrywa: " + gracze[w].nazwa);
                        gracze[w].wygrane_legi++;
                        Resetuj();
                        gracze.Add(gracze[0]);
                        gracze.RemoveAt(0);

                        i = iloscGraczy;

                        Console.WriteLine("Nacisnij dowolny przycisk zeby kontynuowac...");
                        Console.ReadKey();
                    }
                    Console.Clear();
                }
            } while (!wygrMecz());

            Console.WriteLine("Koniec gry:");

            foreach (var item in gracze)
            {
                item.iloscGier++;
                item.srednia3L();
            }
            gracze.Sort();
            for (int i = 0; i < iloscGraczy; i++)
            {
                Console.Write((i + 1) + ".");
                if (gracze[i].wygrane_legi == iloscLegow)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine(gracze[i].nazwa + ", legi:" + gracze[i].wygrane_legi + " Srednia na 3 lotki: " + gracze[i].srednia3L());
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.WriteLine(gracze[i].nazwa + ", legi:" + gracze[i].wygrane_legi + " Srednia na 3 lotki:  " + gracze[i].srednia3L());
                }
            }


            Console.WriteLine("Czy chcesz zapisac wyniki?");
            Console.WriteLine("Tak --- 1,  Nie --- Wybierz dowolna inna liczbe");
            int wybor = odczytaniePoprawnej(null);
            if (wybor == 1)
            {
                ZapiszWynikiDoPliku(); ;
            }

            Console.WriteLine("Nacisnij dowolny przycisk zeby kontynuowac...");
            Console.ReadKey();
            Console.Clear();
            return 0;
        }

        public void wyczysc()
        {
            /* using (Stream stream = new FileStream(@"dart.txt", FileMode.Create))
             {
                 using (StreamWriter sw = new StreamWriter(stream))
                 {
                     sw.WriteLine(string.Empty);
                 }
             }*/
            StreamWriter sw = new StreamWriter(@"dart.txt", false);
            sw.WriteLine(string.Empty);
            sw.Close();
        }

        public List<Zawodnik> Odczyt()
        {
            string path = @"dart.txt";

            if (!File.Exists(path))
            {
                var myFile = File.Create(path);
                myFile.Close();
            }

            string[] lines = System.IO.File.ReadAllLines(path);


            if (lines.Length > 0)
            {

                this.iloscGraczy = lines.Length - 1;

                List<Zawodnik> gracze2 = new List<Zawodnik>(iloscGraczy);
                for (int i = 1; i < lines.Length; i++)
                {
                    Zawodnik zawodnik = new Zawodnik(lines[i].Split(";")[0], double.Parse(lines[i].Split(";")[1]), int.Parse(lines[i].Split(";")[2]));
                    gracze2.Add(zawodnik);

                }
                //foreach (var item in gracze2)
                //{
                //    Console.WriteLine(item.nazwa + ", " + item.srednia3);
                //}

                return gracze2;
            }
            else
            {
                return gracze;
            }
        }


        public void Zapisz()
        {

            string path = @"dart.txt";

            StreamWriter sw;
            if (!File.Exists(path))
            {
                sw = File.CreateText(path);
            }
            else
            {
                gracze.Sort();
                sw = new StreamWriter(path, false);
            }
            sw.WriteLine("Ranking: ");


            foreach (var item in gracze)
            {
                sw.WriteLine(item.nazwa + ";" + item.srednia3 + ";" + item.iloscGier);
            }
            sw.Close();
        }





        private void ZapiszWynikiDoPliku()
        {

            string path = @"dart.txt";

            StreamWriter sw;
            if (!File.Exists(path))
            {
                sw = File.CreateText(path);
            }
            else
            {
                sw = new StreamWriter(path, true);
            }
            sw.Close();
            List<Zawodnik> odczytani = Odczyt();
            //File.Delete(path);

            //wyczysc();
            //Console.WriteLine("Odczytani: ");
            //foreach (var item in odczytani)
            //{
            //    Console.WriteLine(item.nazwa + ", " + item.srednia3);
            //}



            for (int i = 0; i < odczytani.Count(); i++)
            {
                if (gracze.Contains(odczytani[i]))
                {
                    int j = gracze.IndexOf(odczytani[i]);

                    gracze[j].srednia3 = Math.Round((odczytani[i].srednia3 * ((odczytani[i].iloscGier)) + gracze[j].srednia3) / gracze[j].iloscGier, 3);
                }
                else
                {
                    gracze.Add(odczytani[i]);
                }

            }
            gracze.Sort();
            sw = new StreamWriter(path, false);

            sw.WriteLine("Ranking: ");


            foreach (var item in gracze)
            {
                sw.WriteLine(item.nazwa + ";" + item.srednia3 + ";" + item.iloscGier);
            }
            sw.Close();
        }

        private bool czyPoprawna(int temp)
        {
            if (temp < 0 || temp > 180)
            {
                Console.WriteLine("Niepoprawna wartosc rzuconych punktow. Sprobuj jeszcze raz: ");
                return false;
            }
            return true;

        }

        private bool wygrMecz()
        {
            for (int i = 0; i < iloscGraczy; i++)
            {
                if (gracze[i].wygrane_legi == iloscLegow)
                    return true;
            }
            return false;
        }

        public void Resetuj()
        {
            foreach (var item in gracze)
            {
                item.punkty = punktyDoWygrania;
                item.lotka = 0;
            }

        }

        private int wygrana()
        {
            for (int i = 0; i < iloscGraczy; i++)
            {
                if (gracze[i].punkty == 0)
                    return i;
            }
            return iloscGraczy;

        }

    }
}
