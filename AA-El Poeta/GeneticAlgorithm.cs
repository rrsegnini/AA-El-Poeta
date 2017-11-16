using System;
using System.Collections.Generic;
//using AA_PROJECT3;
using AA_El_Poeta;

public class GeneticAlgorithm<T>
{
    public List<Individual> Population { get; private set; }
    public int Generation { get; private set; }
    public float BestFitness { get; private set; }
    //public T[] BestGenes { get; private set; }
    public string[] BestGenes { get; private set; }
    
    

    public int Elitism;
    public float MutationRate;

    private List<Individual> newPopulation;
    private Random random;
    private float fitnessSum;
    private int dnaSize;
    private Func<string> getRandomGene;
    private Func<int, float> fitnessFunction;

    public GeneticAlgorithm(int populationSize, int dnaSize, Random random, Func<string> getRandomGene, Func<int, float> fitnessFunction,
        int elitism, float mutationRate = 0.01f)
    {
        Generation = 1;
        Elitism = elitism;
        MutationRate = mutationRate;
        Population = new List<Individual>(populationSize);
        newPopulation = new List<Individual>(populationSize);
        this.random = random;
        this.dnaSize = dnaSize;
        this.getRandomGene = getRandomGene;
        this.fitnessFunction = fitnessFunction;

        //BestGenes = new T[dnaSize];
        BestGenes = new string[dnaSize];

        for (int i = 0; i < populationSize; i++)
        {
            
            Population.Add(new Individual(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
        }
    }

    public void NewGeneration(int numNewDNA = 0, bool crossoverNewDNA = false)
    {
        int finalCount = Population.Count + numNewDNA;

        if (finalCount <= 0)
        {
            return;
        }

        if (Population.Count > 0)
        {
            CalculateFitness();
            Population.Sort(CompareDNA);
        }
        newPopulation.Clear();

        for (int i = 0; i < Population.Count; i++)
        {
            if (i < Elitism && i < Population.Count)
            {
                newPopulation.Add(Population[i]);
            }
            else if (i < Population.Count || crossoverNewDNA)
            {
                Individual parent1 = ChooseParent();
                Individual parent2 = ChooseParent();

                if (parent1 != null)
                {
                    Individual child = parent1.Crossover(parent2);
                    child.Mutate(MutationRate);

                    newPopulation.Add(child);
                }
                
            }
            else
            {
                newPopulation.Add(new Individual(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
            }
        }

        List<Individual> tmpList = Population;
        Population = newPopulation;
        newPopulation = tmpList;

        Generation++;
    }

    private int CompareDNA(Individual a,Individual b)
    {
        if (a.fitness > b.fitness)
        {
            return -1;
        }
        else if (a.fitness < b.fitness)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private void CalculateFitness()
    {
        fitnessSum = 0;
        Individual best = Population[0];

        for (int i = 0; i < Population.Count; i++)
        {
            fitnessSum += Population[i].CalculateFitness(i);

            if (Population[i].fitness > best.fitness)
            {
                best = Population[i];
            }
        }

        BestFitness = best.fitness;
        //Console.WriteLine(best.genes);
        
        best.genes.CopyTo(BestGenes, 0);
    }

    private Individual ChooseParent()
    {
        double randomNumber = random.NextDouble() * fitnessSum;

        for (int i = 0; i < Population.Count; i++)
        {
            if (randomNumber < Population[i].fitness)
            {
                return Population[i];
            }

            randomNumber -= Population[i].fitness;
        }

        return null;
    }
}