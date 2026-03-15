using System;
using System.Collections.Generic;

namespace Lab1
{
    internal class Knapsack
    {
        public List<int> ItemIndexes { get; } = new List<int>();
        public int TotalValue { get; private set; } = 0;
        public int TotalWeight { get; private set; } = 0;

        public void AddItem(int index, int value, int weight)
        {
            ItemIndexes.Add(index);
            TotalValue += value;
            TotalWeight += weight;
        }

        public bool IsEmpty()
        {
            return ItemIndexes.Count == 0;
        }

        public override string ToString()
        {
          string s = "Wynik:\n";
            s += "  Liczba przedmiotów: " + ItemIndexes.Count + "\n";
            if (!IsEmpty())
                s += "  Indeksy przedmiotów: " + string.Join(", ", ItemIndexes) + "\n";
            s += "  Łączna wartość: " + TotalValue + "\n";
            s += "  Łączna waga: " + TotalWeight + "\n";
            return s;
        }
    }
}