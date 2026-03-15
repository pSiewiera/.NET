using System;

namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Podaj liczbę przedmiotów: ");
            int n = int.Parse(Console.ReadLine());

            Console.Write("Podaj seed ");
            string seedInput = Console.ReadLine();
            int seed = -1; 

            if (!string.IsNullOrEmpty(seedInput))
                seed = int.Parse(seedInput);

            Console.Write("Podaj pojemność plecaka: ");
            int capacity = int.Parse(Console.ReadLine());

            Problem problem = new Problem(n, seed);

            Console.WriteLine(problem.ToString());

            Knapsack knapsack = problem.Solve(capacity);

            Console.WriteLine(knapsack.ToString());
  
        }
    }
}