namespace ConsoleApp1
{
    internal class Program
    {        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            FizzBuzz fb = new FizzBuzz (40);
            fb.Print();
        }
    }
     internal class FizzBuzz
    {
        private int maxNumber;
        public FizzBuzz(int maxNumber)
        {
            this.maxNumber = maxNumber;
        }
        public void Print()
        {
            for (int i=1; i <=maxNumber; i++)
            {
                if(i % 3 == 0 && i % 5 == 0)
                {
                    Console.WriteLine("FizzBuzz");
                }
                
                else if (i % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                }
                else if (i % 3 ==0) 
                {
                    Console.WriteLine("Fizz");
                }
                else
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}