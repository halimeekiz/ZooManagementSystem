using System;
using System.Collections.Generic;
using System.Linq;
using ZooManagementSystem.Core;
using ZooManagementSystem.Environment;

namespace ZooManagementSystem
{
    public static class AnimalListMenu
    {
        public static void Show(Zoo zoo)

        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║         Vælg dyr         ║");
                Console.WriteLine("╠══════════════════════════╣");
                Console.WriteLine("║ 1. Løve                  ║");
                Console.WriteLine("║ 2. Giraf                 ║");
                Console.WriteLine("║ 3. Elefant               ║");
                Console.WriteLine("║ 4. Pingvin               ║");
                Console.WriteLine("║ 5. Tilbage               ║");
                Console.WriteLine("╚══════════════════════════╝");
                Console.Write("Vælg et dyr: ");

                var choice = Console.ReadLine();

                if (choice == "5") { running = false; continue; }

                string species = choice switch
                {
                    "1" => "Lion",
                    "2" => "Giraffe",
                    "3" => "Elephant",
                    "4" => "Penguin",
                    _ => string.Empty
                };

                if (string.IsNullOrEmpty(species))
                {
                    Console.WriteLine("Ugyldigt valg. Prøv igen.");
                    Console.ReadKey();
                    continue;
                }

                // Dansk visningsnavn (kun til UI-tekst)
                string displaySpecies = species switch
                {
                    "Lion" => "Løve",
                    "Giraffe" => "Giraf",
                    "Elephant" => "Elefant",
                    "Penguin" => "Pingvin",
                    _ => species
                };

                // finder alle dyr af den art på tværs af indhegninger
                var matches = new List<Animal>();
                foreach (var enc in zoo.Enclosures)
                    matches.AddRange(enc.Animals.Where(a =>
                        a.Species.Equals(species, StringComparison.OrdinalIgnoreCase)));

                Console.Clear();
                if (matches.Count == 0)
                {
                    Console.WriteLine($"Ingen {displaySpecies} i zoologisk have endnu.");
                }
                else
                {
                    Console.WriteLine($"{displaySpecies} i zoologisk have:");
                    foreach (var a in matches)
                        Console.WriteLine($"- {a.Name} ({displaySpecies}), Alder: {a.GetAge()} år");
                }

                Console.WriteLine("\nTryk en tast for at gå tilbage...");
                Console.ReadKey();
            }
        }
    }
}

