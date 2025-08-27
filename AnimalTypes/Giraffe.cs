using System;                     // Console, DateTime
using ZooManagementSystem.Core;   // Animal (har SayAndPlay/PlaySound)

namespace ZooManagementSystem.AnimalTypes
{
    public class Giraffe : Animal
    {
        // Constructor – sender videre til Animal
        public Giraffe(string name, DateTime birthdate)
            : base(name, "Giraffe", birthdate) { }

        // Artens tekst + lydfil (bruges af Animal.SayAndPlay)
        protected override string SoundText => "laver en dyb brummen.";
        protected override string SoundFile => "giraffee-sound.mp3"; 

        // Kalder fælles helper (skriver tekst + afspiller lyd)
        public override void MakeSound() => SayAndPlay();

        // Ekstra funktion specifikt for giraf
        public void BrowseLeaves()
        {
            Console.WriteLine($"{Name} ({Species}) spiser akacieblade med sin lange tunge.");
        }

        // Simpel konsolmenu for et giraf-individ
        public void GiraffeMenu()
        {
            bool open = true;
            while (open)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║          Giraf           ║");
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
                        Eat();          // fra Animal
                        Sleep();        // fra Animal
                        BrowseLeaves(); // giraf-specifikt
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
