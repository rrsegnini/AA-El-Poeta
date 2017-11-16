using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;

namespace AA_El_Poeta
//namespace AA_PROJECT3
{
    public class Poem
    {

        static string targetString = "Here take my picture; though I bid farewell";
        static public string[] targetStringList = createStringList(Program.makeNgrams(targetString, 2));
        //string data = "Hola, soy Roberto Rojas Segnini. Me gustan los conejos";
        string data = readFile("C:/Users/CASA/source/repos/AA-El Poeta/all.txt");
        int populationSize = 100;
        float mutationRate = 0.01f;
        int elitism = 5;


        int numGenerations;
        public GeneticAlgorithm<char> ga;
        private System.Random random;

        

        public void Start()
        {
            Console.WriteLine("Hola, hola");
            random = new System.Random();
            ga = new GeneticAlgorithm<char>(populationSize, targetString.Length, random, GetRandomString, FitnessFunction, elitism, mutationRate);
        }

        public void Update()
        {
            ga.NewGeneration();

            //UpdateText(ga.BestGenes, ga.BestFitness, ga.Generation, ga.Population.Count, (j) => ga.Population[j].Genes);
            if (ga.BestFitness == 1)
            {
                bool enabled = false;
            }

        }

        public static string readFile(string _fileLocation)
        {
            string text = System.IO.File.ReadAllText(@_fileLocation);
            return text;
        }

        public static int GetAmountOfElements(IEnumerable<string> ngram) {
            int counter = 0;
            foreach (var item in ngram)
            {
                counter++;

            }
            return counter;
        }

        public static string[] createStringList(IEnumerable<string> ngram)
        {
            int length = Poem.GetAmountOfElements(ngram);
            List<string> dictionary = new List<string>();

            foreach (var item in ngram)
            {
                dictionary.Add(item);

            }

            return dictionary.ToArray();

        }


        private string GetRandomString()
        {
            IEnumerable<string> ngramDatos2 = Program.makeNgrams(data, 2);
            int i = random.Next(ngramDatos2.Cast<object>().ToList().Count());
            
            

            return (string)ngramDatos2.Cast<object>().ToList().ElementAt(i);
            //return data[i];
        }

        private float FitnessFunction(int index)
        {
            float score = 0;
            //DNA<char> dna = ga.Population[index];
            Individual dna = ga.Population[index];

            //for (int i = 0; i < dna.genes.Length; i++)
            for (int i = 0; i < targetStringList.Count(); i++)
                {
                if (dna.genes[i] == targetStringList[i])
                {
                    score += 1;
                }
            }

            score /= targetString.Length;

            score = (float)(Math.Pow(2, score) - 1) / (2 - 1);

            return score;
        }

    }
}
