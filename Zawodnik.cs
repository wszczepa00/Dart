using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dart
{
    class Zawodnik : IComparable
    {
        public Zawodnik()
        {
            Console.WriteLine("Podaj nazwe gracza: ");
            nazwa = Console.ReadLine();
        }
        public Zawodnik(string nazwa, double srednia, int iloscgier)
        {
            this.nazwa = nazwa;
            this.srednia3 = srednia;
            this.iloscGier = iloscgier;
        }


        public double srednia3 { get; set; }
        public string nazwa { get; set; }
        public int punkty { get; set; }
        public int wygrane_legi { get; set; }
        public int lotka { get; set; }
        public float wszystkieLotki { get; set; }
        public float wszystkiePunkty { get; set; }
        public int iloscGier { get; set; }
        public int CompareTo(object obj)
        {
            Zawodnik zawodnik = (Zawodnik)obj;

            if (zawodnik.srednia3 > this.srednia3 || (zawodnik.srednia3 == this.srednia3 && zawodnik.wygrane_legi > this.wygrane_legi))
            {
                return 1;
            }
            else if (zawodnik.srednia3 == this.srednia3)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public override string ToString()
        {
            return $"{nazwa}: legi: {wygrane_legi}, licznik: {punkty}, lotka: {lotka}";
        }

        public double srednia3L()
        {
            if (wszystkieLotki != 0)
            {
                this.srednia3 = Math.Round(((wszystkiePunkty / wszystkieLotki) * 3), 3);
            }
            return Math.Round(this.srednia3, 3);
        }

        public override bool Equals(object obj)
        {
            Zawodnik zawodnik = (Zawodnik)obj;
            if (zawodnik.nazwa == this.nazwa)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }


}
