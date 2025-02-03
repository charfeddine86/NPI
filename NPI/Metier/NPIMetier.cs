using NPI.Resources;
using System.Reflection;

namespace NPI.Metier
{
    public class NPIMetier
    {
        public double Calculer(string expression)
        {

            expression = expression.Trim();

            if (String.IsNullOrEmpty(expression))
                throw new Exception(Resource.OperationInvalide);

            var operation = expression.Split(' ').ToList();

            if (operation.Count == 0)
                throw new Exception(Resource.OperationInvalide);

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
                  throw new Exception($" {Resource.ValeurInconnu} : {val}");
            }

            if (pile.Count != 1)
                throw new InvalidOperationException(Resource.OperationInvalide);

            double resultat = pile.Pop();
            if (resultat < Int64.MinValue || resultat > Int64.MaxValue)
             throw new Exception(Resource.ValeurIncorecte);
            

            return resultat;
        }

        private void Operation(Stack<double> pile, string val)
        {
            if (pile.Count < 2)
                throw new InvalidOperationException(Resource.PasAssezOperandes);

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
                        throw new DivideByZeroException(Resource.DivisionParZero);
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
