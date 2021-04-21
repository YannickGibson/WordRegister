using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordRegister
{
    
    class Program
    {
        static void AddToRegistry(Dictionary<string, List<int>> registry, string key, int lineNumber)
        {
            if (!registry.ContainsKey(key)) // If word appears for first time
                registry[key] = new List<int>();

            registry[key].Add(lineNumber); // Add line number
        }
        static void Main(string[] args)
        {


            HashSet<char> allowedChars = new HashSet<char>() { };
            char[] alphabet = "abcdefghijklmnopqrstuvwxyz'".ToCharArray();
            foreach (char alph in alphabet)
            {
                allowedChars.Add(alph);
            }
            HashSet<string> prepositions = new HashSet<string>() { "aboard", "about", "above", "across", "after", "against", "along", "amid", "among", "around", " as", "at", "before", "behind", "below", "beneath", "beside", "between", "beyond", "but", "by", "concerning", "considering", "despite", "down", "during", "except", "except", "following", "for", "from", "in", "inside", "into", "like", "minus", "near", "next", "of", "off", "on", "onto", "opposite", "out", "outside", "over", "past", "per", "plus", "regarding", "round", "save", "since", "than", "through", "to", "toward", "under", "underneath", "unlike", "until", "up", "upon", "versus", "via", "with", "within", "without" };
            HashSet<string> conjunctions = new HashSet<string>() { "a", "a minute later", "accordingly", "actually", "after", "after a short time", "afterward", "also", "and", "another", "as an example", "as a consequence", "as a result", "as soon as", "at last", "at length", "because", "because of this", "before", "besides", "briefly", "but", "consequently", "conversely", "equally", "finally", "first", "for example", "for instance", "for this purpose", "for this reason", "fourth", "from here on", "further", "furthermore", "gradually", "hence", "however", "in addition", "in conclusion", "in contrast", "in fact", "in short", "in spite of", "in spite of this", "in summary", "in the end", "in the meanwhile", "in the meantime", "in the same manner", "in the same way", "just as important", "least", "last", "last of all", "lastly", "later", "meanwhile", "moreover", "nevertheless", "next", "nonetheless", "now", "nor", "of equal importance", "on the contrary", "on the following day", "on the other hand", "other hands", "or", "presently", "second", "similarly", "since", "so", "soon", "still", "subsequently", "such as", "the next week", "then", "thereafter", "therefore", "third", "thus", "to be specific", "to begin with", "to illustrate", "to repeat", "to sum up", "too", "ultimately", "what", "whatever", "whoever", "whereas", "whomever", "when", "while", "with this in mind", "yet" };

            
            string[] lines = File.ReadAllLines("ThreeMenInABoatEnglish.txt");
            Dictionary<string, List<int>> registry = new Dictionary<string, List<int>>();

            for (int i = 0; i < 7342; i++)
            {
                string line = lines[i].ToLower() + "."; // if line ends with word
                int startIndex = -1;
                bool streak = false;
                for (int k = 0; k < line.Length; k++)
                {
                    if ( allowedChars.Contains(line[k]) )
                    {
                        if (streak == false)
                        {
                            startIndex = k;
                            streak = true;
                        }
                        
                    }
                    else
                    {
                        if (streak)
                        {
                            string word = line.Substring(startIndex, k - startIndex).ToLower();

                            if (!prepositions.Contains(word) && !conjunctions.Contains(word))
                            {
                                AddToRegistry(registry, word, lineNumber: i + 1);
                            }

                            streak = false;
                        }
                    }

                }
            }
            string s = "";
            foreach (string key in registry.Keys.OrderBy(key => key))
            {
                s += $"{key} - {string.Join(", ", registry[key])}{Environment.NewLine}";
            }
            Console.WriteLine(s);

            File.WriteAllText("ThreeMenInABoatEnglish registry.txt", s);
            Console.ReadLine();
        }
    }
}
