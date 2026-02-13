using System.Collections.Generic;

using Terraria;
//using Terraria.ModLoader;
using Terraria.ID;

using DarkSouls.Core;
//using DarkSouls.Utils;
using static DarkSouls.Constants.Constants;

namespace DarkSouls.DataStructures
{
    //public class DarkSoulsScalingSystemDebug : ModSystem
    //{
    //    public override void PostSetupContent()
    //    {
    //        foreach (var kv in DarkSoulsScalingSystem.AllWeaponsParams)
    //        {
    //            Item item = ContentSamples.ItemsByType[kv.Key];
    //            var weaponParams = kv.Value;

    //            int maxReq = weaponParams.ReqStrength;
    //            DamageClass expectedClass = DamageClass.Melee;

    //            if (weaponParams.ReqDexterity > maxReq)
    //            {
    //                maxReq = weaponParams.ReqDexterity;
    //                expectedClass = DamageClass.Ranged;
    //            }

    //            if (weaponParams.ReqIntelligence > maxReq)
    //            {
    //                maxReq = weaponParams.ReqIntelligence;
    //                expectedClass = DamageClass.Magic;
    //            }

    //            if (weaponParams.ReqFaith > maxReq)
    //            {
    //                maxReq = weaponParams.ReqFaith;
    //                expectedClass = DamageClass.Summon;
    //            }

    //            if (!item.DamageType.CountsAsClass(expectedClass))
    //            {
    //                string itemName = item.ModItem?.FullName ?? item.Name;
    //                LoggingUtils.Info("DarkSoulsScalingSystemDebug",
    //                    $"Item '{itemName}', {item.DamageType.Name}, WeaponParams ({weaponParams.ToString()})");
    //            }
    //        }
    //    }
    //}

    public static class DarkSoulsScalingSystem
    {
        // Set by LocalizationUpdater
        public static string ReqParamDisplayName;
        public static string ParamBonusDisplayName;
        public static string StrengthDisplayName;
        public static string DexterityDisplayName;
        public static string IntelligenceDisplayName;
        public static string FaithDisplayName;

        public enum ScalingGrade
        {
            None = 0,
            S = 1,
            A = 2,
            B = 3,
            C = 4,
            D = 5,
            E = 6
        }

        public struct WeaponParams
        {
            public int ReqStrength { get; set; }
            public int ReqDexterity { get; set; }
            public int ReqIntelligence { get; set; }
            public int ReqFaith { get; set; }

            public ScalingGrade StrengthScalingGrade { get; set; }
            public ScalingGrade DexterityScalingGrade { get; set; } 
            public ScalingGrade IntelligenceScalingGrade { get; set; }
            public ScalingGrade FaithScalingGrade { get; set; }

            public float Saturation { get; set; }

            public WeaponParams(
                int reqStrength = 0, int reqDexterity = 0, int reqIntelligence = 0, int reqFaith = 0,
                ScalingGrade strengthScalingGrade = ScalingGrade.None,
                ScalingGrade dexterityScalingGrade = ScalingGrade.None,
                ScalingGrade intelligenceScalingGrade = ScalingGrade.None,
                ScalingGrade faithScalingGrade = ScalingGrade.None,
                float saturation = 100)
            {
                ReqStrength = reqStrength;
                ReqDexterity = reqDexterity;
                ReqIntelligence = reqIntelligence;
                ReqFaith = reqFaith;

                StrengthScalingGrade = strengthScalingGrade;
                DexterityScalingGrade = dexterityScalingGrade;
                IntelligenceScalingGrade = intelligenceScalingGrade;
                FaithScalingGrade = faithScalingGrade;
                Saturation = saturation;
            }


            public string ToTooltipText()
            {
                DarkSoulsPlayer dsPlayer = Main.LocalPlayer.GetModPlayer<DarkSoulsPlayer>();
                string text = string.Empty;
                string strengthText = dsPlayer.dsStrength >= ReqStrength ? $"{StrengthDisplayName}: [{DodgerBlueColorTooltip}:{ReqStrength}]" : $"{StrengthDisplayName}: [{CrimsonColorTooltip}:{ReqStrength}]";
                string dexterityText = dsPlayer.dsDexterity >= ReqDexterity ? $"{DexterityDisplayName}: [{DodgerBlueColorTooltip}:{ReqDexterity}]" : $"{DexterityDisplayName}: [{CrimsonColorTooltip}:{ReqDexterity}]";
                string intelligenceText = dsPlayer.dsIntelligence >= ReqIntelligence ? $"{IntelligenceDisplayName}: [{DodgerBlueColorTooltip}:{ReqIntelligence}]" : $"{IntelligenceDisplayName}: [{CrimsonColorTooltip}:{ReqIntelligence}]";
                string faithText = dsPlayer.dsFaith >= ReqFaith ? $"{FaithDisplayName}: [{DodgerBlueColorTooltip}:{ReqFaith}]" : $"{FaithDisplayName}: [{CrimsonColorTooltip}:{ReqFaith}]";

                text = $"{ReqParamDisplayName}:\n  {strengthText}, {dexterityText}, {intelligenceText}, {faithText}\n";
                text += $"{ParamBonusDisplayName}:\n  {StrengthDisplayName}: {ScalingGradeToString(StrengthScalingGrade)}, " +
                        $"{DexterityDisplayName}: {ScalingGradeToString(DexterityScalingGrade)}, " +
                        $"{IntelligenceDisplayName}: {ScalingGradeToString(IntelligenceScalingGrade)}, " +
                        $"{FaithDisplayName}: {ScalingGradeToString(FaithScalingGrade)}";
                return text;
            }

            public new string ToString() => $"{ReqStrength}, {ReqDexterity}, {ReqIntelligence}, {ReqFaith}";
        }

        public readonly struct DamageBonuses
        {
            public readonly int total;
            public readonly int byStrength;
            public readonly int byDexterity;
            public readonly int byIntelligence;
            public readonly int byFaith;

            public DamageBonuses(int total, int byStrength, int byDexterity, int byIntelligence, int byFaith)
            {
                this.total = total;
                this.byStrength = byStrength;
                this.byDexterity = byDexterity;
                this.byIntelligence = byIntelligence;
                this.byFaith = byFaith;
            }
        }


        private static Dictionary<int, WeaponParams> allWeaponsParams = new()
        {
            {ItemID.AbigailsFlower, new() },

            {ItemID.AdamantiteChainsaw, new(14, 10, 0, 0) },
            {ItemID.AdamantiteDrill, new(8, 0, 0, 0) },
            {ItemID.AdamantiteGlaive, new(15, 10, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.AdamantitePickaxe, new(8, 0, 0, 0) },
            {ItemID.AdamantiteRepeater, new(3, 21, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.AdamantiteSword, new(16, 10, 0, 0, ScalingGrade.C, ScalingGrade.E) },
            {ItemID.AdamantiteWaraxe, new(21, 6, 0, 0, ScalingGrade.C, ScalingGrade.E) },

            {ItemID.TitaniumChainsaw, new(14, 10, 0, 0) },
            {ItemID.TitaniumDrill, new(8, 0, 0, 0) },
            {ItemID.TitaniumTrident, new(15, 10, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.TitaniumPickaxe, new(8, 0, 0, 0) },
            {ItemID.TitaniumRepeater, new(3, 21, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.TitaniumSword, new(16, 10, 0, 0, ScalingGrade.C, ScalingGrade.E) },
            {ItemID.TitaniumWaraxe, new(21, 6, 0, 0, ScalingGrade.C, ScalingGrade.E) },

            {ItemID.DD2BetsyBow, new(8, 30, 0, 0, ScalingGrade.E, ScalingGrade.B) },
            {ItemID.AleThrowingGlove, new(8, 12, 0, 0, ScalingGrade.E, ScalingGrade.C) },
            {ItemID.Amarok, new(12, 12, 0, 0, ScalingGrade.C, ScalingGrade.C) },
            {ItemID.JungleYoyo, new(5, 5, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.AmberStaff, new(2, 2, 5, 1, ScalingGrade.None, ScalingGrade.None, ScalingGrade.D, ScalingGrade.None) },
            {ItemID.AmethystStaff, new(2, 2, 5, 1, ScalingGrade.None, ScalingGrade.None, ScalingGrade.D, ScalingGrade.None) },
            {ItemID.Anchor, new(25, 5, 0, 0, ScalingGrade.B) },
            {ItemID.AquaScepter, new(2, 2, 12, 0, ScalingGrade.None, ScalingGrade.None, ScalingGrade.D, ScalingGrade.None) },
            {ItemID.Arkhalis, new(8, 12, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.CrimsonYoyo, new(6, 6, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.AshWoodBow, new() },
            {ItemID.AshWoodHammer, new()},
            {ItemID.AshWoodSword, new() },
            {ItemID.AcornAxe, new(3, 5, 0, 0) },
            {ItemID.BallOHurt, new(8, 5, 0, 0, ScalingGrade.E) },
            {ItemID.DD2BallistraTowerT1Popper, new(0, 0, 8, 15, intelligenceScalingGrade: ScalingGrade.E, faithScalingGrade: ScalingGrade.C) },
            {ItemID.DD2BallistraTowerT2Popper, new(0, 0, 8, 15, intelligenceScalingGrade: ScalingGrade.E, faithScalingGrade: ScalingGrade.C) },
            {ItemID.DD2BallistraTowerT3Popper, new(0, 0, 8, 15, intelligenceScalingGrade: ScalingGrade.E, faithScalingGrade: ScalingGrade.C) },
            {ItemID.Bananarang, new(12, 12, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.BatBat, new(18, 5, 0, 0, ScalingGrade.C, ScalingGrade.E) },
            {ItemID.BatScepter, new(3, 3, 22, 5, intelligenceScalingGrade: ScalingGrade.C, faithScalingGrade: ScalingGrade.E) },
            {ItemID.BeamSword, new(15, 5, 5, 0, ScalingGrade.C, ScalingGrade.None, ScalingGrade.E) },
            {ItemID.BeeGun, new(3, 2, 12, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.BeeKeeper, new(10, 5, 0, 0, ScalingGrade.D) },
            {ItemID.Beenade, new(5, 8, 0, 0, ScalingGrade.None, ScalingGrade.A) },
            {ItemID.ApprenticeStaffT3, new(5, 3, 37, 0, intelligenceScalingGrade: ScalingGrade.B) },
            {ItemID.BladeofGrass, new(8, 5, 0, 0, ScalingGrade.D) },
            {ItemID.Smolstar, new(1, 1, 3, 15, faithScalingGrade: ScalingGrade.C) },
            {ItemID.BladedGlove, new(5, 12, 0, 0, ScalingGrade.E, ScalingGrade.D) },
            {ItemID.Bladetongue, new(12, 5, 5, 0, ScalingGrade.D, ScalingGrade.None, ScalingGrade.E) },
            {ItemID.BlizzardStaff, new(2, 2, 36, 0, intelligenceScalingGrade: ScalingGrade.B) },
            {ItemID.BloodButcherer, new(10, 2, 0, 0, ScalingGrade.D) },
            {ItemID.BloodLustCluster, new(10, 2, 0, 0, ScalingGrade.D) },
            {ItemID.BloodRainBow, new(2, 10, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.SharpTears, new(2, 2, 16, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.BloodWater, new() },
            {ItemID.BloodyMachete, new(6, 6, 0, 0, ScalingGrade.E, ScalingGrade.E) },
            {ItemID.Blowgun, new(2, 12, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.Blowpipe, new(2, 3, 0, 0, dexterityScalingGrade: ScalingGrade.E) },
            {ItemID.BlueMoon, new(12, 5, 0, 0, ScalingGrade.D) },

            {ItemID.BluePhaseblade, new(8, 6, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.GreenPhaseblade, new(8, 6, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.OrangePhaseblade, new(8, 6, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.PurplePhaseblade, new(8, 6, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.RedPhaseblade, new(8, 6, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.WhitePhaseblade, new(8, 6, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.YellowPhaseblade, new(8, 6, 0, 0, ScalingGrade.D, ScalingGrade.E) },

            {ItemID.BluePhasesaber, new(12, 12, 0, 0, ScalingGrade.D, ScalingGrade.C) },
            {ItemID.GreenPhasesaber, new(12, 12, 0, 0, ScalingGrade.D, ScalingGrade.C) },
            {ItemID.OrangePhasesaber, new(12, 12, 0, 0, ScalingGrade.D, ScalingGrade.C) },
            {ItemID.PurplePhasesaber, new(12, 12, 0, 0, ScalingGrade.D, ScalingGrade.C) },
            {ItemID.RedPhasesaber, new(12, 12, 0, 0, ScalingGrade.D, ScalingGrade.C) },
            {ItemID.WhitePhasesaber, new(12, 12, 0, 0, ScalingGrade.D, ScalingGrade.B) },
            {ItemID.YellowPhasesaber, new(12, 12, 0, 0, ScalingGrade.D, ScalingGrade.C) },

            {ItemID.BlueRocket, new(dexterityScalingGrade: ScalingGrade.S) },
            {ItemID.GreenRocket, new(dexterityScalingGrade: ScalingGrade.S) },
            {ItemID.RedRocket, new(dexterityScalingGrade: ScalingGrade.S) },
            {ItemID.YellowRocket, new(dexterityScalingGrade: ScalingGrade.S) },

            {ItemID.Bomb, new(dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.BombFish, new(dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.Bone, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.BoneJavelin, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.BonePickaxe, new(3, 0, 0, 0) },
            {ItemID.BoneSword, new(8, 2, 0, 0, ScalingGrade.E) },
            {ItemID.BoneDagger, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.BookofSkulls, new(2, 2, 13, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.Boomstick, new(2, 8, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.BorealWoodBow, new() },
            {ItemID.BorealWoodHammer, new()},
            {ItemID.BorealWoodSword, new() },
            {ItemID.BouncyBomb, new(dexterityScalingGrade: ScalingGrade.S) },
            {ItemID.BouncyDynamite, new(dexterityScalingGrade: ScalingGrade.S) },
            {ItemID.BouncyGrenade, new(dexterityScalingGrade: ScalingGrade.S) },
            {ItemID.DD2SquireDemonSword, new(30, 8, 0, 0, ScalingGrade.C) },
            {ItemID.BreakerBlade, new(20, 5, 0, 0, ScalingGrade.B) },
            {ItemID.BreathingReed, new(3, 3, 0, 0) },
            {ItemID.BubbleGun, new(2, 2, 32, 0, intelligenceScalingGrade: ScalingGrade.B) },
            {ItemID.BunnyCannon, new() },
            {ItemID.ButchersChainsaw, new(20, 15, 0, 0, ScalingGrade.C, ScalingGrade.C) },
            {ItemID.CactusPickaxe, new() },
            {ItemID.CactusSword, new(3, 1, 0, 0) },
            {ItemID.CnadyCanePickaxe, new(3, 0, 0, 0) },
            {ItemID.CandyCaneSword, new(8, 2, 0, 0, ScalingGrade.E) },
            {ItemID.CandyCornRifle, new(6, 25, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.Cannon, new() },
            {ItemID.Cascade, new(8, 8, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.FireworksLauncher, new(5, 25, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.Celeb2, new(10, 40, 0, 0, dexterityScalingGrade: ScalingGrade.A) },
            {ItemID.ChainGuillotines, new(20, 5, 0, 0, ScalingGrade.C) },
            {ItemID.ChainGun, new(8, 32, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.ChainKnife, new(6, 5, 0, 0, ScalingGrade.D) },
            {ItemID.ChargedBlasterCannon, new(3, 3, 35, 0, 0, intelligenceScalingGrade: ScalingGrade.B) },
            {ItemID.Chik, new(12, 12, 0, 0, ScalingGrade.C, ScalingGrade.C) },
            {ItemID.ChlorophyteChainsaw, new(18, 8, 0, 0, ScalingGrade.C) },
            {ItemID.ChlorophyteClaymore, new(22, 5, 0, 0, ScalingGrade.C, ScalingGrade.E) },
            {ItemID.ChlorophyteDrill, new(9, 0, 0, 0) },
            {ItemID.ChlorophyteGreataxe, new(25, 5, 0, 0, ScalingGrade.B, ScalingGrade.E) },
            {ItemID.ChlorophyteJackhammer, new(15, 12, 0, 0) },
            {ItemID.ChlorophytePartisan, new(20, 10, 0, 0, ScalingGrade.C, ScalingGrade.D) },
            {ItemID.ChlorophyteSaber, new(20, 6, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.ChlorophyteShotbow, new(5, 26, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.ChlorophyteWarhammer, new(28, 5, 0, 0, ScalingGrade.A, ScalingGrade.E) },
            {ItemID.ChlorophytePickaxe, new(9, 0, 0, 0) },

            {ItemID.ChristmasTreeSword, new(25, 8, 0, 0, ScalingGrade.B, ScalingGrade.D) },
            {ItemID.TaxCollectorsStickOfDoom, new(6, 6, 0, 0, ScalingGrade.B, ScalingGrade.B) },
            {ItemID.ClingerStaff, new(3, 2, 25, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.ClockworkAssaultRifle, new(5, 15, 0, 0, dexterityScalingGrade: ScalingGrade.C) },

            {ItemID.CobaltChainsaw, new(12, 8, 0, 0) },
            {ItemID.CobaltDrill, new(6, 0, 0, 0) },
            {ItemID.CobaltNaginata, new(12, 8, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.CobaltPickaxe, new(6, 0, 0, 0) },
            {ItemID.CobaltRepeater, new(2, 18, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.CobaltSword, new(15, 5, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.CobaltWaraxe, new(18, 4, 0, 0, ScalingGrade.D, ScalingGrade.E) },

            {ItemID.PalladiumChainsaw, new(12, 8, 0, 0) },
            {ItemID.PalladiumDrill, new(6, 0, 0, 0) },
            {ItemID.PalladiumPike, new(12, 8, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.PalladiumPickaxe, new(6, 0, 0, 0) },
            {ItemID.PalladiumRepeater, new(2, 18, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.PalladiumSword, new(15, 5, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.PalladiumWaraxe, new(18, 4, 0, 0, ScalingGrade.D, ScalingGrade.E) },

            {ItemID.Code1, new(10, 10, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.Code2, new(14, 14, 0, 0, ScalingGrade.C, ScalingGrade.C) },
            {ItemID.CoinGun, new(5, 20, 0, 0) },
            {ItemID.CombatWrench, new(8, 8, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.CoolWhip, new(2, 3, 0, 18, faithScalingGrade: ScalingGrade.C) },
            {ItemID.CopperAxe, new() },
            {ItemID.CopperBow, new() },
            {ItemID.CopperBroadsword, new() },
            {ItemID.CopperHammer, new() },
            {ItemID.CopperPickaxe, new() },
            {ItemID.CopperShortsword, new() },
            {ItemID.CrimsonRod, new(1, 1, 10, 0, intelligenceScalingGrade: ScalingGrade.E) },
            {ItemID.CrystalSerpent, new(2, 2, 16, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.CrystalStorm, new(2, 2, 16, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.CrystalVileShard, new(2, 2, 14, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.CursedFlames, new(2, 2, 20, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.Cutlass, new(16, 8, 0, 0, ScalingGrade.C, ScalingGrade.E) },
            {ItemID.DaedalusStormbow, new(4, 21, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.DaoofPow, new(20, 5, 0, 0, ScalingGrade.C) },
            {ItemID.ScytheWhip, new(2, 4, 0, 30, faithScalingGrade: ScalingGrade.C) },
            {ItemID.DarkLance, new(12, 5, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.DartPistol, new(5, 15, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.DartRifle, new(5, 15, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.DeadlySphereStaff, new(2, 2, 0, 35, faithScalingGrade: ScalingGrade.C) },
            {ItemID.DeathSickle, new(26, 8, 0, 0, ScalingGrade.C, ScalingGrade.D) },
            {ItemID.DeathbringerPickaxe, new(4, 0, 0, 0) },
            {ItemID.DemonBow, new(4, 8, 0, 0, dexterityScalingGrade: ScalingGrade.E) },
            {ItemID.DemonScythe, new(2, 2, 16, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.StormTigerStaff, new(2, 2, 0, 36, faithScalingGrade: ScalingGrade.C) },
            {ItemID.DiamondStaff, new(2, 2, 6, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.DirtBomb, new(dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.Drax, new(9, 0, 0, 0) },
            {ItemID.DripplerFlail, new(15, 3, 0, 0, ScalingGrade.C) },
            {ItemID.DryBomb, new(dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.SwordWhip, new(2, 3, 0, 24, faithScalingGrade: ScalingGrade.C) },
            {ItemID.Dynamite, new(dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.EbonwoodBow, new() },
            {ItemID.EbonwoodHammer, new() },
            {ItemID.EbonwoodSword, new() },
            {ItemID.ElectrosphereLauncher, new(2, 32, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.ElfMelter, new(2, 32, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.EmeraldStaff, new(2, 2, 6, 0, intelligenceScalingGrade: ScalingGrade.E) },
            {ItemID.EnchantedBoomerang, new(5, 5, 0, 0, ScalingGrade.E, ScalingGrade.E) },
            {ItemID.EnchantedSword, new(7, 2, 2, 0, ScalingGrade.D, ScalingGrade.None, ScalingGrade.E) },
            {ItemID.FairyQueenRangedItem, new(2, 30, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.Excalibur, new(20, 8, 0, 0, ScalingGrade.B, ScalingGrade.E) },
            {ItemID.DyeTradersScimitar, new(10, 4, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.DD2ExplosiveTrapT1Popper, new(0, 0, 8, 15, intelligenceScalingGrade: ScalingGrade.E, faithScalingGrade: ScalingGrade.C) },
            {ItemID.DD2ExplosiveTrapT2Popper, new(0, 0, 8, 15, intelligenceScalingGrade: ScalingGrade.E, faithScalingGrade: ScalingGrade.C) },
            {ItemID.DD2ExplosiveTrapT3Popper, new(0, 0, 8, 15, intelligenceScalingGrade: ScalingGrade.E, faithScalingGrade: ScalingGrade.C) },
            {ItemID.FalconBlade, new(8, 6, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.FetidBaghnakhs, new(15, 10, 0, 0, ScalingGrade.C, ScalingGrade.D) },
            {ItemID.BabyBirdStaff, new() },
            {ItemID.FireWhip, new(2, 3, 0, 15, faithScalingGrade: ScalingGrade.D) },
            {ItemID.Flairon, new(28, 4, 0, 0, ScalingGrade.B, ScalingGrade.E) },
            {ItemID.Flamarang, new(10, 10, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.DD2FlameburstTowerT1Popper, new(0, 0, 8, 15, intelligenceScalingGrade: ScalingGrade.E, faithScalingGrade: ScalingGrade.C) },
            {ItemID.DD2FlameburstTowerT2Popper, new(0, 0, 8, 15, intelligenceScalingGrade: ScalingGrade.E, faithScalingGrade: ScalingGrade.C) },
            {ItemID.DD2FlameburstTowerT3Popper, new(0, 0, 8, 15, intelligenceScalingGrade: ScalingGrade.E, faithScalingGrade: ScalingGrade.C) },
            {ItemID.Flamelash, new(2, 2, 18, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.Flamethrower, new(2, 22, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.FlamingMace, new(10, 5, 0, 0, ScalingGrade.D) },
            {ItemID.FlareGun, new(2, 10, 0, 0, dexterityScalingGrade: ScalingGrade.A) },
            {ItemID.FleshGrinder, new(12, 3, 0, 0, ScalingGrade.C) },
            {ItemID.FlintlockPistol, new(2, 8, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.FlinxStaff, new(1, 1, 0, 6, faithScalingGrade: ScalingGrade.E) },
            {ItemID.FlowerofFire, new(2, 2, 14, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.FlowerofFrost, new(2, 2, 16, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.FlowerPow, new(28, 5, 0, 0, ScalingGrade.B) },
            {ItemID.DD2SquireBetsySword, new(30, 6, 0, 0, ScalingGrade.B) },
            {ItemID.FlyingKnife, new(18, 5, 0, 0, ScalingGrade.D) },
            {ItemID.Flymeal, new(8, 5, 0, 0, ScalingGrade.E) },
            {ItemID.FormatC, new(11, 11, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.FossilPickaxe, new(3, 0, 0, 0) },
            {ItemID.FrostDaggerfish, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.FrostStaff, new(2, 2, 16, 0, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.Frostbrand, new(10, 4, 6, 0, ScalingGrade.D, ScalingGrade.E, ScalingGrade.E) },
            {ItemID.FruitcakeChakram, new(6, 6, 0, 0, ScalingGrade.E, ScalingGrade.E) },
            {ItemID.Gatligator, new(3, 15, 0, 0, ScalingGrade.None, ScalingGrade.C) },
            {ItemID.MonkStaffT2, new(16, 12, 0, 0, ScalingGrade.C, ScalingGrade.D) },
            {ItemID.Gladius, new(8, 6, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.GoldAxe, new(5, 4, 0, 0) },
            {ItemID.GoldBow, new(4, 6, 0, 0, dexterityScalingGrade: ScalingGrade.E) },
            {ItemID.GoldBroadsword, new(8, 2, 0, 0, ScalingGrade.E) },
            {ItemID.GoldHammer, new(9, 2, 0, 0, ScalingGrade.D) },
            {ItemID.GoldPickaxe, new(3, 0, 0, 0) },
            {ItemID.GoldenShower, new(2, 2, 15, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.GolemFist, new(30, 2, 0, 0, ScalingGrade.A) },
            {ItemID.Gradient, new(13, 13, 0, 0, ScalingGrade.C, ScalingGrade.C) },
            {ItemID.GravediggerShovel, new(6, 2, 0, 0, strengthScalingGrade: ScalingGrade.D) },
            {ItemID.ZapinatorGray, new(2, 2, 14, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.Grenade, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.GrenadeLauncher, new(2, 26, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.Gungnir, new(16, 12, 0, 0, ScalingGrade.C, ScalingGrade.D) },
            {ItemID.BloodHamaxe, new(16, 4, 0, 0, ScalingGrade.B) },
            {ItemID.HallowJoustingLance, new(30, 5, 0, 0, ScalingGrade.S) },
            {ItemID.HallowedRepeater, new(3, 25, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.HamBat, new(25, 5, 0, 0, ScalingGrade.A) },
            {ItemID.Hammush, new(20, 3, 0, 0, ScalingGrade.C) },
            {ItemID.Handgun, new(3, 12, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.PartyGirlGrenade, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.S) },
            {ItemID.Harpoon, new(2, 13, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.HeatRay, new(1, 1, 30, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.HelFire, new(11, 11, 0, 0, ScalingGrade.C, ScalingGrade.C) },
            {ItemID.HellwingBow, new(2, 12, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.HiveFive, new(7, 7, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.HolyWater, new() },
            {ItemID.HoneyBomb, new(dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.HornetStaff, new(2, 2, 0, 8, faithScalingGrade: ScalingGrade.D) },
            {ItemID.HoundiusShootius, new(2, 2, 0, 10, faithScalingGrade: ScalingGrade.D) },
            {ItemID.IceBlade, new(6, 2, 2, 0, ScalingGrade.E, ScalingGrade.None, ScalingGrade.E) },
            {ItemID.IceBoomerang, new(6, 6, 0, 0, ScalingGrade.E, ScalingGrade.E) },
            {ItemID.IceBow, new(4, 16, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.IceRod, new(2, 2, 16, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.IceSickle, new(15, 5, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.ImpStaff, new(2, 2, 0, 13, faithScalingGrade: ScalingGrade.D) },
            {ItemID.InfernoFork, new(2, 2, 32, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.InfluxWaver, new(26, 10, 0, 0, ScalingGrade.C, ScalingGrade.D) },
            {ItemID.IronAxe, new() },
            {ItemID.IronBow, new() },
            {ItemID.IronBroadsword, new() },
            {ItemID.IronHammer, new() },
            {ItemID.IronPickaxe, new() },
            {ItemID.IronShortsword, new() },
            {ItemID.JackOLanternLauncher, new(5, 30, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.Javelin, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.JoustingLance, new(20, 5, 0, 0, ScalingGrade.S) },
            {ItemID.RainbowWhip, new(2, 6, 0, 36, faithScalingGrade: ScalingGrade.A) },
            {ItemID.Katana, new(8, 2, 0, 0, ScalingGrade.E) },
            {ItemID.Keybrand, new(28, 6, 0, 0, ScalingGrade.B, ScalingGrade.D) },
            {ItemID.KOCannon, new(18, 5, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.Kraken, new(18, 18, 0, 0, ScalingGrade.C, ScalingGrade.C) },
            {ItemID.LandMine, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.S) },
            {ItemID.LaserDrill, new(11, 0, 0, 0) },
            {ItemID.LaserMachinegun, new(4, 2, 32, 0, intelligenceScalingGrade: ScalingGrade.B) },
            {ItemID.LaserRifle, new(2, 2, 16, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.LastPrism, new(2, 2, 50, 0, intelligenceScalingGrade: ScalingGrade.A) },
            {ItemID.LavaBomb, new(dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.LeadBow, new() },
            {ItemID.LeadBroadsword, new() },
            {ItemID.LeadHammer, new() },
            {ItemID.LeadPickaxe, new() },
            {ItemID.LeadShortsword, new() },
            {ItemID.LeafBlower, new(1, 1, 30, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.BlandWhip, new(1, 1, 0, 3, faithScalingGrade: ScalingGrade.D) },
            {ItemID.SoulDrain, new(1, 1, 22, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.LightDisc, new(14, 14, 0, 0, ScalingGrade.C, ScalingGrade.C) },
            {ItemID.LightsBane, new(10, 2, 0, 0, ScalingGrade.D) },
            {ItemID.DD2LightningAuraT1Popper, new(0, 0, 6, 12, intelligenceScalingGrade: ScalingGrade.E, faithScalingGrade: ScalingGrade.C) },
            {ItemID.DD2LightningAuraT2Popper, new(0, 0, 6, 12, intelligenceScalingGrade: ScalingGrade.E, faithScalingGrade: ScalingGrade.C) },
            {ItemID.DD2LightningAuraT3Popper, new(0, 0, 6, 12, intelligenceScalingGrade: ScalingGrade.E, faithScalingGrade: ScalingGrade.C) },
            {ItemID.LucyTheAxe, new(12, 2, 0, 0, ScalingGrade.D) },
            {ItemID.LunarFlareBook, new(2, 2, 50, 0, intelligenceScalingGrade: ScalingGrade.A) },
            {ItemID.MoonlordTurretStaff, new(2, 2, 0, 45, faithScalingGrade: ScalingGrade.A) },
            {ItemID.Mace, new(8, 4, 0, 0, ScalingGrade.D) },
            {ItemID.MagicDagger, new(2, 2, 18, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.MagicMissile, new(2, 2, 12, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.MagicalHarp, new(2, 2, 18, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.MagnetSphere, new(2, 2, 30, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.CorruptYoyo, new(6, 6, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.AntlionClaw, new(8, 4, 0, 0, ScalingGrade.E) },
            {ItemID.Marrow, new(4, 20, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.MedusaHead, new(1, 1, 20, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.Megashark, new(3, 21, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.Meowmere, new(40, 10, 0, 0, ScalingGrade.A, ScalingGrade.D) },
            {ItemID.MeteorHamaxe, new(12, 3, 0, 0, ScalingGrade.D) },
            {ItemID.MeteorStaff, new(2, 2, 21, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.Minishark, new(2, 10, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.MolotovCocktail, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.MoltenFury, new(2, 14, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.MoltenHamaxe, new(14, 2, 0, 0, ScalingGrade.C) },
            {ItemID.MoltenPickaxe, new(5, 0, 0, 0) },
            {ItemID.MaceWhip, new(2, 5, 0, 32, faithScalingGrade: ScalingGrade.B) },
            {ItemID.Muramasa, new(8, 6, 0, 0, ScalingGrade.C, ScalingGrade.D) },
            {ItemID.MushroomSpear, new(15, 8, 0, 0, ScalingGrade.C, ScalingGrade.D) },
            {ItemID.Musket, new(2, 12, 0, 0, dexterityScalingGrade: ScalingGrade.C) },

            {ItemID.MythrilChainsaw, new(14, 8, 0, 0) },
            {ItemID.MythrilDrill, new(7, 0, 0, 0) },
            {ItemID.MythrilHalberd, new(12, 10, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.MythrilPickaxe, new(7, 0, 0, 0) },
            {ItemID.MythrilRepeater, new(2, 20, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.MythrilSword, new(18, 5, 0, 0, ScalingGrade.C, ScalingGrade.E) },
            {ItemID.MythrilWaraxe, new(20, 5, 0, 0, ScalingGrade.C, ScalingGrade.E) },

            {ItemID.OrichalcumChainsaw, new(14, 8, 0, 0) },
            {ItemID.OrichalcumDrill, new(7, 0, 0, 0) },
            {ItemID.OrichalcumHalberd, new(12, 10, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.OrichalcumPickaxe, new(7, 0, 0, 0) },
            {ItemID.OrichalcumRepeater, new(2, 20, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.OrichalcumSword, new(18, 5, 0, 0, ScalingGrade.C, ScalingGrade.E) },
            {ItemID.OrichalcumWaraxe, new(20, 5, 0, 0, ScalingGrade.C, ScalingGrade.E) },

            {ItemID.NailGun, new(6, 27, 0, 0, dexterityScalingGrade: ScalingGrade.C) },

            {ItemID.NebulaArcanum, new(2, 2, 40, 0, intelligenceScalingGrade: ScalingGrade.A) },
            {ItemID.NebulaBlaze, new(2, 2, 42, 0, intelligenceScalingGrade: ScalingGrade.B) },
            {ItemID.NebulaDrill, new(11, 0, 0, 0) },
            {ItemID.NebulaPickaxe, new(11, 0, 0, 0) },

            {ItemID.DayBreak, new(40, 5, 0, 0, ScalingGrade.A) },
            {ItemID.SolarEruption, new(40, 5, 0, 0, ScalingGrade.A) },
            {ItemID.SolarFlareDrill, new(11, 0, 0, 0) },
            {ItemID.SolarFlarePickaxe, new(11, 0, 0, 0) },

            {ItemID.VortexBeater, new(5, 40, 0, 0, dexterityScalingGrade: ScalingGrade.A) },
            {ItemID.Phantasm, new(5, 40, 0, 0, dexterityScalingGrade: ScalingGrade.A) },
            {ItemID.VortexDrill, new(11, 0, 0, 0) },
            {ItemID.VortexPickaxe, new(11, 0, 0, 0) },

            {ItemID.StardustCellStaff, new(3, 2, 0, 40, faithScalingGrade: ScalingGrade.A) },
            {ItemID.StardustDragonStaff, new(3, 2, 0, 40, faithScalingGrade: ScalingGrade.A) },
            {ItemID.StardustDrill, new(11, 0, 0, 0) },
            {ItemID.StardustPickaxe, new(11, 0, 0, 0) },

            {ItemID.NettleBurst, new(2, 2, 30, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.NightsEdge, new(13, 5, 0, 0, ScalingGrade.C, ScalingGrade.D) },
            {ItemID.FairyQueenMagicItem, new(2, 2, 35, 0, intelligenceScalingGrade: ScalingGrade.A) },
            {ItemID.NightmarePickaxe, new(4, 0, 0, 0) },
            {ItemID.NimbusRod, new(1, 1, 14, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.NorthPole, new(24, 9, 0, 0, strengthScalingGrade: ScalingGrade.B, dexterityScalingGrade: ScalingGrade.E) },
            {ItemID.ObsidianSwordfish, new(22, 5, 0, 0, ScalingGrade.C, ScalingGrade.D) },
            {ItemID.OnyxBlaster, new(6, 14, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.OpticStaff, new(2, 2, 0, 20, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.ZapinatorOrange, new(2, 2, 18, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.PainterPaintballGun, new(2, 8, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.PaladinsHammer, new(30, 5, 0, 0, ScalingGrade.A, ScalingGrade.E) },
            {ItemID.PalmWoodBow, new() },
            {ItemID.PalmWoodHammer, new() },
            {ItemID.PalmWoodSword, new() },
            {ItemID.PaperAirplaneA, new(0, 3, 0, 0, dexterityScalingGrade: ScalingGrade.S) },
            {ItemID.PaperAirplaneB, new(0, 3, 0, 0, dexterityScalingGrade: ScalingGrade.S) },
            {ItemID.PearlwoodBow, new() },
            {ItemID.PearlwoodHammer, new() },
            {ItemID.PewMaticHorn, new(2, 8, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.DD2PhoenixBow, new(3, 22, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.PhoenixBlaster, new(3, 14, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.PickaxeAxe, new(9, 0, 0, 0) },
            {ItemID.Picksaw, new(10, 0, 0, 0) },
            {ItemID.PiranhaGun, new(4, 26, 0, 0, dexterityScalingGrade: ScalingGrade.A) },
            {ItemID.PirateStaff, new(2, 2, 0, 22, faithScalingGrade: ScalingGrade.B) },
            {ItemID.PlatinumAxe, new(5, 4, 0, 0) },
            {ItemID.PlatinumBow, new(4, 6, 0, 0, dexterityScalingGrade: ScalingGrade.E) },
            {ItemID.PlatinumBroadsword, new(8, 2, 0, 0, ScalingGrade.E) },
            {ItemID.PlatinumHammer, new(9, 2, 0, 0, ScalingGrade.D) },
            {ItemID.PlatinumPickaxe, new(3, 0, 0, 0) },
            {ItemID.PlatinumShortsword, new(6, 2, 0, 0, ScalingGrade.E) },
            {ItemID.PoisonStaff, new(2, 2, 20, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.PoisonedKnife, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.A) },
            {ItemID.PossessedHatchet, new(26, 5, 0, 0, ScalingGrade.B, ScalingGrade.D) },
            {ItemID.ProximityMineLauncher, new(6, 24, 0, 0, dexterityScalingGrade: ScalingGrade.A) },
            {ItemID.PsychoKnife, new(22, 10, 0, 0, ScalingGrade.A, ScalingGrade.C) },
            {ItemID.PulseBow, new(4, 24, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.PurpleClubberfish, new(12, 5, 0, 0, ScalingGrade.A, ScalingGrade.D) },
            {ItemID.Pwnhammer, new(0, 0, 0, 0, ScalingGrade.B, ScalingGrade.E) },
            {ItemID.PygmyStaff, new(2, 2, 0, 26, faithScalingGrade: ScalingGrade.B) },
            {ItemID.QuadBarrelShotgun, new(3, 10, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.QueenSpiderStaff, new(1, 1, 0, 18, faithScalingGrade: ScalingGrade.D) },
            {ItemID.RainbowCrystalStaff, new(2, 2, 0, 46, faithScalingGrade: ScalingGrade.A) },
            {ItemID.RainbowGun, new(2, 2, 30, 0, intelligenceScalingGrade: ScalingGrade.B) },
            {ItemID.RainbowRod, new(2, 2, 21, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.Rally, new(6, 6, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.RavenStaff, new(2, 2, 0, 36, faithScalingGrade: ScalingGrade.A) },
            {ItemID.RazorbladeTyphoon, new(2, 2, 34, 0, intelligenceScalingGrade: ScalingGrade.B) },
            {ItemID.Razorpine, new(2, 2, 32, 0, intelligenceScalingGrade: ScalingGrade.B) },
            {ItemID.ReaverShark, new(10, 4, 0, 0, ScalingGrade.B, ScalingGrade.D) },
            {ItemID.RedRyder, new(2, 12, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.RedsYoyo, new(15, 15, 0, 0, ScalingGrade.C, ScalingGrade.C) },
            {ItemID.PrincessWeapon, new(2, 2, 30, 0, intelligenceScalingGrade: ScalingGrade.B) },
            {ItemID.Revolver, new(2, 12, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.RichMahoganyBow, new() },
            {ItemID.RichMahoganyHammer, new() },
            {ItemID.RichMahoganySword, new() },
            {ItemID.RocketLauncher, new(6, 25, 0, 0, dexterityScalingGrade: ScalingGrade.A) },
            {ItemID.Rockfish, new(11, 4, 0, 0, ScalingGrade.B, ScalingGrade.D) },
            {ItemID.RottenEgg, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.S) },
            {ItemID.RubyStaff, new(2, 2, 6, 0, intelligenceScalingGrade: ScalingGrade.E) },
            {ItemID.Ruler, new(6, 4, 0, 0, ScalingGrade.C, ScalingGrade.D) },
            {ItemID.SDMG, new(5, 45, 0, 0, dexterityScalingGrade: ScalingGrade.A) },
            {ItemID.Sandgun, new(2, 15, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.SanguineStaff, new(2, 2, 0, 17, faithScalingGrade: ScalingGrade.D) },
            {ItemID.SapphireStaff, new(2, 2, 6, 0, intelligenceScalingGrade: ScalingGrade.E) },
            {ItemID.SawtoothShark, new(8, 4, 0, 0, ScalingGrade.B, ScalingGrade.D) },
            {ItemID.ScarabBomb, new(dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.ScourgeoftheCorruptor, new(25, 8, 0, 0, ScalingGrade.B, ScalingGrade.C) },
            {ItemID.Seedler, new(24, 6, 0, 0, ScalingGrade.B, ScalingGrade.D) },
            {ItemID.BouncingShield, new(12, 12, 0, 0, ScalingGrade.C, ScalingGrade.C) },
            {ItemID.ShadewoodBow, new() },
            {ItemID.ShadewoodHammer, new() },
            {ItemID.ShadowJoustingLance, new(34, 6, 0, 0, ScalingGrade.S, ScalingGrade.D) },
            {ItemID.ShadowbeamStaff, new(2, 3, 28, 0, intelligenceScalingGrade: ScalingGrade.B) },
            {ItemID.ShadowFlameBow, new(2, 18, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.ShadowFlameHexDoll, new(2, 1, 18, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.ShadowFlameKnife, new(10, 8, 0, 0, ScalingGrade.C, ScalingGrade.C) },
            {ItemID.Shotgun, new(6, 12, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.Shroomerang, new(6, 6, 0, 0, ScalingGrade.E, ScalingGrade.E) },
            {ItemID.ShroomiteDiggingClaw, new(9, 0, 0, 0) },
            {ItemID.Shuriken, new(2, 4, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.Sickle, new(4, 2, 0, 0, ScalingGrade.E) },
            {ItemID.SilverAxe, new(4, 2, 0, 0) },
            {ItemID.SilverBow, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.E) },
            {ItemID.SilverBroadsword, new(5, 2, 0, 0, ScalingGrade.E) },
            {ItemID.SilverHammer, new(6, 2, 0, 0, ScalingGrade.D) },
            {ItemID.SilverPickaxe, new(2, 0, 0, 0) },
            {ItemID.SilverShortsword, new(4, 2, 0, 0, ScalingGrade.E) },
            {ItemID.MonkStaffT3, new(28, 8, 0, 0, ScalingGrade.B, ScalingGrade.C) },
            {ItemID.SkyFracture, new(2, 2, 20, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.SlapHand, new(21, 5, 0, 0, ScalingGrade.S, ScalingGrade.D) },
            {ItemID.SlimeStaff, new(1, 1, 0, 7, faithScalingGrade: ScalingGrade.E) },
            {ItemID.ThornWhip, new(2, 2, 0, 10, faithScalingGrade: ScalingGrade.D) },
            {ItemID.SniperRifle, new(8, 30, 0, 0, dexterityScalingGrade: ScalingGrade.S) },
            {ItemID.Snowball, new(dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.SnowballCannon, new(2, 8, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.SnowballLauncher, new() },
            {ItemID.SnowmanCannon, new(6, 30, 0, 0, dexterityScalingGrade: ScalingGrade.A) },
            {ItemID.SpaceGun, new(2, 2, 10, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.Spear, new(3, 2, 0, 0) },
            {ItemID.SpectreHamaxe, new(27, 5, 0, 0, ScalingGrade.B, ScalingGrade.C) },
            {ItemID.SpectrePickaxe, new(9, 0, 0, 0) },
            {ItemID.SpectreStaff, new(1, 1, 30, 0, intelligenceScalingGrade: ScalingGrade.B) },
            {ItemID.SpiderStaff, new(2, 2, 0, 20, faithScalingGrade: ScalingGrade.C) },
            {ItemID.SpikyBall, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.BoneWhip, new(2, 2, 0, 12, faithScalingGrade: ScalingGrade.D) },
            {ItemID.SpiritFlame, new(2, 2, 18, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.StaffofEarth, new(5, 2, 25, 0, intelligenceScalingGrade: ScalingGrade.A) },
            {ItemID.StaffofRegrowth, new(3, 2, 1, 0) },
            {ItemID.StaffoftheFrostHydra, new(2, 2, 0, 30, intelligenceScalingGrade: ScalingGrade.B) },
            {ItemID.StakeLauncher, new(5, 31, 0, 0, dexterityScalingGrade: ScalingGrade.A) },
            {ItemID.StarAnise, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.StarCannon, new(4, 14, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.StarWrath, new(40, 6, 4, 0, ScalingGrade.A, ScalingGrade.D, ScalingGrade.D, saturation: 60) },
            {ItemID.Starfury, new(9, 2, 4, 0, ScalingGrade.E, ScalingGrade.None, ScalingGrade.E) },
            {ItemID.PiercingStarlight, new(18, 20, 0, 0, ScalingGrade.C, ScalingGrade.A) },
            {ItemID.SparkleGuitar, new(3, 5, 32, 0, intelligenceScalingGrade: ScalingGrade.B) },
            {ItemID.StickyBomb, new(dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.DirtStickyBomb, new(dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.StickyDynamite, new(dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.StickyGrenade, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.ThunderSpear, new(6, 4, 0, 0, ScalingGrade.E) },
            {ItemID.StylistKilLaKillScissorsIWish, new(6, 4, 0, 0, ScalingGrade.E) },
            {ItemID.Stynger, new(5, 28, 0, 0, dexterityScalingGrade: ScalingGrade.A) },
            {ItemID.Sunfury, new(14, 5, 0, 0, ScalingGrade.B) },
            {ItemID.SuperStarCannon, new(5, 23, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.Swordfish, new(10, 5, 0, 0, ScalingGrade.B, ScalingGrade.D) },
            {ItemID.TacticalShotgun, new(5, 28, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.TempestStaff, new(2, 2, 0, 30, faithScalingGrade: ScalingGrade.B) },
            {ItemID.TendonBow, new(2, 8, 0, 0, dexterityScalingGrade: ScalingGrade.E) },
            {ItemID.TentacleSpike, new(9, 4, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.TerraBlade, new(25, 6, 4, 0, ScalingGrade.B, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.Terragrim, new(5, 8, 0, 0, ScalingGrade.E, ScalingGrade.D) },
            {ItemID.EmpressBlade, new(2, 2, 0, 34, faithScalingGrade: ScalingGrade.A) },
            {ItemID.Terrarian, new(25, 25, 0, 0, ScalingGrade.A, ScalingGrade.A, saturation: 30) },
            {ItemID.TheAxe, new(20, 8, 0, 0, ScalingGrade.B, ScalingGrade.D) },
            {ItemID.BeesKnees, new(2, 12, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.TheBreaker, new(12, 2, 0, 0, ScalingGrade.C) },
            {ItemID.TheEyeOfCthulhu, new(19, 19, 0, 0, ScalingGrade.A, ScalingGrade.A, saturation: 40) },
            {ItemID.TheHorsemansBlade, new(25, 8, 0, 0, ScalingGrade.B, ScalingGrade.D) },
            {ItemID.TheMeatball, new(10, 5, 0, 0, ScalingGrade.D) },
            {ItemID.TheRottedFork, new(3, 8, 0, 0, ScalingGrade.E, ScalingGrade.E) },
            {ItemID.TheUndertaker, new(2, 9, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.ThornChakram, new(7, 7, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.ThrowingKnife, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.ThunderStaff, new(2, 2, 7, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.TinAxe, new() },
            {ItemID.TinBow, new() },
            {ItemID.TinBroadsword, new() },
            {ItemID.TinHammer, new() },
            {ItemID.TinPickaxe, new() },
            {ItemID.TinShortsword, new() },
            {ItemID.BookStaff, new(2, 2, 20, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.TopazStaff, new(2, 2, 6, 0, intelligenceScalingGrade: ScalingGrade.E) },
            {ItemID.ToxicFlask, new(2, 2, 28, 0, intelligenceScalingGrade: ScalingGrade.A) },
            {ItemID.Toxikarp, new(2, 20, 0, 0, dexterityScalingGrade: ScalingGrade.D) },
            {ItemID.TragicUmbrella, new(6, 5, 0, 0, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.Trident, new(6, 4, 0, 0, ScalingGrade.E, ScalingGrade.E) },
            {ItemID.Trimarang, new(6, 6, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.TrueExcalibur, new(21, 8, 0, 0, ScalingGrade.B, ScalingGrade.D) },
            {ItemID.TrueNightsEdge, new(20, 5, 4, 0, ScalingGrade.B, ScalingGrade.D, ScalingGrade.E) },
            {ItemID.Tsunami, new(4, 26, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.TungstenAxe, new(4, 2, 0, 0) },
            {ItemID.TungstenBow, new(2, 5, 0, 0, dexterityScalingGrade: ScalingGrade.E) },
            {ItemID.TungstenBroadsword, new(5, 2, 0, 0, ScalingGrade.E) },
            {ItemID.TungstenHammer, new(6, 2, 0, 0, ScalingGrade.D) },
            {ItemID.TungstenPickaxe, new(2, 0, 0, 0) },
            {ItemID.TungstenShortsword, new(4, 2, 0, 0, ScalingGrade.E) },
            {ItemID.Umbrella, new(5, 4, 0, 0, ScalingGrade.E, ScalingGrade.E) },
            {ItemID.UnholyTrident, new(4, 2, 18, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.UnholyWater, new() },
            {ItemID.Uzi, new(2, 18, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.ValkyrieYoyo, new(15, 15, 0, 0, ScalingGrade.C, ScalingGrade.C, saturation: 80) },
            {ItemID.Valor, new(8, 8, 0, 0, ScalingGrade.D, ScalingGrade.D) },
            {ItemID.VampireFrogStaff, new(1, 1, 0, 10, faithScalingGrade: ScalingGrade.E) },
            {ItemID.VampireKnives, new(25, 10, 0, 0, ScalingGrade.A, ScalingGrade.D, saturation: 50) },
            {ItemID.VenomStaff, new(2, 2, 24, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.VenusMagnum, new(2, 27, 0, 0, dexterityScalingGrade: ScalingGrade.C) },
            {ItemID.Vilethorn, new(1, 1, 8, 0, intelligenceScalingGrade: ScalingGrade.E) },
            {ItemID.FieryGreatsword, new(12, 3, 0, 0, ScalingGrade.C, ScalingGrade.D) },
            {ItemID.WaffleIron, new(15, 10, 0, 0, ScalingGrade.S, ScalingGrade.D) },
            {ItemID.WandofFrosting, new() },
            {ItemID.WandofSparking, new(1, 1, 5, 0) },
            {ItemID.WarAxeoftheNight, new(10, 2, 0, 0, ScalingGrade.E) },
            {ItemID.WaspGun, new(2, 2, 28, 0, intelligenceScalingGrade: ScalingGrade.C) },
            {ItemID.WaterBolt, new(2, 2, 12, 0, intelligenceScalingGrade: ScalingGrade.D) },
            {ItemID.WeatherPain, new(1, 1, 10, 0, intelligenceScalingGrade: ScalingGrade.E) },
            {ItemID.WetBomb, new(dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.WoodenBoomerang, new() },
            {ItemID.WoodenBow, new() },
            {ItemID.WoodenHammer, new() },
            {ItemID.WoodenSword, new() },
            {ItemID.WoodYoyo, new() },
            {ItemID.XenoStaff, new(2, 2, 0, 30, faithScalingGrade: ScalingGrade.B) },
            {ItemID.Xenopopper, new(2, 30, 0, 0, dexterityScalingGrade: ScalingGrade.B) },
            {ItemID.Yelets, new(14, 14, 0, 0, ScalingGrade.C, ScalingGrade.C, saturation: 85) },
            {ItemID.Zenith, new(42, 10, 0, 0, ScalingGrade.A, ScalingGrade.C, saturation: 40) },
            {ItemID.ZombieArm, new(6, 4, 0, 0, ScalingGrade.E) }
        };

        public static Dictionary<int, WeaponParams> AllWeaponsParams
        {
            get { return allWeaponsParams; }
        }

        public static string ScalingGradeToString(ScalingGrade level) => level == ScalingGrade.None ? "-" : level.ToString();

        public static float GetScalingGradeModifier(ScalingGrade level)
        {
            return level switch
            {
                ScalingGrade.S => 0.85f,
                ScalingGrade.A => 0.65f,
                ScalingGrade.B => 0.45f,
                ScalingGrade.C => 0.35f,
                ScalingGrade.D => 0.25f,
                ScalingGrade.E => 0.15f,
                _ => 0f,
            };
        }

        public static DamageBonuses GetBonusDamage(Item item)
        {
            WeaponParams wp;
            if (!AllWeaponsParams.TryGetValue(item.type, out wp))
                return new();

            DarkSoulsPlayer dsPlayer = Main.LocalPlayer.GetModPlayer<DarkSoulsPlayer>();

            int bonusDamageByStrength = (int)(GetScalingGradeModifier(wp.StrengthScalingGrade) * StatFormulas.GetPotentialByStrength(dsPlayer.dsStrength) * wp.Saturation);
            int bonusDamageByDexterity = (int)(GetScalingGradeModifier(wp.DexterityScalingGrade) * StatFormulas.GetPotentialByDexterity(dsPlayer.dsDexterity) * wp.Saturation);
            int bonusDamageByIntelligence = (int)(GetScalingGradeModifier(wp.IntelligenceScalingGrade) * StatFormulas.GetPotentialByIntelligence(dsPlayer.dsIntelligence) * wp.Saturation);
            int bonusDamageByFaith = (int)(GetScalingGradeModifier(wp.FaithScalingGrade) * StatFormulas.GetPotentialByFaith(dsPlayer.dsFaith) * wp.Saturation);
            int totalBonusDamage = bonusDamageByStrength + bonusDamageByDexterity + bonusDamageByIntelligence + bonusDamageByFaith;
            return new(totalBonusDamage, bonusDamageByStrength, bonusDamageByDexterity, bonusDamageByIntelligence, bonusDamageByFaith);
        }
    }
}