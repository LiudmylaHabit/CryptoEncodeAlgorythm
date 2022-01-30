using System;

namespace CryptoEncodeAlgorythm
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu.Greetings();
            var phrase = Menu.AskPhraseToEncrypt();
            var key = Menu.AskKey();
            var vigenerEncryptor = new Vigenere();
            var encryptedPhrase = vigenerEncryptor.Encrypt(key.ToCharArray(), phrase.ToCharArray());
            Console.WriteLine(new string(encryptedPhrase));
            Console.WriteLine("For decryption press any key");
            Console.ReadKey();
            Console.WriteLine(new string(vigenerEncryptor.Decrypt(key.ToCharArray(), encryptedPhrase)));
            Console.ReadKey();
        }
    }
}
