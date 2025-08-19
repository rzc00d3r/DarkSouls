using System;
using System.ComponentModel;

using Terraria.ModLoader.Config;

namespace DarkSouls.Config
{
    public class ServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static ServerConfig Instance;

        public override void OnLoaded() => Instance = this;

        [Header("Balance")]
        [DefaultValue(100)]
        [Range(0, 500)]
        [Increment(5)]
        public int SoulsGainMultiplierPercent = 100;

        [DefaultValue(100)]
        [Range(0, 500)]
        [Increment(5)]
        public int LevelUpCostMultiplierPercent = 100;

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
