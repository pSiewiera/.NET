using System;
using System.Windows.Forms;
using Lab1;

namespace Lab1GUI
{
    public partial class Form1 : Form
    {
        TextBox textBoxN = new TextBox();
        TextBox textBoxSeed = new TextBox();
        TextBox textBoxCapacity = new TextBox();
        TextBox textBoxResult = new TextBox();
        Button buttonSolve = new Button();

        public Form1()
        {
            this.Text = "Knapsack";
            this.Width = 500;
            this.Height = 400;

            textBoxN.Top = 20; 
            textBoxN.Left = 20; 
            textBoxN.Width = 100; 
            textBoxN.PlaceholderText = "Items";

            textBoxSeed.Top = 50; 
            textBoxSeed.Left = 20;
            textBoxSeed.Width = 100; 
            textBoxSeed.PlaceholderText = "Seed";

            textBoxCapacity.Top = 80;
            textBoxCapacity.Left = 20; 
            textBoxCapacity.Width = 100; 
            textBoxCapacity.PlaceholderText = "Capacity";

            buttonSolve.Top = 120; 
            buttonSolve.Left = 20; 
            buttonSolve.Text = "Solve";
            buttonSolve.Click += ButtonSolve_Click;

            textBoxResult.Top = 160; 
            textBoxResult.Left = 20; 
            textBoxResult.Width = 440; 
            textBoxResult.Height = 150;
            textBoxResult.Multiline = true;
            textBoxResult.ReadOnly = true;

            this.Controls.Add(textBoxN);
            this.Controls.Add(textBoxSeed);
            this.Controls.Add(textBoxCapacity);
            this.Controls.Add(buttonSolve);
            this.Controls.Add(textBoxResult);
        }

        private void ButtonSolve_Click(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(textBoxN.Text);
                int capacity = int.Parse(textBoxCapacity.Text);
                int seed = 0;
                if (!string.IsNullOrEmpty(textBoxSeed.Text))
                    seed = int.Parse(textBoxSeed.Text);

                Problem problem = new Problem(n, seed);
                Knapsack r = problem.Solve(capacity);

                textBoxResult.Text = problem.ToString() + "\n" + r.ToString();

            }
            catch
            {
                MessageBox.Show("Jest błąd z liczbami");
            }
        }
    }
}