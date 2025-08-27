using System;                                  // Console, DateTime
using ZooManagementSystem.Core;                // Animal (har SayAndPlay/PlaySound)

namespace ZooManagementSystem.AnimalTypes
{
    public class Penguin : Animal
    {
        // Constructor: opretter pingvin og sender navn, art ("Penguin") og fødselsdato videre til Animal via base(...)
        public Penguin(string name, DateTime birthdate)
            : base(name, "Penguin", birthdate) { }

        // Artens tekst + lydfil (bruges af Animal.SayAndPlay)
        protected override string SoundText => "siger: Honk!";
        protected override string SoundFile => "pinguin-sound.mp3"; 

        // Kalder fælles helper (skriver tekst + afspiller lyd)
        public override void MakeSound() => SayAndPlay();

        // Ekstra funktion specifikt for pingvin
        public void PingvinSvmømmer()
        {
            Console.WriteLine($"{Name} ({Species}) svømmer i det kolde vand.");
        }

        public void PenguinMenu()
        {
            bool open = true;
            while (open)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║          Pingvin         ║");
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
                        Eat();               // fra Animal
                        Sleep();             // fra Animal
                        PingvinSvmømmer();   // pingvin-specifikt
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
