using System;
using ZooManagementSystem.Environment;


namespace ZooManagementSystem.People
{
    public class Zookeeper
    {
        // Attributter
        public string Name { get; set; }
        public int Age { get; set; }
        public Enclosure? AssignedEnclosure { get; set; }  // ← nullable

        // Constructor
        public Zookeeper(string name, int age, Enclosure? assignedEnclosure = null)
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Ukendt" : name.Trim();
            Age = age > 0 ? age : 30;
            AssignedEnclosure = assignedEnclosure;
        }

        // Login kan kaldes fra ZooMenu før man åbner Dyrepasser-menuen
        public static bool RequireZooKeeperLogin()
        {
            const string USER = "keeper";   // Brugernavn
            const string PASS = "zoo123";   // Adgangskode
            const int MAX_TRIES = 3;

            for (int tryNo = 1; tryNo <= MAX_TRIES; tryNo++)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║     Dyrepasser login     ║");
                Console.WriteLine("╚══════════════════════════╝\n");

                Console.Write("Brugernavn: ");
                string? u = Console.ReadLine();

                Console.Write("Adgangskode: ");
                string p = ReadPassword();

                if (u == USER && p == PASS)
                {
                    Console.WriteLine("\nLogin godkendt.");
                    System.Threading.Thread.Sleep(600);
                    return true;
                }

                Console.WriteLine("\nForkert brugernavn eller adgangskode.");
                if (tryNo < MAX_TRIES)
                {
                    Console.WriteLine($"Forsøg tilbage: {MAX_TRIES - tryNo}");
                    Console.WriteLine("Tryk en tast for at prøve igen...");
                    Console.ReadKey();
                }
            }
            return false;
        }
        private static string ReadPassword()
        {
            var pwd = string.Empty;
            ConsoleKeyInfo key;
            while (true)
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) 
                    break;

                if (key.Key == ConsoleKey.Backspace)
                {
                    if (pwd.Length > 0)
                    {
                        pwd = pwd[..^1];
                        Console.Write("\b \b");
                    }
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    pwd += key.KeyChar;
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            return pwd;
        }

        // Opret/"login" dyrepasser (let prompt) 
        public static Zookeeper CreateFromPrompt()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════╗");
            Console.WriteLine("║    Opret dyrepasser      ║");
            Console.WriteLine("╚══════════════════════════╝\n");

            Console.Write("Navn: ");
            string? n = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(n)) n = "Ukendt";

            Console.Write("Alder: ");
            if (!int.TryParse(Console.ReadLine(), out int a) || a <= 0) a = 30;

            // starter uden valgt indhegning (vælges i menuen)
            return new Zookeeper(n, a, null);
        }

        // Handlinger kaldes fra ZooKeeperMenu
        public void SelectEnclosure(Zoo zoo)
        {
            if (zoo.Enclosures.Count == 0)
            {
                Console.WriteLine("\nDer findes ingen indhegninger endnu.");
                Pause();
                return;
            }

            Console.Clear();
            Console.WriteLine("Vælg indhegning:\n");

            for (int i = 0; i < zoo.Enclosures.Count; i++)
                Console.WriteLine($"{i + 1}. {zoo.Enclosures[i].Name} ({zoo.Enclosures[i].Size} m²)");

            Console.Write("\nValg (Enter for tilbage): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return;

            if (int.TryParse(input, out int idx) && idx >= 1 && idx <= zoo.Enclosures.Count)
            {
                AssignedEnclosure = zoo.Enclosures[idx - 1];
                Console.WriteLine($"\n{Name} er nu tilknyttet: {AssignedEnclosure.Name}");
            }
            else
            {
                Console.WriteLine("Ugyldigt valg.");
            }
            Pause();
        }
        public void FeedAnimals()
        {
            if (!HasValidEnclosure())
            {
                Pause();
                return;
            }
            Console.WriteLine($"{Name} fodrer dyrene i {AssignedEnclosure!.Name}.");
            Pause();
        }

        public void CleanEnclosure()
        {
            if (!HasValidEnclosure())
            {
                Pause();
                return;
            }
            Console.WriteLine($"{Name} gør rent i {AssignedEnclosure!.Name}.");
            Pause();
        }
        public void ShowAssignedAnimals()
        {
            if (!HasValidEnclosure())
            {
                Pause();
                return;
            }

            Console.WriteLine();
            AssignedEnclosure!.ListAnimals();
            Pause();
        }

        public void DailyRoutine()
        {
            if (!HasValidEnclosure())
            { 
                Pause(); 
                return;
            }

            Console.WriteLine($"\n--- Daglig rutine i {AssignedEnclosure!.Name} ---");
            FeedAnimalsCore();
            CleanEnclosureCore();

            if (AssignedEnclosure!.Animals.Count == 0)
            {
                Console.WriteLine("(ingen dyr i denne indhegning)");
            }
            else
            {
                foreach (var a in AssignedEnclosure.Animals)
                    a.MakeSound();
            }
            Pause();
        }

        private bool HasValidEnclosure()
        {
            if (AssignedEnclosure == null)
            {
                Console.WriteLine("\nVælg først en indhegning (punkt 1).");
                return false;
            }
            return true;
        }

        private void FeedAnimalsCore() => Console.WriteLine($"{Name} fodrer dyrene i {AssignedEnclosure!.Name}.");
        private void CleanEnclosureCore() => Console.WriteLine($"{Name} gør rent i {AssignedEnclosure!.Name}.");

        public void Pause()
        {
            Console.WriteLine("\nTryk en tast for at fortsætte...");
            Console.ReadKey();
        }
    }
}
