using System.Data;

namespace scientific_calculator
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }
        private void OnButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;

            // Handle different button clicks
            switch (buttonText)
            {
                case "AC": // Clear button
                    DisplayLabel.Text = "0";
                    break;
                case "⌫": // Backspace button
                    if (DisplayLabel.Text.Length > 0)
                        DisplayLabel.Text = DisplayLabel.Text.Substring(0, DisplayLabel.Text.Length - 1);
                    break;
                case "=": // Equals button - Perform calculation
                    PerformCalculation();
                    break;
                default: // Other buttons
                    // Append the button text to the label text
                    if (DisplayLabel.Text == "0")
                        DisplayLabel.Text = buttonText;
                    else
                        DisplayLabel.Text += buttonText;
                    break;
            }
        }

        private void PerformCalculation()
        {
            try
            {
                // Replace '×' with '*', '÷' with '/', and '=' with '' for calculation
                string expression = DisplayLabel.Text.Replace("×", "*").Replace("÷", "/").Replace("=", "");

                // Find and process the percentage expressions
                string[] parts = expression.Split('+', '-', '*', '/');
                for (int i = 0; i < parts.Length; i++)
                {
                    if (parts[i].Contains("%"))
                    {
                        double value = double.Parse(parts[i].Replace("%", "")) / 100.0;
                        expression = expression.Replace(parts[i], value.ToString());
                    }
                }

                // Evaluate the expression using DataTable.Compute
                DataTable dataTable = new DataTable();
                var result = dataTable.Compute(expression, "");

                // Update the display label with the result
                DisplayLabel.Text = result.ToString();
            }
            catch (SyntaxErrorException)
            {
                // Handle syntax errors in the expression by displaying an error message
                DisplayLabel.Text = "Syntax Error";
            }
            catch (Exception)
            {
                // Handle other exceptions by displaying an error message
                DisplayLabel.Text = "Calculation Error";
            }
        }




        // Additional functions for scientific calculator
        private void SquareRoot()
        {
            try
            {
                double number = double.Parse(DisplayLabel.Text);
                double result = Math.Sqrt(number);
                DisplayLabel.Text = result.ToString();
            }
            catch (Exception)
            {
                DisplayLabel.Text = "Error";
            }
        }
    }


        // Event handler for equals button (=)
        //private void OnEqualsClicked(object sender, EventArgs e) 
        //    {
        //        // Perform calculation or any other action
        //        // For simplicity, let's clear the label text
        //        DisplayLabel.Text = "";
        //    }
        //}


    }
