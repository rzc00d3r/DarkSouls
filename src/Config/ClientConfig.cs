using Terraria;
using Terraria.ModLoader.Config;

using System;
using System.IO;
using System.ComponentModel;

using Newtonsoft.Json.Linq;
using DarkSouls.Utils;

namespace DarkSouls.Config
{
    public class ClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static ClientConfig Instance;

        public override void OnLoaded() => Instance = this;

        [Header("ResourcePack")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableOverrideHurtSounds = false;

        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableOverrideMusic = false;

        [Header("Interface")]
        [DefaultValue(false)]
        public bool DisableDeathScreen = false;

        [DefaultValue(95)]
        [Range(0, 100)]
        public int SoulsCounterXPercent = 95;

        [DefaultValue(95)]
        [Range(0, 100)]
        public int SoulsCounterYPercent = 95;

        [Header("Other")]
        [DefaultValue(false)]
        public bool DisableDash = false;

        public static bool GetValueFromJSON(string valueName)
        {
            string path = Path.Combine(Main.SavePath, "ModConfigs", "DarkSouls_ClientConfig.json");
            if (!File.Exists(path))
                return false;

            try
            {
                string data = File.ReadAllText(path);
                JObject json = JObject.Parse(data);
                return json[valueName]?.Value<bool>() ?? false;
            }
            catch (Exception ex)
            {
                LoggingUtils.Error("ClientConfig", $"Failed to read value with name \"{valueName}\" from JSON: {ex}");
            }

            return false;
        }
    }
}
