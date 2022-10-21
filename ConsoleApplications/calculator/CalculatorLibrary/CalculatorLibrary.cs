using System.IO;
using System.Diagnostics;

namespace CalculatorLibrary
{
    public class Calculator
    {
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculator.log");
            Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            Trace.AutoFlush = true;
            Trace.WriteLine("Starting Calculator Log");
            Trace.WriteLine(String.Format("Started {0}", System.DateTime.Now.ToString()));
        }

        public double Compute(double n1, double n2, string operation)
        {
            double result = double.NaN;

            switch (operation)
            {
                case "a":
                    result = n1 + n2;
                    Trace.WriteLine(String.Format("{0} + {1} = {2}", n1, n2, result));
                    break;
                case "s":
                    result = n1 - n2;
                    Trace.WriteLine(String.Format("{0} - {1} = {2}", n1, n2, result));
                    break;
                case "m":
                    result = n1 * n2;
                    Trace.WriteLine(String.Format("{0} * {1} = {2}", n1, n2, result));
                    break;
                case "d":
                    if (n2 != 0)
                    {
                        result = n1 / n2;
                        Trace.WriteLine(String.Format("{0} / {1} = {2}", n1, n2, result));
                    }
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}