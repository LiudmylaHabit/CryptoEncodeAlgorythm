using System;
using System.Collections;
using System.Text;

namespace CryptoEncodeAlgorythm
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu.Greetings();
            Vigenere();
            Menu.GoNext();
            RunSha1();
            Console.ReadKey();
        }
        
        static void Vigenere()
        {
            Console.WriteLine("Vigenere");
            var phrase = Menu.AskPhraseToEncrypt().ToUpper();
            var key = Menu.AskKey(true);
            var vigenerEncryptor = new Vigenere();
            var encryptedPhrase = vigenerEncryptor.Encrypt(key.ToCharArray(), phrase.ToCharArray());
            Console.WriteLine(new string(encryptedPhrase));
            Console.WriteLine("For decryption press any key");
            Console.ReadKey();
            Console.WriteLine(new string(vigenerEncryptor.Decrypt(key.ToCharArray(), encryptedPhrase)));
        }

        static void RunSha1()
        {
            Console.WriteLine("SHA1");
            uint[] hash = Sha1.CreateSHA1Text(Menu.AskPhraseToEncrypt());
            var res = string.Empty;
            foreach (var item in hash)
            {
                res += String.Format("{0:x}", item, 16).ToString();
            }
            Console.WriteLine(res);
        }
    }
}
