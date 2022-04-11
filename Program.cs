using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dart
{
    class Program
    {
        static void Main(string[] args)
        {
            string wybor;
            List<Zawodnik> odczytani;
            Console.WriteLine("Witaj w grze Dart.");
            Console.ReadKey();
            Console.Clear();
            do
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Graj.");
                Console.WriteLine("2. Ranking");
                Console.WriteLine("3. Usun zawodnika.");
                Console.WriteLine("4. Wyczysc ranking.");
                Console.WriteLine("5. Zakoncz gre.");
                Console.WriteLine("Wybierz liczbe: ");
                wybor = Console.ReadLine();
                Console.Clear();
                Gra gra;
                switch (wybor)
                {
                    case "1":
                        gra = new Gra();
                        Gra gra1 = new Gra(true);
                        odczytani = gra1.Odczyt();
                        int wybor2;
                        for (int i = 0; i < gra.iloscGraczy; i++)
                        {
                            Console.WriteLine("Ilosc graczy" + gra.iloscGraczy);
                            Console.WriteLine("Gracz nr: " + (i + 1));

                            if (odczytani.Count() == 0)
                            {
                                wybor2 = 1;
                            }
                            else
                            {
                                Console.WriteLine("Wybierz opcje wybrania gracza: ");
                                Console.WriteLine("1- Stworz gracza. ");
                                Console.WriteLine("2- Wybierz gracza. ");
                                wybor2 = gra.odczytaniePoprawnej();
                            }
                            Zawodnik gracz;

                            if (wybor2 == 1)
                            {
                                //Console.WriteLine("Gracz nr: " + (i + 1));
                                gracz = new Zawodnik();
                            }
                            else
                            {
                                int j = 1;
                                Console.WriteLine("Dostepni gracze: ");
                                foreach (var item in odczytani)
                                {
                                    Console.WriteLine((j++) + "." + item.nazwa + ", " + item.srednia3);
                                }

                                gracz = odczytani[(gra.odczytaniePoprawnej("Podaj nr zawodnika, ktorego chcesz wybrac do gry:  ")) - 1];
                            }
                            if (gra.gracze.Contains(gracz))
                            {
                                i--;
                                Console.WriteLine("Taki gracz juz istnieje!!!");
                                Console.ReadKey();
                            }
                            else
                            {
                                gracz.punkty = gra.punktyDoWygrania;
                                gra.gracze.Add(gracz);
                            }
                            Console.Clear();
                        }
                        odczytani.Clear();
                        gra.Rozgrywka();
                        break;

                    case "2":
                        Console.WriteLine("Ranking (srednia punktów; ilosc gier):");
                        gra = new Gra(false);
                        odczytani = gra.Odczyt();

                        foreach (var item in odczytani)
                        {
                            Console.WriteLine(item.nazwa + ": " + item.srednia3 + "; " + item.iloscGier);
                        }
                        odczytani.Clear();
                        Console.WriteLine("Nacisnij dowolny przycisk zeby kontynuowac.......");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case "3":

                        gra = new Gra(false);
                        gra.gracze = gra.Odczyt();

                        Console.WriteLine("Gracze (srednia punktów; ilosc gier): ");
                        foreach (var item in gra.gracze)
                        {
                            Console.WriteLine(item.nazwa + ": " + item.srednia3 + ";" + item.iloscGier);
                        }
                        Zawodnik zawodnik = new Zawodnik();

                        if (gra.gracze.Contains(zawodnik))
                        {
                            gra.gracze.RemoveAt(gra.gracze.IndexOf(zawodnik));
                            gra.Zapisz();
                        }
                        else
                        {
                            Console.WriteLine("Nie ma takiego zawodnika.");
                            Console.WriteLine("Nacisnij dowolny przycisk zeby kontynuowac.......");
                            Console.ReadKey();
                        }
                        gra.gracze.Clear();
                        Console.Clear();
                        break;

                    case "4":

                        gra = new Gra(false);
                        gra.wyczysc();

                        Console.WriteLine("Ranking wyczyszczony");
                        break;
                    case "5":
                        Console.WriteLine("Koniec gry!");
                        break;
                }

            } while (wybor != "5");

        }
    }
}
