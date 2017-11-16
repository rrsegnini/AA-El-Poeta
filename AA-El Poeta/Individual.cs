using System;
using System.Collections;
using System.Collections.Specialized;

namespace AA_El_Poeta
//namespace AA_PROJECT3
{
    public class Individual
    {
        //public string[] genes;
        public string[] genes;
        public float fitness{ get; private set; }

        private Func<int, float> fitnessFunction;
        private Random random;
        private Func<string> getRandomGene;

        public Individual(int size, Random random, Func<string> getRandomGene, Func<int, float> fitnessFunction, bool shouldInitGenes = true)
        {
            genes = new string[size];
            this.random = random;
            this.getRandomGene = getRandomGene;
            this.fitnessFunction = fitnessFunction;
            
            if (shouldInitGenes)
            {
                for (int i = 0; i < Poem.targetStringList.Length; i++)
                {
                    genes[i] = getRandomGene();
                }
            }

        }

        public float CalculateFitness(int index)
        {
            fitness = fitnessFunction(index);
            return fitness;
        }

        public Individual Crossover(Individual otherParent)
        {
            Individual child = new Individual(genes.Length, random, getRandomGene, fitnessFunction, shouldInitGenes: false);

            for (int i = 0; i < genes.Length; i++)
            {
                child.genes[i] = random.NextDouble() < 0.5 ? genes[i] : otherParent.genes[i];
            }
            
            
            return child;
        }

        public void Mutate(float mutationRate)
        {
            for (int i = 0; i < genes.Length; i++)
            {
                if (random.NextDouble() < mutationRate)
                {
                    genes[i] = getRandomGene();
                }
            }
        }
    }
}
