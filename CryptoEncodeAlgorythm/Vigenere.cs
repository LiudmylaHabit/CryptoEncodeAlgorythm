using System;
using System.Collections.Generic;

namespace CryptoEncodeAlgorythm
{
    public class Vigenere
    {
        private char[] _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        private int GetSymbolIndex(char symbol)
        {
            for (int i = 0; i < _alphabet.Length; i++)
            {
                if (_alphabet[i] == symbol)
                    return i;
            }
            return -1;
        }

        public char[] Encrypt(char[] key, char[] phrase)
        {
            var encryptedPhrase = new List<char>();
            var j = 0;

            foreach (var symbol in phrase)
            {
                if (Char.IsLetter(symbol))
                {                   
                    var p = GetSymbolIndex(symbol);
                    var k = GetSymbolIndex(key[j % key.Length]);
                    var c = (p + k) % _alphabet.Length;
                    encryptedPhrase.Add(_alphabet[c]);
                    j++;
                }
                else encryptedPhrase.Add(symbol);
            }
            return encryptedPhrase.ToArray();
        }

        public char[] Decrypt(char[] key, char[] encryptedPhrase)
        {
            var decryptedPhrase = new List<char>();
            var j = 0;

            foreach (var symbol in encryptedPhrase)
            {
                if (Char.IsLetter(symbol))
                {
                    var p = GetSymbolIndex(symbol);
                    var k = GetSymbolIndex(key[j % key.Length]);
                    var c = (p - k) % _alphabet.Length;
                    if (c < 0) 
                        c = _alphabet.Length + c;
                    decryptedPhrase.Add(_alphabet[c]);
                    j++;
                }
                else decryptedPhrase.Add(symbol);
            }
            return decryptedPhrase.ToArray();
        }
    }
}