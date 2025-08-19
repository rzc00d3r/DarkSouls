using Terraria.ID;
using Terraria.ModLoader;

namespace DarkSouls.Utils
{
    sealed class CalamityNPCID
    {
        public static int Oarfish { get; private set; }
        public static int MirageJelly { get; private set; }
        public static int ColossalSquid { get; private set; }
        public static int Eidolist { get; private set; }
        public static int GulperEel { get; private set; }
        public static int Bloatfish { get; private set; }
        public static int BobbitWorm { get; private set; }
        public static int EidolonWyrm { get; private set; }
        public static int ReaperShark { get; private set; }

        public static int THELORDE { get; private set; }

        public static void Initialize()
        {
            if (!DarkSouls.CalamityModIsEnabled)
                return;

            ConsoleUtils.Info("CalamityNPCID", "Initialize");

            Oarfish = GetCalamityNPCIDByName("OarfishHead");
            MirageJelly = GetCalamityNPCIDByName("MirageJelly");
            ColossalSquid = GetCalamityNPCIDByName("ColossalSquid");
            Eidolist = GetCalamityNPCIDByName("Eidolist");
            GulperEel = GetCalamityNPCIDByName("GulperEelHead");
            Bloatfish = GetCalamityNPCIDByName("Bloatfish");
            BobbitWorm = GetCalamityNPCIDByName("BobbitWormHead");
            EidolonWyrm = GetCalamityNPCIDByName("EidolonWyrmHead");
            ReaperShark = GetCalamityNPCIDByName("ReaperShark");

            THELORDE = GetCalamityNPCIDByName("THELORDE");
        }   

        public static int GetCalamityNPCIDByName(string npcName)
        {
            ModNPC modNPC;
            if (ModContent.TryFind("CalamityMod", npcName, out modNPC))
                return modNPC.Type;
            ConsoleUtils.Error("CalamityNPCID", $"Not found Calamity NPC by name ({npcName})");
            return NPCID.None;
        }

        public static ModNPC GetCalamityNPCByName(string npcName)
        {
            ModNPC modNPC;
            if (ModContent.TryFind("CalamityMod", npcName, out modNPC))
                return modNPC;
            ConsoleUtils.Error("CalamityNPCID", $"Not found Calamity NPC by name ({npcName})");
            return null;
        }
    }
}
