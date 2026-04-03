using System;
using System.ComponentModel;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.Chat;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace DarkSouls.Config
{
    public class ServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static ServerConfig Instance;

        public override void OnLoaded() => Instance = this;

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
        {
            ServerConfig newConfig = (ServerConfig)pendingConfig;
            string playerName = Main.player[whoAmI].name;

            string changes = string.Empty;
            string localizedFieldName = string.Empty;

            if (SoulsGainMultiplierPercent != newConfig.SoulsGainMultiplierPercent)
            {
                localizedFieldName = this.GetLocalization("SoulsGainMultiplierPercent.Label").Value;
                changes += $"  [{localizedFieldName}] {SoulsGainMultiplierPercent} -> {newConfig.SoulsGainMultiplierPercent}\n";
            }

            if (LevelUpCostMultiplierPercent != newConfig.LevelUpCostMultiplierPercent)
            {
                localizedFieldName = this.GetLocalization("LevelUpCostMultiplierPercent.Label").Value;
                changes += $"  [{localizedFieldName}] {LevelUpCostMultiplierPercent} -> {newConfig.LevelUpCostMultiplierPercent}\n";
            }

            if (DisableCrowdControlMultiplier != newConfig.DisableCrowdControlMultiplier)
            {
                localizedFieldName = this.GetLocalization("DisableCrowdControlMultiplier.Label").Value;
                changes += $"  [{localizedFieldName}] {DisableCrowdControlMultiplier} -> {newConfig.DisableCrowdControlMultiplier}\n";
            }

            if (DisableScalingSystemForVanilla != newConfig.DisableScalingSystemForVanilla)
            {
                localizedFieldName = this.GetLocalization("DisableScalingSystemForVanilla.Label").Value;
                changes += $"  [{localizedFieldName}] {DisableScalingSystemForVanilla} -> {newConfig.DisableScalingSystemForVanilla}\n";
            }

            if (DisableScalingSystemForCalamity != newConfig.DisableScalingSystemForCalamity)
            {
                localizedFieldName = this.GetLocalization("DisableScalingSystemForCalamity.Label").Value;
                changes += $"  [{localizedFieldName}] {DisableScalingSystemForCalamity} -> {newConfig.DisableScalingSystemForCalamity}\n";
            }

            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral($"\nPlayer {playerName} changes mod configuration!\nDetail:\n{changes}".Trim()), Color.Goldenrod);

            return true;
        }

        [Header("Balance")]
        [DefaultValue(100)]
        [Range(0, 500)]
        [Increment(5)]
        public int SoulsGainMultiplierPercent = 100;

        [DefaultValue(100)]
        [Range(0, 500)]
        [Increment(5)]
        public int LevelUpCostMultiplierPercent = 100;

        [DefaultValue(false)]
        public bool DisableCrowdControlMultiplier = false;

        [Header("Compatibility")]
        [DefaultValue(false)]
        public bool DisableVanillaDashLock = false;

        [Header("$Mods.DarkSouls.Configs.ServerConfig.Headers.ScalingSystem")]
        [DefaultValue(false)]
        public bool DisableScalingSystemForVanilla = false;

        [DefaultValue(false)]
        public bool DisableScalingSystemForCalamity = false;
    }
}
