using System;
using System.Collections.Generic;
using System.Linq;

public class BeeAlgorithm
{
    private readonly int _maxIterations;
    private readonly int _numberOfWorkers;
    private readonly double _personalInfluence;
    private readonly double _globalInfluence;
    private readonly int _numberOfEliteSites;

    public BeeAlgorithm(int maxIterations, int numberOfWorkers, double personalInfluence, double globalInfluence, int numberOfEliteSites)
    {
        _maxIterations = maxIterations;
        _numberOfWorkers = numberOfWorkers;
        _personalInfluence = personalInfluence;
        _globalInfluence = globalInfluence;
        _numberOfEliteSites = numberOfEliteSites;
    }

    public List<Shelf> Optimize(List<Item> items, List<Shelf> shelves)
    {
        List<List<Shelf>> solutions = InitializePopulation(items, shelves);
        Random random = new Random();

        for (int i = 0; i < _maxIterations; i++)
        {
            var bestSites = solutions.OrderByDescending(s => CalculateTotalValue(s)).Take(_numberOfEliteSites).ToList();
            List<List<Shelf>> newSolutions = new List<List<Shelf>>();

            foreach (var site in bestSites)
            {
                var newSolution = PerformLocalSearch(site, items, random);
                newSolutions.Add(newSolution);
            }

            solutions.AddRange(newSolutions);
            solutions.AddRange(InitializePopulation(items, shelves));
            solutions = solutions.OrderByDescending(s => CalculateTotalValue(s)).Take(_numberOfWorkers).ToList();
        }

        return solutions.OrderByDescending(s => CalculateTotalValue(s)).First();
    }

    private List<Shelf> PerformLocalSearch(List<Shelf> currentSolution, List<Item> items, Random random)
    {
        var newSolution = CloneSolution(currentSolution);

        foreach (var item in items)
        {
            if (newSolution.Any(s => s.Items.Contains(item)))
            {
                continue;
            }

            double bestUtility = double.MinValue;
            Shelf bestShelf = null;

            foreach (var shelf in newSolution)
            {
                if (shelf.CurrentWeight + item.Weight > shelf.MaxWeight || shelf.CurrentVolume + item.Volume > shelf.MaxVolume)
                {
                    continue;
                }

                double usefulness = CalculateItemUtility(item, currentSolution, random);

                if (usefulness > bestUtility)
                {
                    bestUtility = usefulness;
                    bestShelf = shelf;
                }
            }

            if (bestShelf != null)
            {
                bestShelf.Items.Add(item);
            }
        }

        return newSolution;
    }

    private double CalculateItemUtility(Item item, List<Shelf> currentSolution, Random random)
    {
        double baseUtility = item.Value / (Math.Pow(item.Weight, 2) + Math.Pow(item.Volume, 2));
        double personalFactor = random.NextDouble() * _personalInfluence * (currentSolution.Any(s => s.Items.Contains(item)) ? 1 : 0);
        double globalFactor = random.NextDouble() * _globalInfluence * currentSolution.Count(s => s.Items.Contains(item));

        return baseUtility + personalFactor + globalFactor;
    }

    private Shelf FindBestShelfForItem(List<Shelf> shelves, Item item)
    {
        return shelves
            .Where(s => s.CurrentWeight + item.Weight <= s.MaxWeight && s.CurrentVolume + item.Volume <= s.MaxVolume)
            .OrderByDescending(s => (s.MaxVolume - s.CurrentVolume) + (s.MaxWeight - s.CurrentWeight))
            .FirstOrDefault();
    }

    private List<List<Shelf>> InitializePopulation(List<Item> items, List<Shelf> shelves)
    {
        Random random = new Random();
        List<List<Shelf>> population = new List<List<Shelf>>();

        for (int i = 0; i < _numberOfWorkers; i++)
        {
            var newShelves = CloneShelves(shelves);

            foreach (var item in items.OrderBy(x => random.Next()))
            {
                var shelf = FindBestShelfForItem(newShelves, item);

                if (shelf != null)
                {
                    shelf.Items.Add(item);
                }
            }

            population.Add(newShelves);
        }

        return population;
    }

    private List<Shelf> CloneSolution(List<Shelf> original)
    {
        return original.Select(s => new Shelf { MaxWeight = s.MaxWeight, MaxVolume = s.MaxVolume, Items = new List<Item>(s.Items) }).ToList();
    }

    private List<Shelf> CloneShelves(List<Shelf> shelves)
    {
        return shelves.Select(s => new Shelf { MaxWeight = s.MaxWeight, MaxVolume = s.MaxVolume, Items = new List<Item>() }).ToList();
    }

    private int CalculateTotalValue(List<Shelf> shelves)
    {
        return shelves.Sum(s => s.CurrentValue);
    }
}