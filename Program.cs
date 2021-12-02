using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace upa_projekt2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Osoba> zaznam_osoba = File.ReadAllLines("theFile.txt")
                                           .Select(radek => Osoba.OsobyCsv(radek))
                                           .ToList();
            List<string> idicka = System.IO.File.ReadLines("idicka.txt").ToList();
            int index_osoby = 0;
            for (int i = 0; i < idicka.Count; i++)
            {
                Console.WriteLine(idicka[i]);
                index_osoby = zaznam_osoba.FindIndex(prod => prod.ID == Convert.ToInt32(idicka[i]));
                if (index_osoby != -1)
                    zaznam_osoba[index_osoby].Zaplaceno(Convert.ToInt32(idicka[i]));
            }            
            for (int i = 0; i < zaznam_osoba.Count; i++)
            {
                zaznam_osoba[i].VypisZpravu(true);
            }
            Console.ReadKey();
        }
    }

    public class Osoba
    {
        string Jmeno;
        string Prijmeni;
        string Email;
        public uint ID;
        public bool zaplaceno = false;

        public Osoba()
        { }
        //konstruktor
        public Osoba(string jmeno, string prijmeni, string email, uint id)
        {
            this.Jmeno = jmeno;
        }

        public static Osoba OsobyCsv(string radek_csv)
        {
            string[] sloupec = radek_csv.Split(',');
            Osoba osoba = new Osoba();
            osoba.Jmeno = sloupec[0];
            osoba.Prijmeni = sloupec[1];
            osoba.Email = sloupec[2];
            osoba.ID = Convert.ToUInt32(sloupec[3]);
            return osoba;
        }
        public void Tisk()
        {
            Console.Write("Jméno: {0},", Jmeno);
            Console.Write("Prijmeni: {0},", Prijmeni);
            Console.Write("Vyska: {0},", Email);
            Console.Write("Vyska: {0},", ID);
            Console.Write("Vyska: {0}", zaplaceno);
        }
        public void VypisZpravu(bool platci_vypis)
        {
            if (platci_vypis)
            {
                if (zaplaceno)
                {
                    Console.WriteLine("Zaplatili:");
                    Tisk();
                }
            }
            if (!platci_vypis)
            { 
                if (!zaplaceno)
                {
                    Console.WriteLine("Nezaplatili:");
                    Tisk();
                }
            }
        }
        public void VypisZpravu()
        {
            {
                Console.WriteLine("Vsichni");
                Tisk();
            }
        }
        public void Zaplaceno(int ID)
        {
            zaplaceno = true;
        }
    }
}