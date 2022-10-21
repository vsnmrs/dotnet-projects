﻿using System.IO;
using System.Diagnostics;

using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter _writer;
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
                    break;
                case "s":
                    result = n1 - n2;
                    Trace.WriteLine(String.Format("{0} - {1} = {2}", n1, n2, result));
                    _writer.WriteValue("Substract");
                    break;
                case "m":
                    result = n1 * n2;
                    Trace.WriteLine(String.Format("{0} * {1} = {2}", n1, n2, result));
                    _writer.WriteValue("Multiply");
                    break;
                case "d":
                    if (n2 != 0)
                    {
                        result = n1 / n2;
                        Trace.WriteLine(String.Format("{0} / {1} = {2}", n1, n2, result));
                        _writer.WriteValue("Divide");
                    }
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