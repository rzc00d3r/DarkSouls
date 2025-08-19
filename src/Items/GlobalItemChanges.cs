using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using System.Collections.Generic;

using DarkSouls.DataStructures;
using static DarkSouls.Constants.Constants;
using DarkSouls.Config;

namespace DarkSouls.Items
{
    public class GlobalItemChanges : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal || item.type == ItemID.ManaCrystal || item.type == ItemID.LifeFruit)
                return false;

            if (ScalingSystemIsDisabled(item))
                return true;

            DarkSoulsPlayer dsPlayer = Main.LocalPlayer.GetModPlayer<DarkSoulsPlayer>();
            DarkSoulsScalingSystem.WeaponParams weaponParams = new();
            if (!DarkSoulsScalingSystem.AllWeaponsParams.TryGetValue(item.type, out weaponParams))
                return true;

            return dsPlayer.dsStrength >= weaponParams.ReqStrength &&
                    dsPlayer.dsDexterity >= weaponParams.ReqDexterity &&
                    dsPlayer.dsIntelligence >= weaponParams.ReqIntelligence &&
                    dsPlayer.dsFaith >= weaponParams.ReqFaith;
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            if (ScalingSystemIsDisabled(item))
                return;

            DarkSoulsScalingSystem.WeaponParams weaponParams;
            if (!DarkSoulsScalingSystem.AllWeaponsParams.TryGetValue(item.type, out weaponParams))
                return;

            DarkSoulsScalingSystem.DamageBonuses damageBonuses = DarkSoulsScalingSystem.GetBonusDamage(item);
            if (damageBonuses.total == 0)
                return;

            damage.Flat = damageBonuses.total;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ScalingSystemIsDisabled(item))
                return;

            DarkSoulsScalingSystem.WeaponParams weaponParams;
            if (!DarkSoulsScalingSystem.AllWeaponsParams.TryGetValue(item.type, out weaponParams))
                return;

            TooltipLine customTooltipLine = new(Mod, "WeaponParams", weaponParams.ToTooltipText());
            tooltips.Add(customTooltipLine);

            DarkSoulsScalingSystem.DamageBonuses damageBonuses = DarkSoulsScalingSystem.GetBonusDamage(item);
            if (damageBonuses.total == 0)
                return;

            int damageLineIndex = tooltips.FindIndex(line => line.Mod == "Terraria" && line.Name == "Damage");
            if (damageLineIndex != -1)
            {
                TooltipLine damageLine = tooltips[damageLineIndex];
                damageLine.Text += $" ([{MediumSeaGreenColorTooltip}:+{damageBonuses.total}] = " +
                    $"[{MediumSeaGreenColorTooltip}:{damageBonuses.byStrength}]+" +
                    $"[{MediumSeaGreenColorTooltip}:{damageBonuses.byDexterity}]+" +
                    $"[{MediumSeaGreenColorTooltip}:{damageBonuses.byIntelligence}]+" +
                    $"[{MediumSeaGreenColorTooltip}:{damageBonuses.byFaith}])";
            }
        }

        public static bool ScalingSystemIsDisabled(Item item)
        {
            if (item.ModItem == null && ServerConfig.Instance.DisableScalingSystemForVanilla)
                return true;
            if (DarkSouls.CalamityModIsEnabled)
                return item.ModItem?.Mod.Name == "CalamityMod" && ServerConfig.Instance.DisableScalingSystemForCalamity;
            return false;
        }
    }
}
