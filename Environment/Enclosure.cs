using System;
using System.Collections.Generic;
using ZooManagementSystem.Core;

namespace ZooManagementSystem.Environment
{
    public class Enclosure
    {
        // Attributter
        public string Name { get; set; }
        public int Size { get; set; } // m²
        public List<Animal> Animals { get; set; }

        // Constructor
        public Enclosure(string name, int size)
        {
            Name = name;
            Size = size;
            Animals = new List<Animal>();
        }

        // Metoder
        public void AddAnimal(Animal animal)
        {
            Animals.Add(animal);
            Console.WriteLine($"{animal.Name} ({animal.Species}) er tilføjet til {Name}.");
        }

        // Har ikke tilføjet den til menuen, men sådan er metoden!

        public void RemoveAnimal(Animal animal)
        {
            if (Animals.Remove(animal))
            {
                Console.WriteLine($"{animal.Name} ({animal.Species}) er fjernet fra {Name}.");
            }
            else
            {
                Console.WriteLine($"{animal.Name} ({animal.Species}) blev ikke fundet i {Name}.");
            }
        }

        public void ListAnimals()
        {
            Console.WriteLine($"Indhegning: {Name}");
            if (Animals.Count == 0)
            {
                Console.WriteLine("  (ingen dyr her endnu)");
            }
            else
            {
                foreach (var animal in Animals)
                {
                    Console.WriteLine($"  - {animal.Name} ({animal.Species}), Alder: {animal.GetAge()} år");
                }
            }
        }
    }
}
