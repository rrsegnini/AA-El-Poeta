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

        public static void DeepCopy(OrderedDictionary datos, OrderedDictionary dictionary)
        {
            foreach (DictionaryEntry item in datos)
            {
                dictionary.Add(item.Key, item.Value);
                Console.WriteLine(item.Key + " " + item.Value);
            }
        }
        public static OrderedDictionary CreateOrderedDictionary(OrderedDictionary datos, IEnumerable<string> ngram)

        {
            ngram.GetEnumerator();
            OrderedDictionary dictionary = new OrderedDictionary();
            DeepCopy(datos, dictionary);

            

            /*OrderedDictionary dictionary =
            new OrderedDictionary();*/

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
                    //dictionary.Add(item, 1);
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





        public static string readFile(string _fileLocation)
        {
            string text = System.IO.File.ReadAllText(@_fileLocation);
            return text;
         }






        public static int CalculateManhattanDistance(int x1, int y1)
        {
            return Math.Abs(x1) + Math.Abs(y1);
        }

        public static int CalculateChebyshevDistance(int x1, int y1)
        {
            return Math.Max(Math.Abs(x1), Math.Abs(y1));
        }

        public static int CalculateOwnDistance(string _text1, string _text2)
        {
            int i, count = 0;
            for (i = 0; i < _text1.Length; i++)
            {
                if (_text1[i] != _text2[i])
                {
                    count++;
                }
            }
            return count;
        }










        public static OrderedDictionary GetPoem(OrderedDictionary datos, int size)
        {
            string output = "";
            
            Random rand = new Random();
            int number = rand.Next(1, 43);


            var r = new Random();
        


            StringBuilder newDocument = new StringBuilder();
            OrderedDictionary resultado = new OrderedDictionary();

            int num = 0;
            foreach (DictionaryEntry item in datos)
            {
                int YesNo = rand.Next(0, 5);
                //Console.WriteLine(YesNo);
                if (YesNo == 1)
                {
                    resultado.Add(item.Key, 1);

                    num++;
                    if (num == size)
                    {
                        break;
                    }
                    //Console.WriteLine(item);
                }
                else
                {
                    resultado.Add(item.Key, 0);
                }
            }



            //string output = String.Join(" ", newDocument.ToString());
            
            return resultado;
            //yield return newDocument.ToString();
        }


        public static OrderedDictionary CreateDictionaryZero(IEnumerable<string> ngram)
        {

            OrderedDictionary dictionary =
            new OrderedDictionary();

            int i = 1;

            foreach (var item in ngram)
            {
                if (!dictionary.Contains(item))
                { 
                    dictionary.Add(item, 0);
                }
                  
            }

            return dictionary;
            
        }


        public static OrderedDictionary Genetic(OrderedDictionary datos, OrderedDictionary poemaMeta, OrderedDictionary poema)
        {
            //string poema = null;
            //IEnumerable<string> ngramMeta = Program.makeNgrams("Puedo escribir los versos tristes esta noche", 2);
            //OrderedDictionary dictionaryMeta = CreateOrderedDictionary(ngramMeta, ngramMeta);
            //Dictionary<string, int> dictionaryMeta = createDictionary(ngramMeta);

            //OrderedDictionary dictionaryMetaZeros = CreateDictionaryZero(ngramMeta);


            //string outputGoal = String.Join(" ", ngramMeta);
            //string output = String.Join(" ", ngram);



            OrderedDictionary ordered = new OrderedDictionary();

            
            int distance = 0;
            for (int i = 0; i < 1000; i++)
            {
                poema = GetPoem(datos, 5);

                distance = CompareDictionaries(poemaMeta, poema);

                /*if (distance >= 0 && distance < 1)
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
               
            }
            

            var normalOrderedDictionary = ordered.Cast<DictionaryEntry>()
                       .OrderBy(r => r.Key)
                       .ToDictionary(c => c.Key, d => d.Value);

            OrderedDictionary orderedFinal = new OrderedDictionary();
            foreach (var item in normalOrderedDictionary)
            {
                orderedFinal.Add(item.Key, item.Value);
                Console.WriteLine(item.Key + " " + item.Value);
            }

    
            //return poema;
            return Genetic(datos, poemaMeta, poema);

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



        public static int CompareDictionaries(OrderedDictionary dict1, OrderedDictionary dict2)
        {
            int distance = 0;
            for (int i = 0; i < dict1.Count; i++)
            {
                distance += CalculateManhattanDistance((int)dict2[i], (int)dict1[i]);
                distance += CalculateChebyshevDistance((int)dict2[i], (int)dict1[i]);
                
                Console.WriteLine(dict1[i] + "-" + dict2[i]);

            //Console.WriteLine(dict2[i]);
            }

            return distance;
        }
    
        


        static void Main(string[] args)
        {
            //string datos = readFile("C:/Users/CASA/source/repos/AA-El Poeta/all.txt");
            string datos = "Hola, soy Roberto Rojas Segnini. Me gustan los conejos";
            IEnumerable<string> ngramDatos2 = Program.makeNgrams(datos, 2);
            string outputDatos = String.Join("|||", ngramDatos2);
            Console.WriteLine(outputDatos);

            OrderedDictionary dictDatos2 = CreateDictionaryZero(ngramDatos2);


            string meta = "I love you";
            IEnumerable<string> ngramMeta = Program.makeNgrams(meta, 2);
            string outputMeta = String.Join("|||", ngramMeta);
            Console.WriteLine(outputMeta);
            OrderedDictionary dictMeta = CreateOrderedDictionary(dictDatos2, ngramMeta);



            string poema = "Hola, me gustan los conejos. Soy Roberto Rojas Segnini";
            IEnumerable<string> ngramPoema2 = Program.makeNgrams(poema, 2);
            string output = String.Join("|||", ngramPoema2);

            OrderedDictionary dict = CreateOrderedDictionary(dictDatos2, ngramPoema2);

            foreach (DictionaryEntry item in dict)
            {
               // Console.WriteLine(item.Key + "  " + item.Value);
            }


            Console.WriteLine(CompareDictionaries(dictMeta, dict));
            //Console.WriteLine(output);




            foreach (DictionaryEntry item in Genetic(dictDatos2, dictMeta, new OrderedDictionary()))
            {
                Console.WriteLine(item.Key);
            }
            //Console.WriteLine(Genetic(poema20));
            Console.ReadLine();
        }
    }
}
