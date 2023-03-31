using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNFT.Services.Generators
{

    public static class PassphraseGenerator
    {
        private static string[] _dictionary;

        public static string Generate()
        {
            if (_dictionary == null)
            {
                // Load dictionary file into a string array
                _dictionary = System.IO.File.ReadAllLines("PassPhraseWords.txt");
            }

            // Select 5 random words from the dictionary and join them into a passphrase
            return string.Join(" ", Enumerable.Range(0, 5).Select(_ => GenerateRandomWord()));
        }

        private static string GenerateRandomWord()
        {
            // Create a random number generator
            Random random = new Random();

            // Select a random word from the dictionary
            return _dictionary[random.Next(_dictionary.Length)];
        }
    }
}

