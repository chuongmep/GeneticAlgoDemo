// See https://aka.ms/new-console-template for more information

using GeneticAlgoDemo;

int numCities = 5;
int populationSize = 10;
int maxGenerations = 100;
double mutationRate = 0.01;

Random random = new Random();

List<Chromosome> population = new List<Chromosome>();
for (int i = 0; i < populationSize; i++)
{
    List<int> genes = Enumerable.Range(0, numCities).OrderBy(x => random.Next()).ToList();
    population.Add(new Chromosome(genes));
}

for (int generation = 0; generation < maxGenerations; generation++)
{
    population.Sort();
    Console.WriteLine($"Generation {generation}: best fitness = {population[0].Fitness()}, best solution = {population[0]}");

    List<Chromosome> newPopulation = new List<Chromosome>();
    for (int i = 0; i < populationSize; i++)
    {
        int parent1Index = random.Next(populationSize / 2);
        int parent2Index = random.Next(populationSize / 2);
        Chromosome parent1 = population[parent1Index];
        Chromosome parent2 = population[parent2Index];
        Chromosome child = parent1.Crossover(parent2);
        child.Mutate(mutationRate);
        newPopulation.Add(child);
    }

    population = newPopulation;
}