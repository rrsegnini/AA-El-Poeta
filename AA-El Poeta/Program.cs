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
        static int Generations = 0;
        static Random rand = new Random();
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
                //
                //Console.WriteLine(item.Key + " " + item.Value);
            }
        }
        public static OrderedDictionary CreateOrderedDictionary(OrderedDictionary datos, IEnumerable<string> ngram)

        {


            OrderedDictionary dictionary = new OrderedDictionary();
            //DeepCopy(datos, dictionary);

            int i = 1;

            foreach (var item in ngram)
            {
                foreach (DictionaryEntry item2 in datos)
                {
                    if ((string)item2.Value == item)
                    {
                        dictionary.Add(item2.Key, item2.Value);
                    }
                }
                /*
                if (datos.Contains(item))
                {

                    i = (int)datos[item];

                    i++;
                    dictionary[item] = i;
                    //dictionary.Add(item, i);
                }
                else
                {
                    //dictionary.Add(item, 1);
                }
                //Console.WriteLine(item);  
            }*/
            }
            //PrintDictionary(dictionary);
                return dictionary;

                /*
                OrderedDictionary dictionary = new OrderedDictionary();
                DeepCopy(datos, dictionary);





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

                return dictionary;*/
           
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
            
            
            //int number = rand.Next(1, 43);


            //var r = new Random();
        


            StringBuilder newDocument = new StringBuilder();
            OrderedDictionary resultado = new OrderedDictionary();

            int num = 0;
            foreach (DictionaryEntry item in datos)
            {
                int YesNo = rand.Next(0, 3);
                //Console.WriteLine(YesNo);
                if (YesNo == 1)
                {
                    resultado.Add(item.Key, item.Value);

                    num++;
                    if (num == size)
                    {
                        break;
                    }
                    //Console.WriteLine(item);
                }
                else
                {
                    //resultado.Add(item.Key, 0);
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
                    dictionary.Add(i, item);
                    i++;
                }

            }
            //PrintDictionary(dictionary);
            return dictionary;

            /*OrderedDictionary dictionary =
            new OrderedDictionary();

            int i = 1;

            foreach (var item in ngram)
            {
                if (!dictionary.Contains(item))
                { 
                    dictionary.Add(item, 0);
                }
                  
            }

            return dictionary;*/

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

                if (distance >= 0 && distance < 1)
                {
                    Console.WriteLine("La distancia: " + distance);
                    return poema;
                }
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
                //Console.WriteLine(item.Key + " " + item.Value);
            }

            foreach (DictionaryEntry item in orderedFinal)
            {
                
                //Console.WriteLine(item.Key + " " + item.Value);
            }


            //return poema;
            return Genetic(datos, poemaMeta, orderedFinal);
            //return Genetic(datos, poemaMeta, poema);

        }




        public static OrderedDictionary CreateGeneration(OrderedDictionary datos, OrderedDictionary poemaMeta)
        {
            OrderedDictionary generation = new OrderedDictionary();
            OrderedDictionary individuo = new OrderedDictionary();
            for (int i = 0; i < 100; i++)
            {
                individuo = GetPoem(datos, 10);
                //PrintDictionary(individuo);
                //individuo.
                generation.Add(i, individuo);
            }

            Generations++;
            //Console.WriteLine("Generaciones: " + Generations);
            return generation;
        }

        public static OrderedDictionary Genetic2(OrderedDictionary datos, OrderedDictionary poemaMeta, OrderedDictionary ElementosPrometedores)
        {
            OrderedDictionary generation = new OrderedDictionary();

            if (ElementosPrometedores == null)
            {
                 generation = CreateGeneration(datos, poemaMeta);
            }
            else
            {
                //generation = new OrderedDictionary();
                    DeepCopy(ElementosPrometedores, generation);
            }



            int distance = 0;
            OrderedDictionary ordered = new OrderedDictionary();
            for (int i = 0; i<generation.Count; i++)
            
            {
     
                distance = CompareDictionaries(poemaMeta, (OrderedDictionary)generation[i]);
                Console.WriteLine(distance);

                if (!ordered.Contains(distance))
                {
                    ordered.Add(distance, (OrderedDictionary)generation[i]);
                }

            }

            var normalOrderedDictionary = ordered.Cast<DictionaryEntry>()
                      .OrderBy(r => r.Key)
                      .ToDictionary(c => c.Key, d => d.Value);

            OrderedDictionary orderedFinal = new OrderedDictionary();
            foreach (var item in normalOrderedDictionary)
            {
                orderedFinal.Add(item.Key, item.Value);
                //Console.WriteLine(item.Key + " " + item.Value);
                //PrintDictionary((OrderedDictionary)item.Value);
            }

               //PrintDictionary (Crossover(orderedFinal));
            return Crossover(orderedFinal);
            //return orderedFinal;

        }



        public static OrderedDictionary GetBestElement(OrderedDictionary dict)
        {
            return (OrderedDictionary)dict[0];
        }


        public static void PrintDictionary(OrderedDictionary dict)
        {
            foreach (DictionaryEntry item in dict)
            {
                Console.WriteLine(item.Key + "  " + item.Value);
            }
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

        public static OrderedDictionary Genetic3(OrderedDictionary datos, OrderedDictionary poemaMeta, OrderedDictionary ElementosPrometedores)
        {
            OrderedDictionary generation = new OrderedDictionary();

            if (ElementosPrometedores == null)
            {
                generation = CreateGeneration(datos, poemaMeta);
                
            }
            else
            {
                //generation = new OrderedDictionary();
                DeepCopy(ElementosPrometedores, generation);
            }



            int distance = 0;
            OrderedDictionary ordered = new OrderedDictionary();
            for (int i = 0; i < generation.Count; i++)

            {

                distance = CompareDict(poemaMeta, (OrderedDictionary)generation[i]);
                //Console.WriteLine(distance);

                if (!ordered.Contains(distance))
                {
                    ordered.Add(distance, (OrderedDictionary)generation[i]);
                }

            }

            var normalOrderedDictionary = ordered.Cast<DictionaryEntry>()
                      .OrderBy(r => r.Key)
                      .ToDictionary(c => c.Key, d => d.Value);

            OrderedDictionary orderedFinal = new OrderedDictionary();
            foreach (var item in normalOrderedDictionary)
            {
                orderedFinal.Add(item.Key, item.Value);
                //Console.WriteLine(item.Key + " " + item.Value);
                //PrintDictionary((OrderedDictionary)item.Value);
            }

            //PrintDictionary((orderedFinal));
            //return (orderedFinal);
            return orderedFinal;

        }


        public static OrderedDictionary SumDictionaries(OrderedDictionary dict1, OrderedDictionary dict2)
        {
            OrderedDictionary resultado = new OrderedDictionary();
            foreach (DictionaryEntry item in dict1)
            {
                resultado.Add(item.Key, item.Value);
            }
            int save = 0;
            foreach (DictionaryEntry item in dict2)
            {
                if ((int)item.Value != 0)
                {
                    save = (int)resultado[item.Key];
                    save += (int)item.Value;
                    resultado[item.Key] = save;
                }
            }

            return resultado;
        }

        public static OrderedDictionary Crossover(OrderedDictionary population)
        {
            bool first = true;
            int cont = 0;
            OrderedDictionary prev = new OrderedDictionary();
            OrderedDictionary NewElements = new OrderedDictionary();
            foreach (DictionaryEntry item in population)
            {
                /* if (first)
                 {
                     first = false;
                 }
                 else
                 {
                     NewElements.Add(cont, SumDictionaries(prev, (OrderedDictionary)item.Value));
                     cont++;
                 }
                 prev = (OrderedDictionary)item.Value;*/

                foreach (DictionaryEntry item2 in (OrderedDictionary)item.Value)
                {
                    if ((int)item2.Value == 1)
                    {
                        //Console.WriteLine(item2.Key);
                    }
                }

            }

            return NewElements;
        }

        


        public static OrderedDictionary NewGroup(OrderedDictionary datos, OrderedDictionary dict, OrderedDictionary poemaMeta)
        {
            OrderedDictionary generation = new OrderedDictionary();
            OrderedDictionary individuo = new OrderedDictionary();
            OrderedDictionary bestGen = new OrderedDictionary();

            int cont = 30;
            foreach (DictionaryEntry item in dict)
            {
                if (cont != 0)
                {
                    bestGen.Add(item.Key, item.Value);
                    cont--;
                }
                else
                {
                    break;
                }

            }

            for (int i = 0; i < 100; i++)
            {
                individuo = GetPoem(bestGen, poemaMeta.Count / 8);
                //PrintDictionary(individuo);
                //individuo.
                generation.Add(i, individuo);
            }

            /*for (int i = 80; i < 80; i++)
            {
                individuo = GetPoem(datos, poemaMeta.Count / 8);
                //PrintDictionary(individuo);
                //individuo.
                generation.Add(i, individuo);
            }*/

            Generations++;
            //Console.WriteLine("Generaciones: " + Generations);
            //PrintDictionary(generation);
            return generation;

            //return CreateGeneration(dict, poemaMeta) + CreateGeneration(datos, poemaMeta);
        }

        public static int CompareDictionaries(OrderedDictionary dict1, OrderedDictionary dict2)
        {
            int distance = 0;
            for (int i = 0; i < dict1.Count; i++)
            {
                distance += CalculateManhattanDistance((int)dict2[i], (int)dict1[i]);
                distance += CalculateChebyshevDistance((int)dict2[i], (int)dict1[i]);
                
                //Console.WriteLine(dict1[i] + "-" + dict2[i]);

            //Console.WriteLine(dict2[i]);
            }

            return distance;
        }


        public static int CompareDict(OrderedDictionary dict1, OrderedDictionary dict2)
        {
            int id_1 = 0;
            int id_2 = 0;
            int resultado = 0;
            foreach (DictionaryEntry item in dict1)
            {
                id_1 += (int)item.Key;
            }
            foreach (DictionaryEntry item in dict2)
            {
                id_2 += (int)item.Key;
            }

            resultado = CalculateManhattanDistance(id_1, id_2);

            return resultado;
        }

        static void Main(string[] args)
        {
            //string datos = readFile("C:/Users/CASA/source/repos/AA-El Poeta/all.txt");
            string datos = "Hola, soy Roberto Rojas Segnini. Me gustan los conejos soy Carlos amo los perros";
            //IEnumerable<string> ngramDatos1 = Program.makeNgrams(datos, 1);
            IEnumerable<string> ngramDatos2 = Program.makeNgrams(datos, 2);
            IEnumerable<string> ngramDatos3 = Program.makeNgrams(datos, 3);
            IEnumerable<string> ngramDatos4 = Program.makeNgrams(datos, 4);
            string outputDatos = String.Join("|||", ngramDatos2);
           // Console.WriteLine(outputDatos);

            OrderedDictionary dictDatos2 = CreateDictionaryZero(ngramDatos2.Concat(ngramDatos3).Concat(ngramDatos4));


            //string meta = "Here take my picture; though I bid farewell";
            string meta = "Hola me gustan los conejos";
            IEnumerable<string> ngramMeta = Program.makeNgrams(meta, 2);
            string outputMeta = String.Join("|||", ngramMeta);
            //Console.WriteLine(outputMeta);
            OrderedDictionary dictMeta = CreateOrderedDictionary(dictDatos2, ngramMeta);

            int distanciaBase = CompareDict(dictMeta, dictMeta);

            string poema = "I love you, man. Girl world peace";
            IEnumerable<string> ngramPoema2 = Program.makeNgrams(poema, 2);
            string output = String.Join("|||", ngramPoema2);

            OrderedDictionary dict = CreateOrderedDictionary(dictDatos2, ngramPoema2);

            Console.WriteLine(CompareDict(dictMeta, dict));

            //PrintDictionary(GetPoem(dictDatos2, ngramMeta.Count()));

            OrderedDictionary a =
            Genetic3(dictDatos2, dictMeta, null);
            OrderedDictionary datos2 = dictDatos2;
            int fin = 100;
            for (int i = 0; i < fin; i++)
            {
                a = Genetic3(datos2, dictMeta, a);
                if (i != fin - 1)
                {
                    //Console.WriteLine(i);
                    a = NewGroup(dictDatos2, a, dictMeta);
                }
                datos2 = NewGroup(dictDatos2, a, dictMeta);
            }

            //PrintDictionary(a);
            //PrintDictionary(GetBestElement(a));

            /*
            


            //Genetic2(dictDatos2, dictMeta, Genetic2(dictDatos2, dictMeta, Genetic2(dictDatos2, dictMeta, Genetic2(dictDatos2, dictMeta, null))));

            
            for (int i = 0; i < 1; i++)
            {
                a = Genetic2(dictDatos2, dictMeta, a);
            }

            a = Genetic2(dictDatos2, dictMeta, a);
            foreach (DictionaryEntry item in a)
            {
                Console.WriteLine("Hola");
                //if ((int)item.Value == 0) { Console.WriteLine(item.Key); }
                   
            }

           

            //Console.WriteLine(Genetic(poema20));*/
            Console.ReadLine();
        }
    }
}
