using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooManagementSystem.Environment;

namespace ZooManagementSystem.MenuSystem
{
    internal class VisitorMenu
    {
        public static void Show(Zoo zoo)
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║         Besøgende        ║");
                Console.WriteLine("╠══════════════════════════╣");
                Console.WriteLine("║ 1. Vis Dyr               ║");
                Console.WriteLine("║ 2. Åbningstider          ║");
                Console.WriteLine("║ 3. Billet Priser         ║");
                Console.WriteLine("╠══════════════════════════╣");
                Console.WriteLine("║ 4. Tilbage               ║");
                Console.WriteLine("╚══════════════════════════╝");
                Console.Write("Vælg en mulighed: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AnimalMenu.Show(zoo);
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("╔══════════════════════════════════════════╗");
                        Console.WriteLine("║       Åbningstider                       ║");
                        Console.WriteLine("╠══════════════════════════════════════════╣");
                        Console.WriteLine("║ Mandag - Fredag          10:00 - 19:00   ║");
                        Console.WriteLine("║ Lørdag - Søndag          10:00 - 18:00   ║");
                        Console.WriteLine("║ Hellidage                    Lukket      ║");
                        Console.WriteLine("╚══════════════════════════════════════════╝");
                        Console.WriteLine("\nTryk en tast for at fortsætte...");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("╔══════════════════════════════════════════╗");
                        Console.WriteLine("║           Priser for billetter           ║");
                        Console.WriteLine("╠══════════════════════════════════════════╣");
                        Console.WriteLine("║ Voksen (18+ år)                150 DKK   ║");
                        Console.WriteLine("║ Barn (3-17 år)                 75 DKK    ║");
                        Console.WriteLine("║ Barn under 3 år                Gratis    ║");
                        Console.WriteLine("║ Årskort                        1050 DKK  ║");
                        Console.WriteLine("╚══════════════════════════════════════════╝");
                        Console.WriteLine("\nTryk en tast for at fortsætte...");
                        Console.ReadKey();
                        break;
                    case "4":
                        running = false; // tilbage til ZooMenu
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg. Prøv igen.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
