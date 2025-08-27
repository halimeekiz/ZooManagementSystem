using System;
using System.Collections.Generic;
using ZooManagementSystem.Core;          // Animal
using ZooManagementSystem.Environment;   // Zoo
using ZooManagementSystem.AnimalTypes;   // Lion, Elephant, Giraffe, Penguin

namespace ZooManagementSystem.MenuSystem
{
    public static class AnimalMenu
    {
        public static void Show(Zoo zoo)
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║        Vælg Dyr          ║");
                Console.WriteLine("╠══════════════════════════╣");
                Console.WriteLine("║ 1. Løve                  ║");
                Console.WriteLine("║ 2. Elefant               ║");
                Console.WriteLine("║ 3. Giraf                 ║");
                Console.WriteLine("║ 4. Pingvin               ║");
                Console.WriteLine("╠══════════════════════════╣");
                Console.WriteLine("║ 5. Tilbage               ║");
                Console.WriteLine("╚══════════════════════════╝");
                Console.Write("Vælg en mulighed: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ÅbnArtsMenu(zoo, "Lion");
                        break;
                    case "2": 
                        ÅbnArtsMenu(zoo, "Elephant"); 
                        break;
                    case "3":
                        ÅbnArtsMenu(zoo, "Giraffe"); 
                        break;
                    case "4": 
                        ÅbnArtsMenu(zoo, "Penguin"); 
                        break;
                    case "5": running = false; 
                        break; // Tilbage
                    default:
                        Console.WriteLine("Ugyldigt valg. Prøv igen.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Hjælper: finder dyr af valgt art og åbn den korrekte menu
        private static void ÅbnArtsMenu(Zoo zoo, string species)
        {
            var animals = new List<Animal>();
            foreach (var enc in zoo.Enclosures)
                foreach (var a in enc.Animals)
                    if (a.Species.Equals(species, StringComparison.OrdinalIgnoreCase))
                        animals.Add(a);

            string display = species switch
            {
                "Lion" => "Løve",
                "Elephant" => "Elefant",
                "Giraffe" => "Giraf",
                "Penguin" => "Pingvin",
                _ => species
            };

            Console.Clear();

            if (animals.Count == 0)
            {
                Console.WriteLine($"Ingen {display.ToLower()} fundet.");
                Console.WriteLine("\nTryk en tast for at gå tilbage...");
                Console.ReadKey();
                return;
            }

            // Hvis kun én: åbn dens menu direkte
            Animal selected;
            if (animals.Count == 1)
            {
                selected = animals[0];
            }
            else
            {
                Console.WriteLine($"Vælg {display.ToLower()}:\n");
                for (int i = 0; i < animals.Count; i++)
                    Console.WriteLine($"{i + 1}. {animals[i].Name} – Alder: {animals[i].GetAge()} år");

                Console.Write("\nValg (Enter for tilbage): ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) return;

                if (!int.TryParse(input, out int idx) || idx < 1 || idx > animals.Count)
                {
                    Console.WriteLine("Ugyldigt valg. Prøv igen.");
                    Console.ReadKey();
                    return;
                }
                selected = animals[idx - 1];
            }

            // Åbn artens instans-menu
            if (selected is Lion lion) lion.LionMenu();
            else if (selected is Elephant elephant) elephant.ElephantMenu();
            else if (selected is Giraffe giraffe) giraffe.GiraffeMenu();
            else if (selected is Penguin penguin) penguin.PenguinMenu(); 
        }
    }
}
