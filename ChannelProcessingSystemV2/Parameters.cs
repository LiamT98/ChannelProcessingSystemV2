using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChannelProcessingSystemV2
{
    static class Parameters
    {
        public static List<Parameter> stored_parameters = new List<Parameter>();



        public static void LoadParameters(string path)
        {
            string line;

            StreamReader file = new StreamReader(path);

            while (!string.IsNullOrEmpty(line = file.ReadLine()))
            {
                string[] p_line = line.Split(',');

                if (!ValidateParameter(p_line))
                    break;

                string p_deno = p_line[0];
                string p_val = p_line[1].TrimStart(' ');

                stored_parameters.Add(new Parameter(p_deno, p_val));

                Console.WriteLine($"Parameter {p_deno} Loaded");
            }
        }

        private static bool ValidateParameter(string[] line)
        {
            char denotation = char.Parse(line[0]);
            string value = line[1].TrimStart(' ');

            // Validate the parameter denotation
            if (!char.IsLetter(denotation))
                return false;
            //if (denotation.Length > 1)
            //    return false;
            if (char.IsUpper(denotation))
                return false;

            // Validate the value
            if (!double.TryParse(value, out double result))
                return false;

            return true;
        }

        public static void AddParameter()
        {

        }
    }
    public class Parameter
    {
        public string para_denotation { get; set; }
        public double para_value { get; set; }

        public Parameter(string deno, string val)
        {
            para_denotation = deno;
            para_value = double.Parse(val);
        }
    }
}
