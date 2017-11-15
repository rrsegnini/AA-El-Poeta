using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA_El_Poeta
{
    class Program
    {
        public static IEnumerable<string> makeNgrams(string text, int nGramSize)
        {
            if (nGramSize == 0) throw new Exception("nGram size was not set");

            StringBuilder nGram = new StringBuilder();
            Queue<int> wordLengths = new Queue<int>();

            int wordCount = 0;
            int lastWordLen = 0;

            //append the first character, if valid.
            //avoids if statement for each for loop to check i==0 for before and after vars.
            if (text != "" && char.IsLetterOrDigit(text[0]))
            {
                nGram.Append(text[0]);
                lastWordLen++;
            }

            //generate ngrams
            for (int i = 1; i < text.Length - 1; i++)
            {
                char before = text[i - 1];
                char after = text[i + 1];

                if (char.IsLetterOrDigit(text[i])
                    ||
                    //keep all punctuation that is surrounded by letters or numbers on both sides.
                    (text[i] != ' '
                        && (char.IsSeparator(text[i]) || char.IsPunctuation(text[i]))
                        && (char.IsLetterOrDigit(before) && char.IsLetterOrDigit(after))
                        )
                    )
                {
                    nGram.Append(text[i]);
                    lastWordLen++;
                }
                else
                {
                    if (lastWordLen > 0)
                    {
                        wordLengths.Enqueue(lastWordLen);
                        lastWordLen = 0;
                        wordCount++;

                        if (wordCount >= nGramSize)
                        {
                            yield return nGram.ToString();
                            nGram.Remove(0, wordLengths.Dequeue() + 1);
                            wordCount -= 1;
                        }

                        nGram.Append(" ");
                    }
                }
            }
            nGram.Append(text.Last());
            yield return nGram.ToString();
        }




        public static Dictionary<string, int> createDictionary(IEnumerable<string> ngram)
        {
            ngram.GetEnumerator();

            Dictionary<string, int> dictionary =
            new Dictionary<string, int>();

            int i = 1;
            foreach (var item in ngram)
            {
                if (dictionary.ContainsKey(item))
                {
                    i = dictionary[item];
                    i++;
                    dictionary[item] = i;
                    //dictionary.Add(item, i);
                }
                else
                {
                    dictionary.Add(item, 1);
                }
                //Console.WriteLine(item);  
            }

            return dictionary;
        }
        //public static void Manhattan(Dictionary<string, int> dictionaryGoal, Dictionary<string, int> dictionary)
        public static void Manhattan(string goalPoem, string poem)
        {
            var a = Math.Abs(poem.GetHashCode()) - Math.Abs(goalPoem.GetHashCode());
            Console.WriteLine(a);
            /*
            foreach (var item in dictionary)
            {
                foreach (var item2 in dictionaryGoal)
                {
                    
                   var a = Math.Abs(item.GetHashCode())- Math.Abs(item2.GetHashCode());
                    Console.WriteLine(a);
                }
            }*/
        }

        public static string readFile(string _fileLocation) {
            string text = System.IO.File.ReadAllText(@_fileLocation);
            return text;
        }

        static void Main(string[] args)
        {
            IEnumerable<string> ngramMeta = Program.makeNgrams("Hola nombre mi asfasfLJKASLFJKH es Daniel Alvarado. Hola, mi nombre es Roberto Rojas Segnini", 2);
            Dictionary<string, int> dictionaryMeta = createDictionary(ngramMeta);

            IEnumerable<string> ngram = Program.makeNgrams("Hola mi nombre es Daniel Alvarado. Hola, mi nombre es Roberto Rojas Segnini", 2);
            Dictionary<string, int> dictionary = createDictionary(ngram);
         
            string outputGoal = String.Join(" ", ngramMeta);
            string output = String.Join(" ", ngram);

            Manhattan(outputGoal, output);
            

                //Console.WriteLine(output);
            Console.ReadLine();
        }
    }
}
