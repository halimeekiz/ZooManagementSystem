using System;                               // Console, AppContext
using System.Diagnostics;                   // ProcessStartInfo
using System.IO;                            // Path, File
using System.Runtime.InteropServices;       // RuntimeInformation
using WMPLib;                               // Windows Media Player (Windows)

namespace ZooManagementSystem
{
    // Denne klasse er en lille hjælper, der gør det nemt at afspille dyre-lyde på alle platforme
    // uden at blande OS-logik ind i dine dyreklasser. På Windows bruger den Windows Media Player (WMPLib).
    // På macOS kalder den afplay, og på Linux prøver den ffplay/mpg123 (falder tilbage til xdg-open).
    internal static class AudioPlayer
    {
        // Holder WMP i live (ellers kan GC stoppe lyden)
        private static WindowsMediaPlayer? _wmp;

        // Afspiler lydfil. relativePath: fx "AnimalSounds/lion-sound.mp3"
        public static void Play(string relativePath)
        {
            // Fuld sti ud fra programmets outputmappe
            var path = Path.IsPathRooted(relativePath)
                ? relativePath
                : Path.Combine(AppContext.BaseDirectory, relativePath);

            if (!File.Exists(path))
            {
                Console.WriteLine($"[INFO] Lydfil ikke fundet: {path}");
                return;
            }

            // Windows → brug WMPLib
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                try
                {
                    _wmp ??= new WindowsMediaPlayer();
                    _wmp.settings.mute = false;
                    _wmp.settings.volume = 100;   // 0–100
                    _wmp.URL = path;
                    _wmp.controls.play();
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[WMP] Kunne ikke afspille: {ex.Message}");
                }
            }
            // macOS → bruger systemets 'afplay'
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                TryStart("afplay", $"\"{path}\"");
                return;
            }
            // Linux → prøv ffplay/mpg123, ellers xdg-open (GUI)
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                if (TryStart("ffplay", $"-nodisp -autoexit \"{path}\"")) return;
                if (TryStart("mpg123", $"-q \"{path}\"")) return;
                TryStart("xdg-open", $"\"{path}\"");
                return;
            }

            Console.WriteLine("[INFO] Ukendt platform – ingen afspiller fundet.");
        }

        // Starter et CLI-program stille (returnerer false hvis ikke findes)
        private static bool TryStart(string fileName, string args)
        {
            try
            {
                var psi = new ProcessStartInfo(fileName, args)
                {
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                Process.Start(psi);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
