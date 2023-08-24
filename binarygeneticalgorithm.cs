/* 
  MIT License
  Copyright (c) 2023 angzam78 (Angelo Zammit)
*/

using System;
using System.Text;
using System.Collections.Generic;

public class GeneticAlgorithm
{
    private Random random = new Random();

    public string Generate(int length)
    {
        var stringbuilder = new StringBuilder(length);

        while (length-- > 0)
        {
            stringbuilder.Append(random.Next(2));
        }

        return stringbuilder.ToString();
    }

    public string Select(IEnumerable<string> population, IEnumerable<double> fitnesses, double sum = 0)
    {   
        foreach (double fitness in fitnesses) 
        {
            sum += fitness;
        }
        
        double selection = random.NextDouble() * sum;

        var chromosomes = population.GetEnumerator(); 
        var fitnesslist = fitnesses.GetEnumerator();

        double runtotal = 0;

        while (fitnesslist.MoveNext() && chromosomes.MoveNext())
        {
            runtotal += fitnesslist.Current;
            if (runtotal >= selection) break;
        }

        return chromosomes.Current;
    }

    public string Mutate(string chromosome, double probability)
    {
        StringBuilder stringbuilder = new StringBuilder(chromosome.Length);

        foreach (char gene in chromosome)
        {
            if (random.NextDouble() >= probability)
            {
                stringbuilder.Append(gene);
            }
            else
            {
                stringbuilder.Append(gene == '0' ? '1' : '0');
            }
        }

        return stringbuilder.ToString();
    }

    public IEnumerable<string> Crossover(string chromosome1, string chromosome2)
    {
        int cut = random.Next(chromosome1.Length);

        return new [] {chromosome1.Substring(0, cut) + chromosome2.Substring(cut), chromosome2.Substring(0, cut) + chromosome1.Substring(cut)};
    }

    public string Run(Func<string, double> fitness, int length, double p_crossover, double p_mutate, int iterations = 100)
    {
        const int POP_SIZE = 200;

        var currentpopulation = new List<string>();
        var currentfitnesses = new List<double>();

        while(currentpopulation.Count < POP_SIZE)
        {
            string chromosome = Generate(length);
            currentpopulation.Add(chromosome);
            currentfitnesses.Add(fitness(chromosome));
        }

        for (int i = 0 ; i < iterations ; i++ )
        {
            var newpopulation = new List<string>();
            var newfitnesses = new List<double>();

            while(newpopulation.Count < POP_SIZE)
            {
                string chromosome1 = Select(currentpopulation, currentfitnesses);
                string chromosome2 = Select(currentpopulation, currentfitnesses);   

                if (random.NextDouble() >= p_crossover)
                {
                    foreach(string chromosomeX in Crossover(chromosome1, chromosome2))
                    {
                        chromosome1 = chromosome2;
                        chromosome2 = chromosomeX;
                    }
                }
                
                chromosome1 = Mutate(chromosome1, p_mutate);
                chromosome2 = Mutate(chromosome2, p_mutate);

                newpopulation.Add(chromosome1);
                newfitnesses.Add(fitness(chromosome1));

                newpopulation.Add(chromosome2);
                newfitnesses.Add(fitness(chromosome2));
            }

            currentpopulation = newpopulation;
            currentfitnesses = newfitnesses;
        }

        return Select(currentpopulation, currentfitnesses);
    }
}