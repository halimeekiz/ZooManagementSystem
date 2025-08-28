using System;                     // Console, DateTime
using ZooManagementSystem.Core;   // Animal (har SayAndPlay/PlaySound)

namespace ZooManagementSystem.AnimalTypes
{
    // Jeg bruger nedarvning her, fordi Lion arver fra Animal.
    // På den måde genbruger jeg fælles egenskaber (Name, Species, Birthdate) fra baseklassen.

    public class Lion : Animal
    {
        // Constructor: giver navn og fødselsdato videre til Animal via base(...)
        public Lion(string name, DateTime birthdate)
            : base(name, "Lion", birthdate) { }

        // Artens tekst + lydfil (bruges af Animal.SayAndPlay)
        protected override string SoundText => "siger: Roar!";
        protected override string SoundFile => "lion-sound.mp3";

        // Polymorfi: Løven implementerer MakeSound – kalder fælles helper
        public override void MakeSound() => SayAndPlay();

        // Ekstra funktion specifikt for løver
        public void Hunt()
        {
            Console.WriteLine($"{Name} ({Species}) er på jagt.");
        }

        // Menu for løven
        public void LionMenu()
        {
            bool open = true;
            while (open)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║           Løve           ║");
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
                        Eat();   // fra Animal
                        Sleep(); // fra Animal
                        Hunt();  // specifikt for løven
                        Console.WriteLine("\nTryk en tast for at fortsætte...");
                        Console.ReadKey();
                        break;
                    case "4":
                        open = false;
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
