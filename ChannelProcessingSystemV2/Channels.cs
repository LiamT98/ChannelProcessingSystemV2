using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace ChannelProcessingSystemV2
{
    static class Channels
    {
        public static List<Channel> stored_channels = new List<Channel>();

        public static void LoadChannel(string path)
        {
            StreamReader channel_file = new StreamReader(path);

            string channel_string = channel_file.ReadToEnd();
            channel_string = channel_string.Replace(" ","");

            string[] channel_arr = channel_string.Split(',');

            if (!ValidateChannel(channel_arr))
                return;

            string[] val_string_array = new string[channel_arr.Length - 1];

            Array.Copy(channel_arr, 1, val_string_array, 0, channel_arr.Length - 1);

            double[] val_dbl_array = Array.ConvertAll(val_string_array, Double.Parse);

            stored_channels.Add(new Channel(channel_arr[0], val_dbl_array));

            Console.WriteLine($"{channel_arr[0]} Channel Loaded\nPress any key to continue...");
            Console.ReadKey();
        }

        private static bool ValidateChannel(string[] channel)
        {
            if (!char.IsLetter(channel[0][0]))
                return false;
            if (channel[0].Length > 1)
                return false;
            if (!char.IsUpper(channel[0][0]))
                return false;

            for (int i = 1; i < channel.Length; i++)
                if (!double.TryParse(channel[i], out double result))
                    return false;

            return true;
        }
    }
    class Channel
    {
        public string channel_name { get; set; }
        public double[] channel_values { get; set; }

        public Channel(string name, double[] vals)
        {
            channel_name = name;
            channel_values = vals;
        }
    }
}
