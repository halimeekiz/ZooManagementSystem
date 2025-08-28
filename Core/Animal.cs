using System;
using System.IO;
using ZooManagementSystem; // AudioPlayer

namespace ZooManagementSystem.Core
{
    // Jeg bruger abstraktion her, fordi Animal er en abstrakt baseklasse.
    // Den definerer en kontrakt (fx SoundText og SoundFile), som alle dyr skal implementere,
    // men uden at fortælle hvordan det gøres i praksis.
    public abstract class Animal
    {
        // Attributter (Indkapsling af data om et dyr)
        public string Name { get; set; }         // Dyrets navn
        public string Species { get; set; }      // Art (bruges også til visning/logik)
        public DateTime Birthdate { get; set; }  // Fødselsdato (bruges til alder)

        // Constructor (Nedarvning: kaldes fra konkrete dyreklasser via base(...))
        public Animal(string name, string species, DateTime birthdate)
        {
            Name = name;
            Species = species;
            Birthdate = birthdate;
        }

        // Metoder

        // Jeg bruger polymorfi her, fordi Lion overskriver SoundText og SoundFile.
        // Når SayAndPlay() kaldes på en Animal, vil resultatet være forskelligt for hver dyretype.
        public abstract void MakeSound();

        // Virtuel standardadfærd – kan overskrives i underklasser (Polymorfi)
        public virtual void Eat()
        {
            Console.WriteLine($"{Name} ({Species}) spiser.");
        }
                
        public virtual void Sleep()
        {
            Console.WriteLine($"{Name} ({Species}) sover.");
        }

        // Udregner alder i hele år (simpel approx.: dage/365)
        public int GetAge()
        {
            return (DateTime.Now - Birthdate).Days / 365;
        }

        // Hver art specificerer selv tekst og filnavn
        protected abstract string SoundText { get; }   // fx "siger: Trumpet!"
        protected abstract string SoundFile { get; }   // fx "elephant-sound.mp3"

        // Skriver tekst og afspiler lyd (fælles for alle)
        protected void SayAndPlay()
        {
            Console.WriteLine($"{Name} ({Species}) {SoundText}");
            PlaySound(SoundFile);
        }

        // Afspil lydfil via AudioPlayer fra /AnimalSounds
        protected void PlaySound(string fileName)
        {
            AudioPlayer.Play(Path.Combine("AnimalSounds", fileName));
        }
    }
}
