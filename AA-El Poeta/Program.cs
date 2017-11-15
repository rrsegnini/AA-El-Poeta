using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA_El_Poeta
{
    class Program
    {
        static string poema20 = "Puedo escribir los versos más tristes esta noche.Escribir, por ejemplo: «La noche está estrellada," +
                "y tiritan, azules, los astros, a lo lejos.» " +
                "El viento de la noche gira en el cielo y canta." +
                "Puedo escribir los versos más tristes esta noche" +
                "Yo la quise, y a veces ella también me quiso." +
                "En las noches como ésta la tuve entre mis brazos" +
                "La besé tantas veces bajo el cielo infinito." +
                "Ella me quiso, a veces yo también la quería" +
                "Cómo no haber amado sus grandes ojos fijos." +
                "Puedo escribir los versos más tristes esta noche." +
                "Pensar que no la tengo.Sentir que la he perdido." +
                "Oír la noche inmensa, más inmensa sin ella." +
                "Y el verso cae al alma como al pasto el rocío." +
                "Qué importa que mi amor no pudiera guardarla." +
                "La noche está estrellada y ella no está conmigo" +
                "Eso es todo.A lo lejos alguien canta.A lo lejos." +
                "Mi alma no se contenta con haberla perdido." +
                "Como para acercarla mi mirada la busca" +
                "Mi corazón la busca, y ella no está conmigo." +
                "La misma noche que hace blanquear los mismos árboles." +
                "Nosotros, los de entonces, ya no somos los mismos." +
                "Ya no la quiero, es cierto, pero cuánto la quise." +
                "Mi voz buscaba el viento para tocar su oído.De otro.Será de otro.Como antes de mis besos." +
                "Su voz, su cuerpo claro.Sus ojos infinitos.Ya no la quiero, es cierto, pero tal vez la quiero." +
                "Es tan corto el amor, y es tan largo el olvido.Porque en noches como ésta la tuve entre mis brazos," +
                "Mi alma no se contenta con haberla perdido.Aunque éste sea el último dolor que ella me causa," +
                "y éstos sean los últimos versos que yo le escribo";
        
        

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




        /*public static Dictionary<string, int> createDictionary(IEnumerable<string> ngram)

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
        }*/

        public static OrderedDictionary CreateOrderedDictionary(IEnumerable<string> ngram)

        {
            ngram.GetEnumerator();

            OrderedDictionary dictionary =
            new OrderedDictionary();

            int i = 1;
            
            foreach (var item in ngram)
            {
                //dictionary.Contains
                if (dictionary.Contains(item))
                {
                    i = (int)dictionary[item];
                    
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
        public static int Manhattan(string goalPoem, string poem)
        {
            var a = Math.Abs(poem.GetHashCode()) - Math.Abs(goalPoem.GetHashCode());
            // Console.WriteLine(a);
            return a;
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



























        public static string GetPoem(string words)
        {
            Random rand = new Random();
            int number = rand.Next(1, 43);

            IEnumerable<string> ngram =
                Program.makeNgrams
                (words, 2);

            IEnumerable<string> ngram2 =
                Program.makeNgrams
                (words, 3);

            IEnumerable<string> ngram3 = ngram.Concat(ngram2);

            var r = new Random();
            //ngram3 = ngram3.OrderBy(i => r.Next());

            // ngram3.ToList().

            ngram3 = ngram3.OrderBy(a => Guid.NewGuid()).ToList();

            OrderedDictionary dictionary = CreateOrderedDictionary(ngram3);
            //Dictionary<string, int> dictionary = createDictionary(ngram3);
            //string output = String.Join(" ", dictionary);

            StringBuilder newDocument = new StringBuilder();

            //IEnumerable<string> newDocument = null;}



            /*foreach (var item in dictionary)
            {
                int YesNo = rand.Next(0, 3);
                //Console.WriteLine(YesNo);
                if (YesNo == 1)
                {
                    newDocument.Append(item.Key);
                    newDocument.Append(" ");
                    //Console.WriteLine(item);
                }
            }*/
            int num = 0;
            foreach (DictionaryEntry item in dictionary)
            {
                int YesNo = rand.Next(0, 5);
                //Console.WriteLine(YesNo);
                if (YesNo == 1)
                {
                    newDocument.Append(item.Key);
                    newDocument.Append(" ");
                    num++;
                    if (num == 5)
                    {
                        break;
                    }
                    //Console.WriteLine(item);
                }
            }




            string output = String.Join(" ", newDocument.ToString());
            return output;
            //yield return newDocument.ToString();
        }


        public static OrderedDictionary CreateDictionaryZero(IEnumerable<string> ngram)
        {

            OrderedDictionary dictionary =
            new OrderedDictionary();

            int i = 1;

            foreach (var item in ngram)
            {
 
                    dictionary.Add(item, 0);
                
                Console.WriteLine(dictionary[item]);  
            }

            return dictionary;
            
        }


        public static string Genetic(string words)
        {
            IEnumerable<string> ngramMeta = Program.makeNgrams("Puedo escribir los versos tristes esta noche", 2);
            OrderedDictionary dictionaryMeta = CreateOrderedDictionary(ngramMeta);
            //Dictionary<string, int> dictionaryMeta = createDictionary(ngramMeta);

            OrderedDictionary dictionaryMetaZeros = CreateDictionaryZero(ngramMeta);


            string outputGoal = String.Join(" ", ngramMeta);
            //string output = String.Join(" ", ngram);



            OrderedDictionary ordered = new OrderedDictionary();

            string poema = null;
            int distance = 0;
            for (int i = 0; i < 1000; i++)
            {
                poema = GetPoem(words);

                distance = Manhattan(outputGoal, poema);
                /*if (distance >= 0 && distance < 100)
                {
                    //break;
                    return poema;
                }*/
                if (!ordered.Contains(distance) && distance >= 0)
                {
                    /*if (distance < 1000)
                    {
                        return poema;
                    }*/
                    ordered.Add(distance, poema);

                }
                //Console.WriteLine(poema);
            }
            //ordered.Remove(ordered[0]);

            var normalOrderedDictionary = ordered.Cast<DictionaryEntry>()
                       .OrderBy(r => r.Key)
                       .ToDictionary(c => c.Key, d => d.Value);

            OrderedDictionary orderedFinal = new OrderedDictionary();
            foreach (var item in normalOrderedDictionary)
            {
                orderedFinal.Add(item.Key, item.Value);
                Console.WriteLine(item.Key + " " + item.Value);
            }


            return poema;
            //return Genetic(DictionaryToString(orderedFinal));

        }

        public static string DictionaryToString(OrderedDictionary NewWords)
        {
            string result = null;

            for (int i = 0; i < 40; i++)
            {
                //result += NewWords.Item
                result += NewWords[i];
            }

            return result;
        }
        static void Main(string[] args)
        {


           
            Console.WriteLine(Genetic(poema20));
            Console.ReadLine();
        }
    }
}
