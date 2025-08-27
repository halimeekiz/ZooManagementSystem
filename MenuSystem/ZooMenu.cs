using System;
using System.Xml.Linq;
using ZooManagementSystem.AnimalTypes;                       // Lion, Giraffe, Elephant, Penguin
using ZooManagementSystem.Environment;
using ZooManagementSystem.People;

namespace ZooManagementSystem.MenuSystem
{
    internal class ZooMenu
    {
        public static void Show()
        {
            bool running = true;

            // ÉN fælles zoo-instans
            var zoo = new Zoo("Min Zoo");
            Seed(zoo); // fylder test-data i zoo'en

            while (running)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║        Hovedmenu         ║");
                Console.WriteLine("╠══════════════════════════╣");
                Console.WriteLine("║ 1. Besøgende             ║");
                Console.WriteLine("║ 2. Dyrepasser            ║");
                Console.WriteLine("╠══════════════════════════╣");
                Console.WriteLine("║ 3. Afslut                ║");
                Console.WriteLine("╚══════════════════════════╝");
                Console.Write("Vælg en mulighed: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        VisitorMenu.Show(zoo);
                        break;

                    case "2":
                        if (Zookeeper.RequireZooKeeperLogin())
                        {
                            ZooKeeperMenu.Show(zoo);   // fælles zoo-instans
                        }
                        else
                        {
                            Console.WriteLine("\nLogin mislykkedes. Tryk en tast for at gå tilbage...");
                            Console.ReadKey();
                        }
                        break;

                    case "3":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Ugyldigt valg. Prøv igen.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Laver et par indhegninger og dyr 
        private static void Seed(Zoo zoo)
        {
            var savanne = new Enclosure("Savannen", 5000);
            savanne.AddAnimal(new Lion("Simba", new DateTime(2018, 5, 1)));
            savanne.AddAnimal(new Giraffe("Gerda", new DateTime(2020, 3, 12)));

            var arktis = new Enclosure("Arktis", 1200);
            arktis.AddAnimal(new Penguin("Pingo", new DateTime(2022, 11, 5)));

            var elefanthus = new Enclosure("Elefanthuset", 3000);
            elefanthus.AddAnimal(new Elephant("Ella", new DateTime(2015, 7, 20)));

            zoo.AddEnclosure(savanne);
            zoo.AddEnclosure(arktis);
            zoo.AddEnclosure(elefanthus);
        }
    }
}
