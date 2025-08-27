using System;
using ZooManagementSystem.Environment;
using ZooManagementSystem.People;


namespace ZooManagementSystem.MenuSystem
{
    internal class ZooKeeperMenu
    {
        // Bruger fælles Zoo-instans fra Hovedmenuen
        public static void Show(Zoo zoo)
        {
            // Opretter / "login" dyrepasser – al logik ligger i Zookeeper
            var keeper = Zookeeper.CreateFromPrompt();

            bool running = true;
            while (running)
            {
                Console.Clear();

                //  Recompute hver gang, så visningen følger evt. nyt valg
                var encName = keeper.AssignedEnclosure?.Name ?? "(ingen valgt)";

                Console.WriteLine("╔════════════════════════════╗");
                Console.WriteLine("║      Dyrepasser-menu       ║");
                Console.WriteLine("╠════════════════════════════╣");
                Console.WriteLine($"║ Dyrepasser: {keeper.Name}, {keeper.Age} år   ║");
                Console.WriteLine($"║ Indhegning: {encName}     ║");
                Console.WriteLine("╠════════════════════════════╣");
                Console.WriteLine("║ 1. Vælg/Skift indhegning   ║");
                Console.WriteLine("║ 2. Fodr dyr                ║");
                Console.WriteLine("║ 3. Gør rent                ║");
                Console.WriteLine("║ 4. Vis dyr i indhegning    ║");
                Console.WriteLine("║ 5. Daglig rutine           ║");
                Console.WriteLine("╠════════════════════════════╣");
                Console.WriteLine("║ 6. Tilbage                 ║");
                Console.WriteLine("╚════════════════════════════╝");
                Console.Write("Vælg en mulighed: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        keeper.SelectEnclosure(zoo); 
                        break;
                    case "2":
                        keeper.FeedAnimals();
                        break;
                    case "3":
                        keeper.CleanEnclosure(); 
                        break;
                    case "4": 
                        keeper.ShowAssignedAnimals(); 
                        break;
                    case "5": 
                        keeper.DailyRoutine(); 
                        break;
                    case "6": 
                        running = false; 
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg.");
                        keeper.Pause();
                        break;
                }
            }
        }
    }
}
