using System.IO;
using System.Diagnostics;

using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        private JsonWriter _writer;

        //counts how many operations where performed during the program run
        private int _operationCount = 0;

        public int OperationCount
        {
            get { return _operationCount; }
        }

        public Calculator()
        {
            //log into a file with the Trace class
            StreamWriter logFile = File.CreateText("calculator.log");
            Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            Trace.AutoFlush = true;
            Trace.WriteLine("Starting Calculator Log");
            Trace.WriteLine(String.Format("Started {0}", System.DateTime.Now.ToString()));

            //log into a json file
            StreamWriter jsonLog = File.CreateText("calculator.json");
            jsonLog.AutoFlush = true;
            _writer = new JsonTextWriter(jsonLog);
            _writer.Formatting = Formatting.Indented;
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operation");
            _writer.WriteStartArray();
        }

        public double Compute(double n1, double n2, string operation)
        {
            double result = double.NaN;

            _writer.WriteStartObject();
            _writer.WritePropertyName("Operand 1");
            _writer.WriteValue(n1);
            _writer.WritePropertyName("Operand 2");
            _writer.WriteValue(n2);
            _writer.WritePropertyName("Operation");

            switch (operation)
            {
                case "a":
                    result = n1 + n2;
                    Trace.WriteLine(String.Format("{0} + {1} = {2}", n1, n2, result));
                    _writer.WriteValue("Add");
                    _operationCount++;
                    break;
                case "s":
                    result = n1 - n2;
                    Trace.WriteLine(String.Format("{0} - {1} = {2}", n1, n2, result));
                    _writer.WriteValue("Substract");
                    _operationCount++;
                    break;
                case "m":
                    result = n1 * n2;
                    Trace.WriteLine(String.Format("{0} * {1} = {2}", n1, n2, result));
                    _writer.WriteValue("Multiply");
                    _operationCount++;
                    break;
                case "d":
                    if (n2 != 0)
                    {
                        result = n1 / n2;
                        Trace.WriteLine(String.Format("{0} / {1} = {2}", n1, n2, result));
                        _writer.WriteValue("Divide");
                        _operationCount++;
                    }
                    break;
                case "p":
                    result = Math.Pow(n1, n2);
                    Trace.WriteLine(String.Format("{0} ^ {1} = {2}", n1, n2, result));
                    _writer.WriteValue("Power");
                    _operationCount++;
                    break;
                default:
                    break;
            }

            _writer.WritePropertyName("Result");
            _writer.WriteValue(result);
            _writer.WriteEndObject();

            return result;
        }

        public double Compute(double num, string operation)
        {
            double result = double.NaN;

            _writer.WriteStartObject();
            _writer.WritePropertyName("Operand 1");
            _writer.WriteValue(num);
            _writer.WritePropertyName("Operation");

            switch (operation)
            {
                case "r":
                    result = Math.Sqrt(num);
                    Trace.WriteLine(String.Format("Sqrt {0} = {1}", num, result));
                    _writer.WriteValue("Square root");
                    _operationCount++;
                    break;
                default:
                    break;
            }

            _writer.WritePropertyName("Result");
            _writer.WriteValue(result);
            _writer.WriteEndObject();

            return result;
        }

        public void Finish()
        {
            _writer.WriteEndArray();
            _writer.WriteEndObject();
            _writer.Close();
        }
    }
}