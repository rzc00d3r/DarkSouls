using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using DarkSouls.Core;

namespace DarkSouls
{
    public class DarkSoulsBuffChanges : GlobalBuff
    {
        public override bool ReApply(int type, Player player, int time, int buffIndex)
        {
            if (!Main.debuff[type])
                return false;

            if (type == BuffID.PotionSickness)
                return false;

            if (time <= 2)
                return false;

            if (time > player.buffTime[buffIndex])
                player.buffTime[buffIndex] = (int)(time * (1 - StatFormulas.GetDebuffsResistanceByResistance(player.GetModPlayer<DarkSoulsPlayer>().dsResistance)));

            return true;
        }
    }
}