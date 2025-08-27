using System;
using ZooManagementSystem.Core;   // Animal (har SayAndPlay, PlaySound)

namespace ZooManagementSystem.AnimalTypes
{
    // Elefant nedarver fra Animal
    public class Elephant : Animal
    {
        // Constructor – sender videre til Animal
        public Elephant(string name, DateTime birthdate)
            : base(name, "Elephant", birthdate) { }

        // Artens tekst + lydfil (bruges af Animal.SayAndPlay)
        protected override string SoundText => "siger: Trumpet!";
        protected override string SoundFile => "elephant-sound.mp3";

        // Kalder fælles helper (skriver tekst + afspiller lyd)
        public override void MakeSound() => SayAndPlay();

        // Ekstra funktion specifik for elefant
        public void SprayWater()
        {
            Console.WriteLine($"{Name} ({Species}) sprøjter vand med snablen.");
        }

        // Menu for et elefant-individ (uændret struktur)
        public void ElephantMenu()
        {
            bool open = true;
            while (open)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║        Elefant           ║");
                Console.WriteLine("╠══════════════════════════╣");
                Console.WriteLine("║ 1. Vis oplysninger       ║");
                Console.WriteLine("║ 2. Hør lyden             ║");
                Console.WriteLine("║ 3. Daglig rutine         ║");
                Console.WriteLine("║ 4. Tilbage               ║");
                Console.WriteLine("╚══════════════════════════╝");
                Console.Write("Vælg en mulighed: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine($"\nNavn: {Name}\nArt: {Species}\nAlder: {GetAge()} år");
                        Console.WriteLine("\nTryk en tast for at fortsætte...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.WriteLine("\nAfspiller lyd...");
                        MakeSound();
                        Console.WriteLine("\nTryk en tast for at fortsætte...");
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.WriteLine("\nDaglig rutine:");
                        Eat();        // fra Animal
                        Sleep();      // fra Animal
                        SprayWater(); // elefant-specifikt
                        Console.WriteLine("\nTryk en tast for at fortsætte...");
                        Console.ReadKey();
                        break;

                    case "4":
                        open = false;
                        break;

                    default:
                        Console.WriteLine("Ugyldigt valg.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
