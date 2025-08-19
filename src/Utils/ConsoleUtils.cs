using System;
using Terraria;

namespace DarkSouls.Utils
{
    public static class ConsoleUtils
    {
        public static void WriteLine(object obj, ConsoleColor color)
        {
            if (!Main.dedServ)
            {
                ConsoleColor fgColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(obj);
                Console.ForegroundColor = fgColor;
            }
        }

        public static void Info(string subsectionName, object obj)
        {
            string time = DateTime.Now.ToString("HH:mm:ss:fff");
            obj = $"[{time}] [DarkSouls/INFO] [{subsectionName}]: {obj}";
            WriteLine(obj, ConsoleColor.Cyan);
        }

        public static void Error(string subsectionName, object obj)
        {
            string time = DateTime.Now.ToString("HH:mm:ss:fff");
            obj = $"[{time}] [DarkSouls/ERROR] [{subsectionName}]: {obj}";
            WriteLine(obj, ConsoleColor.DarkRed);
        }
    }
}
