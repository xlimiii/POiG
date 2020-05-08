using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace MVVM_Pilkarze.Model
{
    class Druzyna
    {
        public List<Pilkarz> Sklad { get; set; }
        public Druzyna()
        {
            Sklad = new List<Pilkarz>();
            WczytajDruzyne(@"Pilkarze.json");
        }

        public void WczytajDruzyne(string plik)
        {
            if (File.Exists(plik))
            {
                string tekst = File.ReadAllText(plik);
                Sklad = JsonConvert.DeserializeObject<List<Pilkarz>>(tekst);   
            }
        }

        public void ZapiszDruzyne(string plik)
        {
            string jsonFormat = JsonConvert.SerializeObject(Sklad);
            File.WriteAllText(plik, jsonFormat);

        }

        public void  DodajPilkarza(Pilkarz p)
        {
            Sklad.Add(p);
        }

        public void UsunPilkarza(int i)
        {
            Sklad.RemoveAt(i);
        }

        public void EdytujPilkarza(Pilkarz p, int i)
        {
            Sklad[i] = p;
        }
        public List<Pilkarz> ZwrocPilkarza
        {
            get => Sklad;
        }


    }
}
