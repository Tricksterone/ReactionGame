using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReactionGame
{
    class Program
    {
        public static List<Highscore> highscores = new List<Highscore>();
        public static HttpClient httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            Random random = new Random();

            while (true)
            {
                Console.Clear();
                Stopwatch stopwatch = new Stopwatch();


                Console.WriteLine("Tryck valfri tangent för att starta spelet!");

                Console.ReadKey(true);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nVänta lite");

                float waitTime = random.Next(500, 3000);
                while (Console.KeyAvailable != true && waitTime > 0)
                {
                    Thread.Sleep(100);
                    waitTime -= 100;
                    Console.Write(".");
                }

                if (waitTime > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nTjuvstart! Prova igen.");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nTryck NU!!\n");
                    stopwatch.Start();
                    Console.ReadKey(true);
                    stopwatch.Stop();

                    Console.ResetColor();
                    Console.Write("Det tog: " + stopwatch.ElapsedMilliseconds + " millisekunder! ");

                    if (IsNewHighscore(stopwatch.ElapsedMilliseconds))
                    {
                        RegisterNewHighscore(stopwatch.ElapsedMilliseconds);
                    }

                    await GetTop10(httpClient);
                }

                Console.ResetColor();
                Console.WriteLine("\nTryck på valfri tangent för att börja om, eller Q för att avsluta.");
                if (Console.ReadKey(true).Key == ConsoleKey.Q) Environment.Exit(0);
            }
        }

        private static bool IsNewHighscore(long elapsedMilliseconds)
        {
            if (highscores.Count == 0) return true;
            bool lowerFound = false;
            foreach (Highscore score in highscores)
            {
                if (score.Time <= elapsedMilliseconds)
                {
                    lowerFound = true;
                }
            }

            return !lowerFound;
        }

        static async void RegisterNewHighscore(long time)
        {
            var httpClient = new HttpClient();

            Console.Write("Nytt rekord!");
            Console.Write("\nSkriv ditt namn: ");

            Highscore highscore = new Highscore
            {
                Name = Console.ReadLine(),
                Time = time
            };

            var response = await httpClient.PostAsJsonAsync("http://localhost:5175/Highscores", highscore);
        }

        static async Task<List<Highscore>> GetTop10(HttpClient httpClient)
        {
            var highscores = await httpClient.GetFromJsonAsync<List<Highscore>>("http://localhost:5175/Highscores/top10");
            Console.WriteLine("\n\nHIGHSCORE:");

            foreach (var item in highscores)
            {
                Console.WriteLine(item);
            }
            return highscores;
        }

        static async Task<List<Highscore>> GetAllHighscores(HttpClient httpClient)
        {
            var highscores = await httpClient.GetFromJsonAsync<List<Highscore>>("http://localhost:5175/Highscores");
            Console.WriteLine("\n\nHIGHSCORE:");

            foreach (var item in highscores)
            {
                Console.WriteLine(item);
            }
            return highscores;
        }
    }
}
