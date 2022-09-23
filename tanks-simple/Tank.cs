using System;
namespace tanks_simple
{
    public class Tank
    {
        public string id;
        public int liters = 1;
        public Tank(string settingsStr)
        {
            string[] settings = settingsStr.Split(";");
            id = settings[0];
            string[] sizesStr = settings[1].Split(",");

            for (int i = 0; i < sizesStr.Length; i++)
            {
                liters = liters * int.Parse(sizesStr[i]);
            }
        }
    }
}

