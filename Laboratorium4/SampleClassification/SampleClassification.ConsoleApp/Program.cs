//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using System;
using SampleClassification.Model;

namespace SampleClassification.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create single instance of sample data from first line of dataset for model input
            ModelInput sampleData = new ModelInput()
            {
                Text = @"Last session of the day  http://twitpic.com/67ezh",
                Time_of_Tweet = @"morning",
                Age_of_User = @"0-20",
                Country = @"Afghanistan",
                Population__2020 = 38928344F,
                Land_Area__Km__ = 652860F,
                Density__P_Km__ = 60F,
            };

            // Make a single prediction on the sample data and print results
            var predictionResult = ConsumeModel.Predict(sampleData);

            Console.WriteLine("Using model to make single prediction -- Comparing actual Sentiment with predicted Sentiment from sample data...\n\n");
            Console.WriteLine($"Text: {sampleData.Text}");
            Console.WriteLine($"Time_of_Tweet: {sampleData.Time_of_Tweet}");
            Console.WriteLine($"Age_of_User: {sampleData.Age_of_User}");
            Console.WriteLine($"Country: {sampleData.Country}");
            Console.WriteLine($"Population__2020: {sampleData.Population__2020}");
            Console.WriteLine($"Land_Area__Km__: {sampleData.Land_Area__Km__}");
            Console.WriteLine($"Density__P_Km__: {sampleData.Density__P_Km__}");
            Console.WriteLine($"\n\nPredicted Sentiment value {predictionResult.Prediction} \nPredicted Sentiment scores: [{String.Join(",", predictionResult.Score)}]\n\n");
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }
    }
}
