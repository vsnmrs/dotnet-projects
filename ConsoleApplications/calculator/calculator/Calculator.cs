namespace calculator
{
    internal class Calculator
    {
        public static double Calculate(double n1, double n2, string operation)
        {
            double result = double.NaN;

            switch (operation)
            {
                case "a":
                    result = n1 + n2;
                    break;
                case "s":
                    result = n1 - n2;
                    break;
                case "m":
                    result = n1 * n2;
                    break;
                case "d":
                    if (n2 != 0)
                        result = n1 / n2;
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
