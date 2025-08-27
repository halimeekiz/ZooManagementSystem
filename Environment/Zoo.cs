using System;
using System.Collections.Generic;            // List<T>
using System.Text.Json;                      // JsonSerializer
using System.IO;                             // File, InvalidDataException

namespace ZooManagementSystem.Environment
{
    // Repræsenterer hele zoologisk have (navn + indhegninger)
    public class Zoo
    {
        // Attributter
        public string Name { get; set; }
        public List<Enclosure> Enclosures { get; set; }

        // Constructor
        public Zoo(string name)
        {
            Name = name;
            Enclosures = new List<Enclosure>();
        }

        // Metoder
        public void AddEnclosure(Enclosure enclosure)
        {
            Enclosures.Add(enclosure);
            Console.WriteLine($"{enclosure.Name} er tilføjet til {Name}.");
        }

        // Simulerer en dag i zoologisk have
        // Bruger polymorfi til at håndtere forskellige dyrearter
        // og deres specifikke lyde
        // (fodring, rengøring, lyde)
        // Eksempel på brug af lister og iteration
        // Håndterer tomme lister (ingen indhegninger eller dyr)

        public void DailyRoutine()
        {
            Console.WriteLine($"--- Daglig rutine i {Name} ---\n");

            if (Enclosures.Count == 0)
            {
                Console.WriteLine("(ingen indhegninger endnu)\n");
                return;
            }

            foreach (var enclosure in Enclosures)
            {
                Console.WriteLine($"I indhegning: {enclosure.Name}");
                Console.WriteLine($"Fodrer dyr i {enclosure.Name}...");
                Console.WriteLine($"Gør rent i {enclosure.Name}...");

                if (enclosure.Animals.Count == 0)
                {
                    Console.WriteLine("(ingen dyr i denne indhegning)\n");
                    continue;
                }
                // Polymorfi: kalder korrekt MakeSound() pr. dyretype
                foreach (var animal in enclosure.Animals)
                    animal.MakeSound();

                Console.WriteLine(); // tom linje mellem indhegninger
            }
        }
        // Indlæs en Zoo fra JSON-fil (kaster hvis filen ikke indeholder gyldig Zoo)
        public static Zoo LoadFromJson(string filename)
        {
            string json = File.ReadAllText(filename);                // læs hele filen
            var result = JsonSerializer.Deserialize<Zoo>(json);   
            if (result == null) throw new InvalidDataException("Filen indeholder ikke en gyldig Zoo.");
            return result;
        }

        // Oprettede til senere brug 
        public void ListAllAnimals()
        {
            Console.WriteLine($"Dyr i {Name}:\n");

            if (Enclosures.Count == 0)
            {
                Console.WriteLine("  (ingen indhegninger endnu)\n");
                return;
            }

            foreach (var enclosure in Enclosures)
            {
                enclosure.ListAnimals();
                Console.WriteLine();                    // tom linje mellem indhegninger
            }
        }
    }
}
