using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using ZetaLongPaths;

namespace ChannelProcessingSystemV2
{
    class Program
    {
        static int poll_rate = 100;
        // B values
        static List<Double> B_vals = new List<double>();
        // Mean of B
        static double b;

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!\n\nPress any key to continue...");

            Console.ReadKey();

            LoadParameters();
            LoadChannel();

            foreach (double X in Channels.stored_channels.First(x => x.channel_name == "X").channel_values)
            {
                Console.WriteLine();
                //Print X
                Console.WriteLine($"X: {X}");
                // Print B
                Console.WriteLine($"B: {Function2(Function3(X), Function1(X))}");
                // Print C
                Console.WriteLine($"C: {Function4(X)}");
                // Print b (in progress)
                Console.WriteLine($"b: {b}");

                Thread.Sleep(poll_rate / 3);
                Console.Write(".");
                Thread.Sleep(poll_rate / 3);
                Console.Write(".");
                Thread.Sleep(poll_rate / 3);
                Console.Write(".");
                Thread.Sleep(poll_rate / 3);
            }

            Console.WriteLine();
            // Print b
            Console.WriteLine($"Final b: {b}");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void LoadParameters()
        {
            Console.WriteLine("Load Parameter Set");
            Console.WriteLine();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Load a parameter set";
            ofd.InitialDirectory = "c://";
            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
                Parameters.LoadParameters(ofd.FileName);
        }

        static void LoadChannel()
        {
            Console.WriteLine("Load Channel");
            Console.WriteLine();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Load a channel";
            ofd.InitialDirectory = "c://";
            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
                Channels.LoadChannel(ofd.FileName);
        }

        static double Function1(double X)
        {
            // Define return value
            double Y;

            // Get stored parameters
            Parameter m = Parameters.stored_parameters.First(x => x.para_denotation == "m");
            Parameter c = Parameters.stored_parameters.First(x => x.para_denotation == "c");

            // Intergrate components
            Y = (m.para_value * X) + c.para_value;

            Console.WriteLine($"Y: {Y}");

            return Y;
        }

        static double Function2(double A, double Y)
        {
            // Define return values
            double B;

            B = A + Y;

            B_vals.Add(B);

            b = B_vals.Average();

            return B;
        }

        static double Function3(double X)
        {
            double A;

            A = 1 / X;

            Console.WriteLine($"A: {A}");

            return A;
        }

        static double Function4(double X)
        {
            double C;

            C = X + b;

            return C;
        }

        static void PrintBanner()
        {
            ConsoleHelper.SetCurrentFont("Consolas", 1);

            Console.WriteLine(Properties.Resources.banner);
        }
    }
}
