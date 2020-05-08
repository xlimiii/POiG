using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Pilkarze.Model
{
    internal class Pilkarz
    {

        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public uint Wiek { get; set; }
        public uint Waga { get; set; }

        public Pilkarz(string imie, string nazwisko, uint wiek, uint waga)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Wiek = wiek;
            Waga = waga;
        }

        public Pilkarz(string imie, string nazwisko) : this(imie, nazwisko, 30, 75) { }

        public Pilkarz(Pilkarz p)
        {
            Imie = p.Imie;
            Nazwisko = p.Nazwisko;
            Wiek = p.Wiek;
            Waga = p.Waga;
        }
        public Pilkarz()
        {

        }
        public override string ToString()
        {
            return $"{Nazwisko} {Imie} lat: {Wiek} waga: {Waga} kg";
        }
        public bool CzyTakiSam(Pilkarz p)
        {
            if (p.Nazwisko != Nazwisko)
                return false;
            if (p.Imie != Imie)
                return false;
            if (p.Wiek != Wiek)
                return false;
            if (p.Waga != Waga)
                return false;
            return true;
        }

    }
}
