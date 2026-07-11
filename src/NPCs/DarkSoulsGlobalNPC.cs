using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.Chat;
using Terraria.ModLoader;
using Terraria.Localization;

namespace DarkSouls.NPCs
{
    public class DarkSoulsGlobalNPC : GlobalNPC
    {
        public override bool PreKill(NPC npc)
        {
            if (npc.type == NPCID.WallofFlesh && !Main.hardMode)
                SendWarpMessage("Mods.DarkSouls.Messages.LordvesselUnlocked");
            else if (npc.type == NPCID.MoonLordCore && !NPC.downedMoonlord)
                SendWarpMessage("Mods.DarkSouls.Messages.FreeWarpUnlocked");

            return base.PreKill(npc);
        }

        private void SendWarpMessage(string localizationKey)
        {
            Color textColor = new Color(255, 140, 40);

            if (Main.netMode == NetmodeID.SinglePlayer)
                Main.NewText(Language.GetTextValue(localizationKey), textColor);
            else if (Main.netMode == NetmodeID.Server)
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey(localizationKey), textColor);
        }
    }
}