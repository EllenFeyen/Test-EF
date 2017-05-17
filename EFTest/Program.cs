using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var entities = new LandenStedenTalenEntities())
            {
                var landenOpNaam = from landen in entities.Landen
                                   orderby landen.Naam
                                   select landen;
                Console.WriteLine("Landen op naam:");
                foreach (var land in landenOpNaam)
                {
                    Console.Write(land.LandCode);
                    Console.WriteLine("\t{0}", land.Naam);
                }

                Console.Write("Landcode:");
                var landcode = Console.ReadLine();
                try
                {
                    Land gekozenLand = entities.Landen.Where(l => l.LandCode == landcode).First();

                    Console.WriteLine("Steden:");
                    var stedenGekozenLand = entities.Steden.Where(stad => stad.LandCode == landcode);
                    foreach (var stad in stedenGekozenLand)
                    {
                        Console.WriteLine("\t{0}", stad.Naam);
                    }

                    Console.WriteLine("Talen:");
                    foreach (var taal in gekozenLand.Talen)
                    {
                        Console.WriteLine("\t{0}", taal.Naam);
                    }


                    Console.Write("Nieuwe stad:");
                    var naamStad = Console.ReadLine();
                    var nieweStad = new Stad { Naam = naamStad, LandCode = landcode };

                    entities.Steden.Add(nieweStad);
                    entities.SaveChanges();
                }
                catch (Exception)
                {
                    Console.WriteLine("Landcode niet gevonden");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Druk op enter om af te sluiten");
            Console.ReadLine();
        }
    }
}
