using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace czas
{
    class Program
    {
        static int Czyliczba(string a, string zapyt)
        {
            int liczba;
            if (int.TryParse(a, out liczba) && liczba > 0)
            {
                return (liczba);
            }
            else
            {
                Console.WriteLine(zapyt);
                a = Console.ReadLine();
                liczba = Czyliczba(a, zapyt);
                return (liczba);
            }
        }
        static bool Linioweczas(int[] tab, int szukana)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] == szukana)
                {
                    return (true);
                }
            }
            return (false);
        }
        static int Linioweins(int[] tab, int szukana)
        {
            int licznik = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                licznik++;
                if (tab[i] == szukana)
                {
                    return (licznik);
                }
            }
            return (licznik);
        }
        static bool Binczas(int[] tab, int szukana)
        {
            int lewa = 0, prawa = tab.Length - 1, srodek;
            while (lewa <= prawa)
            {
                srodek = (lewa + prawa) / 2;
                if (tab[srodek] == szukana)
                {
                    return (true);
                }
                else if (tab[srodek] > szukana)
                {
                    prawa = srodek - 1;
                }
                else
                {
                    lewa = srodek + 1;
                }
            }
            return (false);
        }
        static int Binins(int[] tab, int szukana)
        {
            int lewa = 0, prawa = tab.Length - 1, srodek, licznik = 0;
            while (lewa <= prawa)
            {
                licznik++;
                srodek = (lewa + prawa) / 2;
                if (tab[srodek] == szukana)
                {
                    return (licznik);
                }
                else if (tab[srodek] > szukana)
                {
                    prawa = srodek - 1;
                }
                else
                {
                    lewa = srodek + 1;
                }
            }
            return (licznik);
        }
        static double Sredniaztablicy(double[] tab)
        {
            double suma = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                suma += tab[i];
            }
            return (suma / tab.Length);
        }
        static void Main(string[] args)
        {
            string path = @"C:\Nowy Folder\zapis.txt";
            StreamWriter plik = new StreamWriter(path);
            plik.WriteLine("liniowe max instrumentacja;liniowe max czas;binarne max instrumentacja;binarne max czas;liniowe średnia instrumentacja;liniowe średnia czas;binarne średnia instrumentacja;binarne średnia czas");
            Random rnd = new Random();
            int rozmiar, ile, pocz, kon, szukana;
            long czasstart, czasstop;
            Console.WriteLine("Witaj w algorytmie pomiaru czasu");
            rozmiar = Czyliczba("", "podaj rozmiar tablicy");
            ile = Czyliczba("", "podaj ile razy program ma przeprowadzic pomiar dla danej wielkosci tablicy");
            pocz = Czyliczba("", "podaj dolny zakres losowannych liczb");
            kon = Czyliczba("", "podaj gorny zakres losowanych liczb");

            for (int i = 100; i <= rozmiar; i += 1)
            {
                double[] srednilin = new double[ile];
                double[] sredniebin = new double[ile];
                double[] sredniczaslinn = new double[ile];
                double[] sredniczasbin = new double[ile];
                int maxlin = 0;
                int maxbin = 0;
                double maxczaslin = 0;
                double maxczasbin = 0;

                for (int z = 0; z < ile; z++)
                {
                    int[] tab1 = new int[i];
                    szukana = rnd.Next(pocz, kon);
                    for (int j = 0; j < i; j++)
                    {
                        tab1[j] = rnd.Next(pocz, kon);
                    }
                    Array.Sort(tab1);

                    srednilin[z] = Linioweins(tab1, szukana);

                    czasstart = Stopwatch.GetTimestamp();
                    Linioweczas(tab1, szukana);
                    czasstop = Stopwatch.GetTimestamp();
                    sredniczaslinn[z] = czasstop - czasstart;

                    sredniebin[z] = Binins(tab1, szukana);

                    czasstart = Stopwatch.GetTimestamp();
                    Binczas(tab1, szukana);
                    czasstop = Stopwatch.GetTimestamp();
                    sredniczasbin[z] = czasstop - czasstart;
                }
                int[] tab2 = new int[i];
                for (int j = 0; j < i; j++)
                {
                    tab2[j] = rnd.Next(pocz, kon);
                }
                Array.Sort(tab2);

                maxlin = Linioweins(tab2, kon + 1);

                czasstart = Stopwatch.GetTimestamp();
                Linioweczas(tab2, kon + 1);
                czasstop = Stopwatch.GetTimestamp();
                maxczaslin = czasstop - czasstart;

                maxbin = Binins(tab2, kon + 1);

                czasstart = Stopwatch.GetTimestamp();
                Binczas(tab2, kon + 1);
                czasstop = Stopwatch.GetTimestamp();
                maxczasbin = czasstop - czasstart;

                Console.Write("liniowe max instrumentacja:");
                Console.WriteLine(maxlin);
                Console.Write("liniowe max czas:");
                Console.WriteLine(maxczaslin);
                Console.Write("binarne max instrumentacja:");
                Console.WriteLine(maxbin);
                Console.Write("binarne max czas:");
                Console.WriteLine(maxczasbin);
                Console.Write("liniowe średnia instrumentacja:");
                Console.WriteLine(Sredniaztablicy(srednilin));
                Console.Write("liniowe średnia czas:");
                Console.WriteLine(Sredniaztablicy(sredniczaslinn));
                Console.Write("binarne średnia instrumentacja");
                Console.WriteLine(Sredniaztablicy(sredniebin));
                Console.Write("binarne średnia czas");
                Console.WriteLine(Sredniaztablicy(sredniczasbin));
                Console.WriteLine();
                double srlin = Sredniaztablicy(srednilin);
                double srczlin = Sredniaztablicy(sredniczaslinn);
                double srbin = Sredniaztablicy(sredniebin);
                double srczbin = Sredniaztablicy(sredniczasbin);
                plik.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7}", maxlin, maxczaslin, maxbin, maxczasbin, srlin, srczlin, srbin, srczbin);



            }
            plik.Close();
            Console.WriteLine("koniec");
            Console.ReadLine();
        }
    }
}
