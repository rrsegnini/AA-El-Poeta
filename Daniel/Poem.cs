using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace AA_PROJECT3
{
    public class Poem
    {

        string targetString = "Hola, soy conejos";
        string data = "Hola, soy Roberto Rojas Segnini. Me gustan los conejos";
        int populationSize = 200;
        float mutationRate = 0.01f;
        int elitism = 5;


        int numGenerations;
        private GeneticAlgorithm<char> ga;
        private System.Random random;

        void Start()
        {

            random = new System.Random();
            ga = new GeneticAlgorithm<char>(populationSize, targetString.Length, random, GetRandomString, FitnessFunction, elitism, mutationRate);
        }

        void Update()
        {
            ga.NewGeneration();

            //UpdateText(ga.BestGenes, ga.BestFitness, ga.Generation, ga.Population.Count, (j) => ga.Population[j].Genes);


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
            int i = random.Next(validCharacters.Length);
            return validCharacters[i];
        }

        private float FitnessFunction(int index)
        {
            float score = 0;
            DNA<char> dna = ga.Population[index];

            for (int i = 0; i < dna.Genes.Length; i++)
            {
                if (dna.Genes[i] == targetString[i])
                {
                    score += 1;
                }
            }

            score /= targetString.Length;

            score = (Mathf.Pow(2, score) - 1) / (2 - 1);

            return score;
        }

    }
}
