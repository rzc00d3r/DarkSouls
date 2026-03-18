using System;

using static DarkSouls.DataStructures.DarkSoulsScalingSystem;

namespace DarkSouls.ModSupport
{
    public class ModCalls
    {
        public static bool RegisterWeapon(int itemType, WeaponParams weaponParams)
        {
            if (!AllWeaponsParams.ContainsKey(itemType))
            {
                AllWeaponsParams.Add(itemType, weaponParams);
                return true;
            }
            return false;
        }

        public static object Call(params object[] args)
        {
            if (args is null || args.Length == 0 || !(args[0] is string funcName))
                return new ArgumentException("[DarkSouls] Mod.Call: first argument must be method name!");

            switch (funcName)
            {
                case "RegisterWeapon":
                    // 0: RegisterWeapon
                    // 1: int itemType (ItemID)
                    // 2-5: int reqStrength, int reqDexterity, int regIntelligence, int reqFaith
                    // 6-9: int strengthScalingGrade, int dexterityScalingGrade, int intelligenceScalingGrade, int faithScalingGrade
                    // 10: float saturation
                    if (args.Length < 11)
                        return new ArgumentException("[DarkSouls] Mod.Call: Not enough arguments for method RegisterWeapon!");
                    try
                    {
                        int itemType = Convert.ToInt32(args[1]);

                        int reqStr = Convert.ToInt32(args[2]);
                        int reqDex = Convert.ToInt32(args[3]);
                        int reqInt = Convert.ToInt32(args[4]);
                        int reqFth = Convert.ToInt32(args[5]);

                        ScalingGrade strScale = (ScalingGrade)Convert.ToInt32(args[6]);
                        ScalingGrade dexScale = (ScalingGrade)Convert.ToInt32(args[7]);
                        ScalingGrade intScale = (ScalingGrade)Convert.ToInt32(args[8]);
                        ScalingGrade fthScale = (ScalingGrade)Convert.ToInt32(args[9]);

                        float saturation = Convert.ToSingle(args[10]);

                        WeaponParams wp = new WeaponParams(
                            reqStr, reqDex, reqInt, reqFth,
                            strScale, dexScale, intScale, fthScale,
                            saturation
                        );

                        return RegisterWeapon(itemType, wp);
                    }
                    catch (Exception ex)
                    {
                        return new ArgumentException($"[DarkSouls] Mod.Call: method RegisterWeapon, exception: {ex.Message}!");
                    }
                default:
                    return new ArgumentException("[DarkSouls] Mod.Call: Invalid method name!");
            }
        }
    }
}
