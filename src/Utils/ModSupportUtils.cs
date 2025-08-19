using Terraria.ModLoader;

namespace DarkSouls.Utils
{
    public class ModSupportUtils
    {
        public static bool GetCalamityBossDowned(string bossName)
        {
            if (ModLoader.TryGetMod("CalamityMod", out var calamityMod))
                return (bool)calamityMod.Call("GetBossDowned", bossName);
            ConsoleUtils.Error("ModSupportUtils", $"GetCalamityBossDowned - CalamityMod is not enabled!");
            return false;
        }
    }
}