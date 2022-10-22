using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public struct Memory
    {
        public Memory(string op, double value)
        {
            Operation = op;
            Value = value;
        }

        public string Operation;
        public double Value;
    }

    public class Calculator
    {
        private JsonWriter _writer;

        //counts how many operations where performed during the program run
        private int _operationCount = 0;

        private Queue<Memory> _memory;
        private const int MEMORY_SIZE = 5;

        public Queue<Memory> Memory { get { return _memory; } }

        public int OperationCount
        {
            get { return _operationCount; }
        }

        public Calculator()
        {
            _memory = new Queue<Memory>();

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
                    LogOperation("{0} + {1} = {2}", "Add", n1, n2, result);
                    break;
                case "s":
                    result = n1 - n2;
                    LogOperation("{0} - {1} = {2}", "Substract", n1, n2, result);
                    break;
                case "m":
                    result = n1 * n2;
                    LogOperation("{0} * {1} = {2}", "Multiply", n1, n2, result);
                    break;
                case "d":
                    if (n2 != 0)
                    {
                        result = n1 / n2;
                        LogOperation("{0} / {1} = {2}", "Divide", n1, n2, result);
                    }
                    break;
                case "p":
                    result = Math.Pow(n1, n2);
                    LogOperation("{0} ^ {1} = {2}", "Power", n1, n2, result);
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
                    LogOperation("Sqrt {0} = {1}", "Square root", num, null, result);
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

        private void LogOperation(string traceFormat, string jsonFormat, double n1, double? n2, double result)
        {
            string op = string.Empty;

            if (n2 != null)
                op = String.Format(traceFormat, n1, n2, result);
            else
                op = String.Format(traceFormat, n1, result);

            Trace.WriteLine(op);
            _writer.WriteValue(jsonFormat);
            _operationCount++;

            UpdateMemory(op, result);
        }

        private void UpdateMemory(string operation, double result)
        {
            if (_memory.Count >= MEMORY_SIZE)
                _memory.Dequeue();

            _memory.Enqueue(new Memory(operation, result));
        }

        public void PrintMemory()
        {
            if (_memory.Count == 0)
                Console.WriteLine("Memory is empty!");

            int memIndex = 0;
            foreach (Memory mem in _memory)
                Console.WriteLine((++memIndex).ToString() + ". " + mem.Operation);
        }

        public void ClearMemory()
        {
            _memory.Clear();
        }
    }
}