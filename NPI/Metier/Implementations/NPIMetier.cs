using NPI.Metier.Interfaces;
using NPI.Resources;
using System.Reflection;

namespace NPI.Metier.Implementations
{
    public class NPIMetier : INPIMetier
    {
        public double Calculer(string expression)
        {

            expression = expression.Trim();

            if (string.IsNullOrEmpty(expression))
                throw new Exception("Operation invalide.");

            var operation = expression.Split(' ').ToList();

            if (operation.Count == 0)
                throw new Exception("Operation invalide.");

            if (operation.Count == 1 && double.TryParse(operation[0], out double number))
            {
                return number;
            }

            Stack<double> pile = new Stack<double>();
            foreach (string val in operation)
            {
                if (double.TryParse(val, out number))
                    pile.Push(number);
                else if (val.Trim() == "+" || val.Trim() == "-" ||
                         val.Trim() == "/" || val.Trim() == "%" ||
                         val.Trim() == "*" || val.Trim() == "^")
                    Operation(pile, val);
                else
                    throw new Exception($" {"Valeur Inconnu"} : {val}");
            }

            if (pile.Count != 1)
                throw new InvalidOperationException("Operation invalide.");

            double resultat = pile.Pop();
            if (resultat < long.MinValue || resultat > long.MaxValue)
                throw new Exception("Valeur Incorecte");


            return resultat;
        }

        private void Operation(Stack<double> pile, string val)
        {
            if (pile.Count < 2)
                throw new InvalidOperationException("Pas Assez de operandes");

            double b = pile.Pop();
            double a = pile.Pop();

            switch (val.Trim())
            {
                case "+":
                    pile.Push(a + b);
                    break;
                case "-":
                    pile.Push(a - b);
                    break;
                case "*":
                    pile.Push(a * b);
                    break;
                case "/":
                    if (b == 0)
                        throw new DivideByZeroException("Division par Zero");
                    pile.Push(a / b);
                    break;
                case "^":
                    pile.Push(Math.Pow(a, b));
                    break;
                case "%":
                    pile.Push(a % b);
                    break;
            }
        }
    }
}
