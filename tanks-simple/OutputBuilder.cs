using System;
namespace tanks_simple
{
    public class OutputBuilder
    {
        public string output;
        public OutputBuilder(string liters, Tank[] tanks)
        {
            liters += "l";
            string[] ids = new string[tanks.Length];
            for (int i = 0; i < tanks.Length; i++)
            {
                ids[i] = tanks[i].id + "(" + tanks[i].liters.ToString() + "l)"; // TODO: ~~liters~~ id
                output = String.Join("; ", liters + " remaining", tanks.Length, String.Join(",", ids));
            }
            // TODO: write to file
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "/";
            string fileName = path + "out.txt";
            using StreamWriter file = new(fileName, append: true);
            file.WriteLine(output);
        }
    }
}

