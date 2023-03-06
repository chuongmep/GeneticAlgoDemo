namespace GeneticAlgoDemo;

public class Chromosome : IComparable<Chromosome>
{
    private List<int> genes;

    public Chromosome(List<int> genes)
    {
        this.genes = genes;
    }
    
    public double Fitness()
    {
        double totalDistance = 0;
        for (int i = 0; i < genes.Count - 1; i++)
        {
            totalDistance += DistanceBetween(genes[i], genes[i + 1]);
        }
        totalDistance += DistanceBetween(genes[genes.Count - 1], genes[0]);
        return 1 / totalDistance;
    }

    private double DistanceBetween(int city1, int city2)
    {
        // Here you would calculate the distance between two cities using their latitudes and longitudes, or some other measure.
        // For simplicity, we'll just use a random number between 0 and 1.
        return Math.Sqrt(Math.Pow(city1 - city2, 2));
    }
    
    public Chromosome Crossover(Chromosome other)
    {
        int crossoverPoint = genes.Count / 2;
        List<int> childGenes = new List<int>();
        childGenes.AddRange(genes.GetRange(0, crossoverPoint));
        foreach (int gene in other.genes)
        {
            if (!childGenes.Contains(gene))
            {
                childGenes.Add(gene);
            }
        }
        return new Chromosome(childGenes);
    }
    
    public void Mutate(double mutationRate)
    {
        Random random = new Random();
        for (int i = 0; i < genes.Count; i++)
        {
            if (random.NextDouble() < mutationRate)
            {
                int j = random.Next(genes.Count);
                (genes[i], genes[j]) = (genes[j], genes[i]);
            }
        }
    }
    
    public int CompareTo(Chromosome other)
    {
        return other.Fitness().CompareTo(this.Fitness());
    }

    public override string ToString()
    {
        return "[" + string.Join(", ", genes) + "]";
    }
}