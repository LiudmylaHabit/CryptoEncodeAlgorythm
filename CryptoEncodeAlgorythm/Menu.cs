using System;
using System.Text.RegularExpressions;

namespace CryptoEncodeAlgorythm
{
    public static class Menu
    {
        public static void Greetings()
        {
            Console.WriteLine("Hi there!");
        }

        // Ограничение для длины ключа - не менее 3х символов
        public static string AskKey(bool checkLength)
        {
            string keyPhrase = string.Empty;
            do
            {
                Console.WriteLine("Please write key-phrase:");
                keyPhrase = Console.ReadLine().Trim();
                if (keyPhrase.Length < 3 && checkLength)
                    Console.WriteLine("The key phrase is to silly! Try again");
            }
            while (keyPhrase.Length < 3);
            return keyPhrase.ToUpper();
        }

        public static string AskPhraseToEncrypt()
        {
            string phrase = string.Empty;
            do
            {
                Console.WriteLine("Please write phrase:");
                phrase = Console.ReadLine().Trim();
                if (phrase.Length < 1)
                    Console.WriteLine("The phrase is empty! Try again");
                else if (!Regex.Match(phrase, @"[a-z]").Success)
                    Console.WriteLine("The phrase contains nothing to encode. Please, try again!");
                else return phrase;
            }
            while (true);
        }

        public static void GoNext()
        {
            for (int i = 0; i < 25; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine("\nYOUR DATA IS NOT SECURE!");
            Console.WriteLine("\nTo go next press any key...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}