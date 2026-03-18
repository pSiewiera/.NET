using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Lab1Tests")]
[assembly: InternalsVisibleTo("Lab1GUI")]
namespace Lab1
{
    internal class Problem
    {
        public int NumberOfItems { get; }
         List<Item> Items { get; } = new List<Item>();

        public Problem(int numberOfItems, int seed = -1)
        {
            NumberOfItems = numberOfItems;
            Random rnd;

            if (seed >= 0)
                rnd = new Random(seed); 
            else
                rnd = new Random(); 

            for (int i = 1; i <= numberOfItems; i++)
            {
                int value = rnd.Next(1, 11);   
                int weight = rnd.Next(1, 11);  
                Items.Add(new Item(i, value, weight));
            }
        }

        public Knapsack Solve(int capacity)
        {
            Knapsack knapsack = new Knapsack();
            bool[] taken = new bool[NumberOfItems]; 

            int remaining = capacity;

            while (true)
            {
                int bestIndex = -1;
                double bestRatio = -1;

                for (int i = 0; i < NumberOfItems; i++)
                {
                    if (!taken[i] && Items[i].Weight <= remaining)
                    {
                        double ratio = Items[i].ValuePerWeight();
                        if (ratio > bestRatio)
                        {
                            bestRatio = ratio;
                            bestIndex = i;
                        }
                    }
                }

                if (bestIndex == -1) break; 

                taken[bestIndex] = true;
                Item item = Items[bestIndex];
                knapsack.AddItem(item.Index, item.Value, item.Weight);
                remaining -= item.Weight;
            }

            return knapsack;
        }

        public override string ToString()
        {
            string s = $" liczba przedmiotów = {NumberOfItems}\r\nLista przedmiotów:\r\n";
            foreach (var item in Items)
            {
                s += "  " + item.ToString() + "\r\n";
            }
            return s;
        }
    }
}