using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BeeSpace
{
    public class Command : ICommands
    {
        Collections collection = new();
        GreedyAlgorithm greedy = new();
        BeeAlgorithm bee = new(100, 10, 0.5, 0.3, 3);
        private int functionValue;

        public void Exit()
        {
            Environment.Exit(0);
        }

        public void Command1()
        {
            List<Shelf> bestSolution = greedy.Optimize(collection.Items, collection.Shelves);
            bestSolution = greedy.Optimize(collection.Items, collection.Shelves);
            Console.WriteLine("Best solution found by greedy algorithm:");
            PrintSolution(bestSolution);
        }
        public void Command2()
        {
            List<Shelf> bestSolution = bee.Optimize(collection.Items, collection.Shelves);
            Console.WriteLine("Best solution found by bee algorithm:");
            PrintSolution(bestSolution);
            
        }
        public void Command3()
        {
            collection.PrintCollections();
        }
        public void Command4()
        {
            collection.GenerateRandomCollections(0, 0);
        }
        public void Command5()
        {
            collection.FillCollectionsFromConsole();
        }
        public void Command6()
        {
            string input = Console.ReadLine();
            collection.FillCollectionsFromFile(input);
        }

        public void Command7()
        {
            int researchCount = 1;
            List<double> researchSolutions = new();

            for (int i = 0; i < researchCount; i++)
            {
                List<int> iterationNumber = new List<int> { 1, 5, 10, 25, 50, 100, 200, 300, 500, 1000 };
                List<int> solutions = new();
                collection.GenerateRandomCollections(3, 0);


                foreach (int iterator in iterationNumber)
                {
                    BeeAlgorithm beeAlgorithm = new(iterator, 10, 0.01, 0.01, 3);
                    List<Shelf> bestSolution = beeAlgorithm.Optimize(collection.Items, collection.Shelves);
                    Console.WriteLine("Best solution found by bee algorithm:");

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"For {iterator} itarations");
                    Console.ResetColor();
                    PrintSolution(bestSolution);
                    solutions.Add(functionValue);
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Average target function value: {solutions.Average()}");
                Console.ResetColor();
                researchSolutions.Add(solutions.Average());
            }

            int index = 1;
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var solution in researchSolutions)
            {
                Console.WriteLine($"Research {index}: {solution}  ");
                index++;
            }
            Console.ResetColor();
        }

        public void Command8()
        {
            int researchCount = 1;
            List<double> researchSolutions = new();

            for (int i = 0; i < researchCount; i++)
            {
                //List<int> beesNumber = new List<int> { 1, 5, 10, 25, 50, 100, 200, 300, 500, 1000 };
                List<int> beesNumber = new List<int> { 1, 100, 1000 };
                List<int> solutions = new();
                collection.GenerateRandomCollections(3, 0);


                foreach (int iterator in beesNumber)
                {
                    

                    BeeAlgorithm beeAlgorithm = new(100, iterator, 0.1, 0.1, 3);
                    List<Shelf> bestSolution = beeAlgorithm.Optimize(collection.Items, collection.Shelves);
                    Console.WriteLine("Best solution found by bee algorithm:");

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"For {iterator} bees");
                    Console.ResetColor();
                    PrintSolution(bestSolution);
                    solutions.Add(functionValue);
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Average target function value: {solutions.Average()}");
                Console.ResetColor();
                researchSolutions.Add(solutions.Average());
            }

            int index = 1;
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var solution in researchSolutions)
            {
                Console.WriteLine($"Research {index}: {solution}  ");
                index++;
            }
            Console.ResetColor();
        }

        public void Command9()
        {
            Stopwatch stopwatch = new Stopwatch();
            
            stopwatch.Start();
            Command1();
            stopwatch.Stop();
            Console.WriteLine("Time taken by Greedy Algorithm: " + stopwatch.Elapsed);

            stopwatch.Reset();

            stopwatch.Start();
            Command2();
            stopwatch.Stop();
            Console.WriteLine("Time taken by Bee Algorithm: " + stopwatch.Elapsed);

        }

        private void PrintSolution(List<Shelf> bestSolution)
        {
            functionValue = 0;
            foreach (var shelf in bestSolution)
            {
                Console.WriteLine($"Shelf (MaxWeight: {shelf.MaxWeight}, MaxVolume: {shelf.MaxVolume})");
                foreach (var item in shelf.Items)
                {
                    functionValue += item.Value;
                    Console.WriteLine($"  Item (Weight: {item.Weight}, Volume: {item.Volume}, Value: {item.Value})");
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"The resulting solution: {functionValue}");
            Console.ResetColor();
        }
    }
   

}
