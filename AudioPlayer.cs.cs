using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace ZooManagementSystem
{
    internal static class AudioPlayer
    {
        // Hold COM-objektet i live (Windows)
        private static object? _wmp;

        public static void Play(string relativePath)
        {
            var path = Path.IsPathRooted(relativePath)
                ? relativePath
                : Path.Combine(AppContext.BaseDirectory, relativePath);

            if (!File.Exists(path))
            {
                Console.WriteLine($"[INFO] Lydfil ikke fundet: {path}");
                return;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                try
                {
                    var t = Type.GetTypeFromProgID("WMPlayer.OCX", throwOnError: false);
                    if (t != null)
                    {
                        _wmp ??= Activator.CreateInstance(t)!;
                        dynamic player = _wmp;
                        player.settings.mute = false;
                        player.settings.volume = 100; // 0–100
                        player.URL = path;
                        player.controls.play();
                        return;
                    }
                    Console.WriteLine("[WMP] Windows Media Player COM-objekt ikke fundet.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[WMP] Kunne ikke afspille: {ex.Message}");
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                TryStart("afplay", $"\"{path}\"");
                return;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                if (TryStart("ffplay", $"-nodisp -autoexit \"{path}\"")) return;
                if (TryStart("mpg123", $"-q \"{path}\"")) return;
                TryStart("xdg-open", $"\"{path}\"");
                return;
            }

            Console.WriteLine("[INFO] Ukendt platform – ingen afspiller fundet.");
        }

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

        // (valgfrit) kald fx ved app-exit, hvis du vil frigive COM eksplicit:
        public static void Dispose()
        {
            if (_wmp != null)
            {
                try { Marshal.FinalReleaseComObject(_wmp); } catch { /* ignore */ }
                _wmp = null;
            }
        }
    }
}
