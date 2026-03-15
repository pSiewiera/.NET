using System;

namespace Lab1
{
    internal class Item
    {
        public int Weight { get; }
        public int Value { get; }
        public int Index { get; }

        public Item(int index, int value, int weight)
        {
            Index = index;
            Value = value;
            Weight = weight;
        }

        public double ValuePerWeight()
        {
            return (double)Value / Weight;
        }

        public override string ToString()
        {
            return "Przedmiot " + Index + " - Wartość: " + Value + ", Waga: " + Weight;
        }
    }
}