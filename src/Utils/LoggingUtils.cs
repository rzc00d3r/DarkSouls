using log4net;
using Terraria.ModLoader;

namespace DarkSouls.Utils
{
    public static class LoggingUtils
    {
        private static ILog Logger => ModContent.GetInstance<DarkSouls>().Logger;

        public static void Info(string subsectionName, object message)
        {
            string formattedMessage = $"[{subsectionName}] {message}";
            Logger.Info(formattedMessage);
        }

        public static void Error(string subsectionName, object message)
        {
            string formattedMessage = $"[{subsectionName}] {message}";
            Logger.Error(formattedMessage);
        }
    }
}