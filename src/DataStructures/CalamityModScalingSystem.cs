using Terraria.ModLoader;

using DarkSouls.Utils;
using static DarkSouls.DataStructures.DarkSoulsScalingSystem;

namespace DarkSouls.DataStructures
{
    public static class CalamityModScalingSystem
    {
        public const float PostMoonLordSaturation = 60f;
        public const float DefaultCalamitySaturation = 90f;

        private static bool RegisterWeapon(string itemName, WeaponParams weaponParams, bool postMoonLordSaturation = false)
        {
            if (ModContent.TryFind<ModItem>("CalamityMod", itemName, out var item))
            {
                if (AllWeaponsParams.ContainsKey(item.Type))
                {
                    LoggingUtils.Error("CalamityModScalingSystem", $"Same key has already been added ({itemName})!");
                    return false;
                }

                if (postMoonLordSaturation)
                    weaponParams.Saturation = PostMoonLordSaturation;
                else if (weaponParams.Saturation == 100)
                    weaponParams.Saturation = DefaultCalamitySaturation;

                AllWeaponsParams.Add(item.Type, weaponParams);
                return true;
            }
            LoggingUtils.Error("CalamityModScalingSystem", $"Not found item by name ({itemName})!");
            return false;
        }

        public static void Initialize()
        {
            LoggingUtils.Info("CalamityModScalingSystem", "Initialize");

            #region Melee Weapons
            #region Pre-Hardmode
            // Swords
            RegisterWeapon("AcidwoodSword", new());
            RegisterWeapon("Basher", new(10, 2, 0, 0, ScalingGrade.E));
            RegisterWeapon("BrokenBiomeBlade", new(12, 4, 0, 0, ScalingGrade.E));
            RegisterWeapon("BurntSienna", new(8, 2, 0, 0));
            RegisterWeapon("DriftwoodSword", new());
            RegisterWeapon("Auger", new(10, 2, 0, 0, ScalingGrade.D));
            RegisterWeapon("GeliticBlade", new(13, 2, 0, 0, ScalingGrade.D));
            RegisterWeapon("MycelialClaws", new(7, 5, 0, 0, ScalingGrade.E, ScalingGrade.E));
            RegisterWeapon("PerfectDark", new(11, 2, 0, 0, ScalingGrade.D));
            RegisterWeapon("SeashineSword", new(6, 2, 0, 0));
            RegisterWeapon("TaintedBlade", new(12, 2, 0, 0, ScalingGrade.D));
            RegisterWeapon("TeardropCleaver", new(8, 2, 0, 0, ScalingGrade.E));
            RegisterWeapon("VeinBurster", new(12, 0, 0, 0, ScalingGrade.D));
            RegisterWeapon("WindBlade", new(10, 2, 0, 0, ScalingGrade.D));

            // Shortswords
            RegisterWeapon("SaharaSlicers", new(5, 3, 0, 0));

            // Yoyos
            RegisterWeapon("AirSpinner", new(6, 6, 0, 0, ScalingGrade.E, ScalingGrade.E));
            RegisterWeapon("Aorta", new(6, 6, 0, 0, ScalingGrade.E, ScalingGrade.E));
            RegisterWeapon("TheGodsGambit", new(7, 7, 0, 0, ScalingGrade.E, ScalingGrade.E));
            RegisterWeapon("Riptide", new(4, 4, 0, 0));
            RegisterWeapon("SmokingComet", new(6, 6, 0, 0, ScalingGrade.E, ScalingGrade.E));

            // Spears
            RegisterWeapon("AmidiasTrident", new(6, 4, 0, 0, ScalingGrade.E, ScalingGrade.E));
            RegisterWeapon("GoldplumeSpear", new(8, 4, 0, 0, ScalingGrade.D, ScalingGrade.E));
            RegisterWeapon("SausageMaker", new(8, 5, 0, 0, ScalingGrade.D, ScalingGrade.E));
            RegisterWeapon("YateveoBloom", new(6, 3, 0, 0, ScalingGrade.E));

            // Flails
            RegisterWeapon("BallOFugu", new(12, 3, 0, 0, ScalingGrade.D, ScalingGrade.E));
            RegisterWeapon("UrchinMace", new(6, 2, 0, 0, ScalingGrade.E));

            // Others
            RegisterWeapon("BladecrestOathsword", new(9, 2, 0, 0, ScalingGrade.D));
            RegisterWeapon("DepthCrusher", new(10, 2, 0, 0, ScalingGrade.D));
            RegisterWeapon("FracturedArk", new(10, 5, 0, 0, ScalingGrade.D, ScalingGrade.E));
            RegisterWeapon("MonstrousKnives", new(4, 2, 0, 0, ScalingGrade.C));
            RegisterWeapon("OldLordClaymore", new(11, 2, 0, 0, ScalingGrade.D));
            RegisterWeapon("WulfrumScrewdriver", new());

            // Tools
            // Multi-use Tools
            RegisterWeapon("AerialHamaxe", new(9, 2, 0, 0, ScalingGrade.D));
            RegisterWeapon("ReefclawHamaxe", new(5, 2, 0, 0, ScalingGrade.D));

            // Pickaxes
            RegisterWeapon("Gelpick", new(5, 0, 0, 0));
            RegisterWeapon("GreatbayPickaxe", new(4, 0, 0, 0));
            RegisterWeapon("SkyfringePickaxe", new(5, 0, 0, 0));
            RegisterWeapon("Spadefish", new());

            // Drils
            RegisterWeapon("MarniteObliterator", new(3, 0, 0, 0));
            RegisterWeapon("WulfrumDrill", new());

            // Axes
            RegisterWeapon("FellerofEvergreens", new(5, 2, 0, 0));

            // Hammers
            RegisterWeapon("AcidwoodHammer", new());
            RegisterWeapon("DriftwoodHammer", new());
            RegisterWeapon("MarniteDeconstructor", new(4, 2, 0, 0));

            // Mining
            RegisterWeapon("WulfrumDiggingTurtle", new());
            #endregion
            #region Hardmode
            // Swords
            RegisterWeapon("AbsoluteZero", new(24, 8, 0, 0, ScalingGrade.B, ScalingGrade.D));
            RegisterWeapon("AegisBlade", new(25, 10, 0, 0, ScalingGrade.B, ScalingGrade.D));
            RegisterWeapon("AnarchyBlade", new(25, 5, 0, 0, ScalingGrade.C));
            RegisterWeapon("AstralScythe", new(26, 8, 0, 0, ScalingGrade.C, ScalingGrade.E));
            RegisterWeapon("Avalanche", new(18, 2, 0, 0, ScalingGrade.D));
            RegisterWeapon("BalefulHarvester", new(32, 12, 0, 0, ScalingGrade.A, ScalingGrade.C));
            RegisterWeapon("TrueBiomeBlade", new(20, 5, 0, 0, ScalingGrade.C, ScalingGrade.E));
            RegisterWeapon("BlightedCleaver", new(22, 6, 0, 0, ScalingGrade.C, ScalingGrade.D));
            RegisterWeapon("Brimlash", new(20, 8, 0, 0, ScalingGrade.C, ScalingGrade.D));
            RegisterWeapon("BrimstoneSword", new(18, 6, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("BrinyBaron", new(28, 6, 0, 0, ScalingGrade.B, ScalingGrade.D));
            RegisterWeapon("Carnage", new(20, 5, 0, 0, ScalingGrade.B));
            RegisterWeapon("TrueCausticEdge", new(16, 4, 0, 0, ScalingGrade.C, ScalingGrade.E));
            RegisterWeapon("CelestialClaymore", new(18, 4, 0, 0, ScalingGrade.C, ScalingGrade.E));
            RegisterWeapon("CometQuasher", new(24, 6, 2, 0, ScalingGrade.B, ScalingGrade.D));
            RegisterWeapon("TheDarkMaster", new(16, 4, 0, 0, ScalingGrade.C, ScalingGrade.E));
            RegisterWeapon("DarklightGreatsword", new(18, 4, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("EntropicClaymore", new(30, 10, 0, 0, ScalingGrade.B, ScalingGrade.C));
            RegisterWeapon("EvilSmasher", new(24, 0, 0, 0, ScalingGrade.B));
            RegisterWeapon("ExaltedOathblade", new(26, 6, 0, 0, ScalingGrade.B, ScalingGrade.D));
            RegisterWeapon("FeralthornClaymore", new(22, 6, 0, 0, ScalingGrade.C, ScalingGrade.D));
            RegisterWeapon("FlarefrostBlade", new(16, 5, 0, 0, ScalingGrade.C, ScalingGrade.E));
            RegisterWeapon("Floodtide", new(28, 4, 0, 0, ScalingGrade.C, ScalingGrade.D));
            RegisterWeapon("ForbiddenOathblade", new(22, 5, 0, 0, ScalingGrade.C, ScalingGrade.D));
            RegisterWeapon("ForsakenSaber", new(20, 5, 0, 0, ScalingGrade.C, ScalingGrade.D));
            RegisterWeapon("GrandGuardian", new(35, 8, 0, 0, ScalingGrade.A, ScalingGrade.C));
            RegisterWeapon("Greentide", new(25, 8, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("HellfireFlamberge", new(25, 10, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("Hellkite", new(22, 6, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("MajesticGuard", new(20, 5, 0, 0, ScalingGrade.C, ScalingGrade.D));
            RegisterWeapon("MantisClaws", new(20, 12, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("MonolithSword", new(16, 3, 0, 0, ScalingGrade.C, ScalingGrade.E));
            RegisterWeapon("Roxcalibur", new(21, 2, 0, 0, ScalingGrade.B));
            RegisterWeapon("SoulHarvester", new(24, 8, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("StormRuler", new(30, 8, 2, 0, ScalingGrade.A, ScalingGrade.C, ScalingGrade.E));
            RegisterWeapon("TitanArm", new(36, 2, 0, 0, ScalingGrade.S));
            RegisterWeapon("OmegaBiomeBlade", new(31, 8, 0, 0, ScalingGrade.B, ScalingGrade.C));
            RegisterWeapon("UltimusCleaver", new(30, 5, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("Virulence", new(25, 8, 0, 0, ScalingGrade.C, ScalingGrade.C));

            // Shortswords
            RegisterWeapon("Lucrecia", new(28, 6, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("SubmarineShocker", new(15, 6, 0, 0, ScalingGrade.C, ScalingGrade.C));

            // Yoyos
            RegisterWeapon("FaultLine", new(18, 18, 0, 0, ScalingGrade.B, ScalingGrade.B, saturation: 80));
            RegisterWeapon("TheMicrowave", new(19, 19, 0, 0, ScalingGrade.A, ScalingGrade.A, saturation: 40));
            RegisterWeapon("Oblivion", new(15, 15, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("Pandemic", new(18, 18, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("Quagmire", new(15, 15, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("Shimmerspark", new(12, 12, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("SulphurousGrabber", new(11, 11, 0, 0, ScalingGrade.B, ScalingGrade.B));
            RegisterWeapon("YinYo", new(12, 12, 0, 0, ScalingGrade.C, ScalingGrade.C));

            // Spears
            RegisterWeapon("AstralPike", new(24, 10, 0, 0, ScalingGrade.C, ScalingGrade.D));
            RegisterWeapon("BotanicPiercer", new(24, 6, 0, 0, ScalingGrade.C, ScalingGrade.D));
            RegisterWeapon("Brimlance", new(17, 5, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("EarthenPike", new(14, 4, 0, 0, ScalingGrade.D, ScalingGrade.E));
            RegisterWeapon("GalvanizingGlaive", new(26, 10, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("HellionFlowerSpear", new(19, 8, 0, 0, ScalingGrade.C, ScalingGrade.D));
            RegisterWeapon("StarnightLance", new(16, 4, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("TenebreusTides", new(26, 8, 0, 0, ScalingGrade.B, ScalingGrade.C));
            RegisterWeapon("VulcaniteLance", new(26, 10, 0, 0, ScalingGrade.B, ScalingGrade.C));

            // Flails
            RegisterWeapon("ClamCrusher", new(20, 2, 0, 0, ScalingGrade.B));
            RegisterWeapon("Nebulash", new(30, 5, 0, 0, ScalingGrade.A));
            RegisterWeapon("Tumbleweed", new(28, 4, 0, 0, ScalingGrade.B));

            // Others
            RegisterWeapon("AbyssBlade", new(30, 4, 0, 0, ScalingGrade.B));
            RegisterWeapon("TrueArkoftheAncients", new(22, 8, 0, 0, ScalingGrade.B, ScalingGrade.D));
            RegisterWeapon("Bonebreaker", new(10, 8, 0, 0, ScalingGrade.B, ScalingGrade.B));
            RegisterWeapon("FallenPaladinsHammer", new(32, 5, 0, 0, ScalingGrade.A, ScalingGrade.E));
            RegisterWeapon("Omniblade", new(22, 12, 0, 0, ScalingGrade.B, ScalingGrade.B));
            RegisterWeapon("Pwnagehammer", new(24, 4, 0, 0, ScalingGrade.C, ScalingGrade.E));
            RegisterWeapon("StygianShield", new(30, 6, 0, 0, ScalingGrade.S, ScalingGrade.D));
            RegisterWeapon("TyphonsGreed", new(28, 5, 0, 0, ScalingGrade.B, ScalingGrade.D));

            // Tools
            // Multi-use Tools
            RegisterWeapon("AstralHamaxe", new(9, 5, 0, 0, ScalingGrade.C));
            RegisterWeapon("SeismicHampick", new(10, 5, 0, 0, ScalingGrade.C));

            // Pickaxes
            RegisterWeapon("AstralPickaxe", new(10, 0, 0, 0));
            RegisterWeapon("BeastialPickaxe", new(9, 0, 0, 0));
            RegisterWeapon("ShardlightPickaxe", new(9, 0, 0, 0));

            // Axes
            RegisterWeapon("AxeofPurity", new(15, 3, 0, 0, ScalingGrade.D));
            RegisterWeapon("BerserkerWaraxe", new(18, 4, 0, 0, ScalingGrade.C));
            RegisterWeapon("InfernaCutter", new(14, 4, 0, 0, ScalingGrade.C));
            RegisterWeapon("TectonicTruncator", new(16, 4, 0, 0, ScalingGrade.C));

            // Chainsaws
            RegisterWeapon("Respiteblock", new(26, 10, 0, 0, ScalingGrade.A, ScalingGrade.C));

            // Hammers
            RegisterWeapon("AbyssalWarhammer", new(18, 2, 0, 0, ScalingGrade.B, ScalingGrade.E));
            RegisterWeapon("HydraulicVoltCrasher", new(20, 4, 0, 0, ScalingGrade.B, ScalingGrade.D));
            RegisterWeapon("MonolithHammer", new(15, 3, 0, 0, strengthScalingGrade: ScalingGrade.A));
            #endregion
            #region Post-Moon Lord
            // Swords
            RegisterWeapon("Ataraxia", new(60, 15, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("DefiledGreatsword", new(48, 12, 0, 0, ScalingGrade.A, ScalingGrade.C), true);
            RegisterWeapon("DevilsDevastation", new(55, 15, 0, 0, ScalingGrade.A, ScalingGrade.C), true);
            RegisterWeapon("DraconicDestruction", new(65, 15, 0, 0, ScalingGrade.S, ScalingGrade.B), true);
            RegisterWeapon("Earth", new(60, 10, 10, 0, ScalingGrade.S, ScalingGrade.A, ScalingGrade.C), true);
            RegisterWeapon("EssenceFlayer", new(53, 15, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("Excelsus", new(53, 15, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("GaelsGreatsword", new(65, 15, 0, 0, ScalingGrade.S, ScalingGrade.A), true);
            RegisterWeapon("GalactusBlade", new(48, 12, 0, 0, ScalingGrade.A, ScalingGrade.C), true);
            RegisterWeapon("GrandDad", new(54, 12, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("GreatswordofJudgement", new(40, 8, 2, 0, ScalingGrade.A, ScalingGrade.C), true);
            RegisterWeapon("HolyCollider", new(50, 12, 0, 0, ScalingGrade.A, ScalingGrade.C), true);
            RegisterWeapon("IridescentExcalibur", new(65, 15, 0, 0, ScalingGrade.S, ScalingGrade.A), true);
            RegisterWeapon("TheLastMourning", new(52, 12, 0, 0, ScalingGrade.A, ScalingGrade.C), true);
            RegisterWeapon("LifehuntScythe", new(48, 12, 0, 0, ScalingGrade.A, ScalingGrade.C), true);
            RegisterWeapon("LionHeart", new(50, 10, 5, 0, ScalingGrade.A, ScalingGrade.C, ScalingGrade.D), true);
            RegisterWeapon("MirrorBlade", new(48, 15, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("TheMutilator", new(55, 10, 0, 0, ScalingGrade.A, ScalingGrade.C), true);
            RegisterWeapon("RedSun", new(62, 18, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("SolsticeClaymore", new(42, 8, 0, 0, ScalingGrade.A, ScalingGrade.C), true);
            RegisterWeapon("StellarStriker", new(38, 8, 4, 0, ScalingGrade.A, ScalingGrade.C, ScalingGrade.D), true);
            RegisterWeapon("Swordsplosion", new(44, 12, 0, 0, ScalingGrade.A, ScalingGrade.C), true);
            RegisterWeapon("Terratomere", new(50, 12, 0, 0, ScalingGrade.S, ScalingGrade.B), true);
            RegisterWeapon("TerrorBlade", new(54, 12, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("VoidEdge", new(55, 10, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            
            // Shortswords
            RegisterWeapon("CosmicShiv", new(54, 16, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("ElementalShiv", new(38, 12, 0, 0, ScalingGrade.A, ScalingGrade.C), true);
            RegisterWeapon("GalileoGladius", new(50, 15, 0, 0, ScalingGrade.A, ScalingGrade.B), true);

            // Yoyos
            RegisterWeapon("BurningRevelation", new(30, 30, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("Lacerator", new(32, 32, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("TheObliterator", new(35, 35, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("TheOracle", new(37, 37, 0, 0, ScalingGrade.S, ScalingGrade.S, saturation: 20f));
            RegisterWeapon("Ozzathoth", new(40, 40, 0, 0, ScalingGrade.S, ScalingGrade.S, saturation: 20f));

            // Spears
            RegisterWeapon("BansheeHook", new(50, 15, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("ElementalLance", new(38, 12, 0, 0, ScalingGrade.A, ScalingGrade.C), true);
            RegisterWeapon("GildedProboscis", new(42, 12, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("Nadir", new(55, 18, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("SkytideDragoon", new(48, 14, 0, 0, ScalingGrade.A, ScalingGrade.B), true);

            // Boomerangs
            RegisterWeapon("SeekingScorcher", new(42, 18, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("TriactisTruePaladinianMageHammerofMightMelee", new(70, 12, 0, 0, ScalingGrade.S, ScalingGrade.A), true);

            // Flails
            RegisterWeapon("CosmicDischarge", new(58, 12, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("CrescentMoon", new(55, 10, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("DragonPow", new(68, 8, 0, 0, ScalingGrade.S, ScalingGrade.B), true);
            RegisterWeapon("Mourningstar", new(52, 8, 0, 0, ScalingGrade.A, ScalingGrade.C), true);
            RegisterWeapon("PulseDragon", new(48, 8, 4, 0, ScalingGrade.A, ScalingGrade.C, ScalingGrade.C), true);
            RegisterWeapon("RemsRevenge", new(42, 5, 0, 0, ScalingGrade.B, ScalingGrade.C), true);
            RegisterWeapon("SpineOfThanatos", new(60, 10, 5, 0, ScalingGrade.S, ScalingGrade.B, ScalingGrade.B), true);

            // Others
            RegisterWeapon("ArkoftheCosmos", new(65, 10, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("ArkoftheElements", new(44, 6, 0, 0, ScalingGrade.A, ScalingGrade.C), true);
            RegisterWeapon("TheBurningSky", new(58, 12, 5, 0, ScalingGrade.A, ScalingGrade.B, ScalingGrade.C), true);
            RegisterWeapon("DeathsAscension", new(53, 12, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("DevilsSunrise", new(50, 15, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("DragonRage", new(64, 20, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("EmpyreanKnives", new(56, 14, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("Exoblade", new(65, 12, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("FourSeasonsGalaxia", new(54, 15, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("GalaxySmasher", new(60, 10, 0, 0, ScalingGrade.S, ScalingGrade.B), true);
            RegisterWeapon("IllustriousKnives", new(60, 20, 0, 0, ScalingGrade.S, ScalingGrade.S), true);
            RegisterWeapon("InsidiousImpaler", new(58, 8, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("Murasama", new(66, 6, 0, 0, ScalingGrade.S, ScalingGrade.A), true);
            RegisterWeapon("NeptunesBounty", new(53, 12, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("Phaseslayer", new(54, 16, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("PhosphorescentGauntlet", new(60, 5, 0, 0, ScalingGrade.S, saturation: 500f));
            RegisterWeapon("ScourgeoftheCosmos", new(58, 12, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("StellarContempt", new(45, 5, 0, 0, ScalingGrade.A), true);
            RegisterWeapon("StreamGouge", new(56, 14, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("Violence", new(60, 18, 0, 0, ScalingGrade.S, ScalingGrade.A), true);

            // Tools
            // Multi-use Tools
            RegisterWeapon("Grax", new(45, 8, 0, 0, ScalingGrade.S, ScalingGrade.C), true);

            // Pickaxes
            RegisterWeapon("BlossomPickaxe", new(50, 0, 0, 0));
            RegisterWeapon("CrystylCrusher", new(60, 0, 0, 0));
            RegisterWeapon("GenesisPickaxe", new(36, 0, 0, 0));

            // Chainsaws
            RegisterWeapon("PhotonRipper", new(66, 12, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            #endregion
            #endregion

            #region Ranged Weapons
            #region Pre-Hardmode
            // Bows
            RegisterWeapon("AcidwoodBow", new());
            RegisterWeapon("Barinade", new(2, 5, 0, 0));
            RegisterWeapon("DriftwoodBow", new());
            RegisterWeapon("Galeforce", new(2, 9, 0, 0, dexterityScalingGrade: ScalingGrade.E));
            RegisterWeapon("Goobow", new(2, 13, 0, 0, dexterityScalingGrade: ScalingGrade.D));
            RegisterWeapon("LunarianBow", new(2, 14, 0, 0, dexterityScalingGrade: ScalingGrade.D));
            RegisterWeapon("Shellshooter", new(2, 9, 0, 0, dexterityScalingGrade: ScalingGrade.E));
            RegisterWeapon("Toxibow", new(2, 8, 0, 0, dexterityScalingGrade: ScalingGrade.E));

            // Guns
            RegisterWeapon("AquashardShotgun", new(2, 10, 0, 0, dexterityScalingGrade: ScalingGrade.E));
            RegisterWeapon("Archerfish", new(2, 12, 0, 0, dexterityScalingGrade: ScalingGrade.D));
            RegisterWeapon("BulletFilledShotgun", new(2, 11, 0, 0, dexterityScalingGrade: ScalingGrade.D));
            RegisterWeapon("CrackshotColt", new(1, 8, 0, 0, dexterityScalingGrade: ScalingGrade.E));
            RegisterWeapon("Eviscerator", new(3, 10, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("Fungicide", new(1, 11, 0, 0, dexterityScalingGrade: ScalingGrade.E));
            RegisterWeapon("GunkShot", new(2, 12, 0, 0, dexterityScalingGrade: ScalingGrade.D));
            RegisterWeapon("M1Garand", new(2, 13, 0, 0, dexterityScalingGrade: ScalingGrade.C));

            // Flamethrowers
            RegisterWeapon("DragoonDrizzlefish", new(1, 11, 0, 0, dexterityScalingGrade: ScalingGrade.E));
            RegisterWeapon("OverloadedBlaster", new(2, 13, 0, 0, dexterityScalingGrade: ScalingGrade.D));
            RegisterWeapon("Shadethrower", new(2, 12, 0, 0, dexterityScalingGrade: ScalingGrade.D));
            RegisterWeapon("SparkSpreader", new(2, 6, 0, 0, dexterityScalingGrade: ScalingGrade.E));

            // Others
            RegisterWeapon("FirestormCannon", new(2, 13, 0, 0, dexterityScalingGrade: ScalingGrade.D));
            RegisterWeapon("FlurrystormCannon", new(2, 10, 0, 0, dexterityScalingGrade: ScalingGrade.E));
            RegisterWeapon("MagnaCannon", new(2, 13, 0, 0, dexterityScalingGrade: ScalingGrade.D));
            RegisterWeapon("OpalStriker", new(2, 13, 0, 0, dexterityScalingGrade: ScalingGrade.D));
            RegisterWeapon("Pumpler", new(2, 8, 0, 0, dexterityScalingGrade: ScalingGrade.D));
            RegisterWeapon("ReedBlowgun", new(2, 6, 0, 0));
            RegisterWeapon("Taser", new(2, 10, 0, 0, dexterityScalingGrade: ScalingGrade.E));
            RegisterWeapon("SlagfireDouser", new(2, 2, 0, 0, dexterityScalingGrade: ScalingGrade.D));
            RegisterWeapon("StormSurge", new(2, 7, 0, 0, dexterityScalingGrade: ScalingGrade.E));
            RegisterWeapon("WulfrumBlunderbuss", new());
            #endregion
            #region Hardmode
            // Bows
            RegisterWeapon("TheBallista", new(3, 28, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("Barinautical", new(2, 20, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("BlossomFlux", new(3, 27, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("BrimstoneFury", new(2, 23, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("ContinentalGreatbow", new(3, 31, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("CorrodedCaustibow", new(2, 18, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("DarkechoGreatbow", new(2, 18, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("FlarewingBow", new(2, 17, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("HoarfrostBow", new(2, 18, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("Malevolence", new(2, 32, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("MarksmanBow", new(3, 27, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("MonolithBow", new(2, 16, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("VernalBolter", new(2, 28, 0, 0, dexterityScalingGrade: ScalingGrade.C));

            // Repeaters
            RegisterWeapon("Arbalest", new(2, 23, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("BladedgeRailbow", new(2, 26, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("EternalBlizzard", new(2, 31, 0, 0, dexterityScalingGrade: ScalingGrade.C));

            // Guns
            RegisterWeapon("Animosity", new(2, 26, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("Arietes41", new(2, 28, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("AstralBlaster", new(2, 38, 0, 0, dexterityScalingGrade: ScalingGrade.B));
            RegisterWeapon("ClamorRifle", new(2, 18, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("ConferenceCall", new(3, 37, 0, 0, dexterityScalingGrade: ScalingGrade.A));
            RegisterWeapon("DeepcoreGK2", new(4, 24, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("FrostbiteBlaster", new(2, 19, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("Hellborn", new(2, 20, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("Helstorm", new(2, 32, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("Hydra", new(3, 27, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("Leviatitan", new(2, 30, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("Megalodon", new(4, 28, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("MidasPrime", new(2, 18, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("Needler", new(2, 17, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("NitroExpressRifle", new(3, 20, 0, 0, dexterityScalingGrade: ScalingGrade.B));
            RegisterWeapon("P90", new(2, 19, 0, 0, dexterityScalingGrade: ScalingGrade.D));
            RegisterWeapon("PestilentDefiler", new(2, 31, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("PlagueTaintedSMG", new(4, 32, 0, 0, dexterityScalingGrade: ScalingGrade.B));
            RegisterWeapon("RealmRavager", new(2, 33, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("Shroomer", new(5, 40, 0, 0, dexterityScalingGrade: ScalingGrade.S, saturation: 200f));
            RegisterWeapon("SlagMagnum", new(2, 16, 0, 0, dexterityScalingGrade: ScalingGrade.D));
            RegisterWeapon("ThermoclineBlaster", new(2, 17, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("Vortexpopper", new(3, 40, 0, 0, dexterityScalingGrade: ScalingGrade.B));

            // Launchers
            RegisterWeapon("FlakKraken", new(4, 29, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("FlakToxicannon", new(2, 19, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("TheHive", new(3, 30, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("MineralMortar", new(2, 22, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("Scorpio", new(5, 40, 0, 0, dexterityScalingGrade: ScalingGrade.B));

            // Flamethrowers
            RegisterWeapon("AuroraBlazer", new(4, 32, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("BlightSpewer", new(2, 33, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("DeadSunsWind", new(4, 38, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("HavocsBreath", new(2, 27, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("Meowthrower", new(2, 17, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("WildfireBloom", new(3, 29, 0, 0, dexterityScalingGrade: ScalingGrade.C));

            // Others
            RegisterWeapon("AdamantiteParticleAccelerator", new(4, 20, 0, 0, dexterityScalingGrade: ScalingGrade.B));
            RegisterWeapon("ArcNovaDiffuser", new(3, 30, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("BarracudaGun", new(3, 28, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("Buzzkill", new(2, 25, 0, 0, dexterityScalingGrade: ScalingGrade.B));
            RegisterWeapon("GaussRifle", new(3, 30, 2, 0, dexterityScalingGrade: ScalingGrade.B, intelligenceScalingGrade: ScalingGrade.D));
            RegisterWeapon("MatterModulator", new(2, 23, 2, 0, dexterityScalingGrade: ScalingGrade.C, intelligenceScalingGrade: ScalingGrade.D));
            RegisterWeapon("NullificationPistol", new(2, 30, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("PolarisParrotfish", new(2, 18, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("SandstormGun", new(3, 28, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("SeasSearing", new(2, 21, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("SpectralstormCannon", new(3, 40, 0, 0, dexterityScalingGrade: ScalingGrade.B));
            RegisterWeapon("SpeedBlaster", new(2, 19, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("StarSputter", new(2, 38, 0, 0, dexterityScalingGrade: ScalingGrade.B)); ;
            RegisterWeapon("StellarCannon", new(2, 33, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            RegisterWeapon("TitaniumRailgun", new(2, 24, 0, 0, dexterityScalingGrade: ScalingGrade.C));
            #endregion
            #region Post-Moon Lord
            // Bows
            RegisterWeapon("Alluvion", new(3, 66, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("ArterialAssault", new(4, 60, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("AstrealDefeat", new(3, 45, 0, 0, dexterityScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("ClockworkBow", new(4, 46, 0, 0, dexterityScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("Contagion", new(5, 75, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("DaemonsFlame", new(4, 61, 0, 0, dexterityScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("Deathwind", new(4, 65, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Drataliornus", new(4, 70, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("HeavenlyGale", new(5, 71, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("TheMaelstrom", new(4, 60, 0, 0, dexterityScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("Monsoon", new(3, 62, 0, 0, dexterityScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("NettlevineGreatbow", new(3, 57, 0, 0, dexterityScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("Phangasm", new(3, 67, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("PlanetaryAnnihilation", new(4, 47, 0, 0, dexterityScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("TheStorm", new(3, 59, 0, 0, dexterityScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("TelluricGlare", new(3, 57, 0, 0, dexterityScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("Ultima", new(4, 65, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);

            // Repeaters
            RegisterWeapon("Condemnation", new(5, 78, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);

            // Guns
            RegisterWeapon("AcesHigh", new(2, 72, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("AngelicShotgun", new(3, 57, 0, 0, dexterityScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("AntiMaterielRifle", new(6, 65, 0, 0, dexterityScalingGrade: ScalingGrade.S, saturation: 300f), true);
            RegisterWeapon("Auralis", new(5, 55, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("CorinthPrime", new(3, 60, 0, 0, dexterityScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("FetidEmesis", new(3, 62, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("GoldenEagle", new(2, 51, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("HalibutCannon", new(5, 71, 0, 0, dexterityScalingGrade: ScalingGrade.S), true);
            RegisterWeapon("Infinity", new(4, 67, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("TheJailor", new(3, 72, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Karasawa", new(4, 68, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Kingsbane", new(5, 71, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("OnyxChainBlaster", new(4, 47, 0, 0, dexterityScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("Onyxia", new(5, 67, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("PearlGod", new(3, 63, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("PridefulHuntersPlanarRipper", new(2, 47, 0, 0, dexterityScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("RubicoPrime", new(3, 68, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("SDFMG", new(3, 69, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Seadragon", new(3, 60, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("TheSevensStriker", new(4, 56, 0, 3, dexterityScalingGrade: ScalingGrade.A, faithScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("Shredder", new(3, 48, 0, 0, dexterityScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("SomaPrime", new(4, 76, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Spyker", new(3, 59, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("SurgeDriver", new(6, 70, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Svantechnical", new(3, 77, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("TyrannysEnd", new(5, 70, 0, 0, dexterityScalingGrade: ScalingGrade.S, saturation: 500f), true);
            RegisterWeapon("UniversalGenesis", new(5, 67, 0, 0, dexterityScalingGrade: ScalingGrade.S), true);
            RegisterWeapon("Voidragon", new(4, 76, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);

            // Launchers
            RegisterWeapon("BlissfulBombardier", new(2, 58, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("ChickenCannon", new(3, 72, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("HandheldTank", new(4, 58, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("MagnomalyCannon", new(4, 72, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("ScorchedEarth", new(4, 67, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("ThePack", new(5, 65, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);

            // Flamethrowers
            RegisterWeapon("BloodBoiler", new(2, 62, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("CleansingBlaze", new(3, 70, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("DragonsBreath", new(4, 71, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("ElementalEruption", new(3, 46, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("HalleysInferno", new(3, 60, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Photoviscerator", new(5, 70, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("PristineFury", new(2, 59, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);

            // Others
            RegisterWeapon("TheAnomalysNanogun", new(5, 62, 4, 0, dexterityScalingGrade: ScalingGrade.A, intelligenceScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("FreedomStar", new(2, 56, 3, 0, dexterityScalingGrade: ScalingGrade.A, intelligenceScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("HeavyLaserRifle", new(2, 56, 3, 0, dexterityScalingGrade: ScalingGrade.A, intelligenceScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("MolecularManipulator", new(2, 61, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Norfleet", new(5, 65, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("PulseRifle", new(3, 69, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("SepticSkewer", new(2, 63, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Starfleet", new(4, 62, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Starmada", new(5, 72, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Starmageddon", new(5, 67, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("SulphuricAcidCannon", new(3, 63, 0, 0, dexterityScalingGrade: ScalingGrade.S));
            RegisterWeapon("SuperradiantSlaughterer", new(3, 45, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("TauCannon", new(5, 60, 0, 0, dexterityScalingGrade: ScalingGrade.A), true);
            #endregion
            #endregion

            #region Magic Weapons
            #region Pre-Hardmode
            // Wands
            RegisterWeapon("AquamarineStaff", new(0, 0, 7, 0));
            RegisterWeapon("BloodBath", new(2, 0, 10, 0, intelligenceScalingGrade: ScalingGrade.E));
            RegisterWeapon("HellwingStaff", new(2, 0, 13, 0, intelligenceScalingGrade: ScalingGrade.D));
            RegisterWeapon("HyphaeRod", new(1, 0, 11, 0, intelligenceScalingGrade: ScalingGrade.E));
            RegisterWeapon("IcicleStaff", new());
            RegisterWeapon("ManaRose", new(1, 0, 7, 0));
            RegisterWeapon("NightsRay", new(2, 0, 14, 0, intelligenceScalingGrade: ScalingGrade.D));
            RegisterWeapon("ParasiticSceptor", new(1, 0, 8, 0, intelligenceScalingGrade: ScalingGrade.E));
            RegisterWeapon("PlasmaRod", new(1, 0, 8, 0, intelligenceScalingGrade: ScalingGrade.E));
            RegisterWeapon("SandstreamScepter", new(0, 0, 7, 0));
            RegisterWeapon("ShaderainStaff", new(2, 0, 10, 0, intelligenceScalingGrade: ScalingGrade.E));
            RegisterWeapon("SkyGlaze", new(1, 0, 7, 0, intelligenceScalingGrade: ScalingGrade.E));

            // Magic Guns
            RegisterWeapon("AbyssShocker", new(2, 0, 13, 0, intelligenceScalingGrade: ScalingGrade.D));
            RegisterWeapon("AcidGun", new(1, 0, 9, 0, intelligenceScalingGrade: ScalingGrade.E));
            RegisterWeapon("PulsePistol", new(1, 0, 11, 0, intelligenceScalingGrade: ScalingGrade.D));

            // Spell Tomes
            RegisterWeapon("AbyssalTome", new(1, 0, 13, 0, intelligenceScalingGrade: ScalingGrade.D));
            RegisterWeapon("CoralSpout", new(0, 0, 8, 0, intelligenceScalingGrade: ScalingGrade.E));
            RegisterWeapon("EldritchTome", new(1, 0, 13, 0, intelligenceScalingGrade: ScalingGrade.D));
            RegisterWeapon("FlareBolt", new(1, 0, 14, 0, intelligenceScalingGrade: ScalingGrade.D));
            RegisterWeapon("FrostBolt", new());
            RegisterWeapon("Tradewinds", new(1, 0, 11, 0, intelligenceScalingGrade: ScalingGrade.D));
            RegisterWeapon("VeeringWind", new(1, 0, 7, 0, intelligenceScalingGrade: ScalingGrade.E));
            RegisterWeapon("Waywasher", new(1, 0, 7, 0, intelligenceScalingGrade: ScalingGrade.E));

            // Others
            RegisterWeapon("BlackAnurian", new(2, 0, 13, 0, intelligenceScalingGrade: ScalingGrade.D));
            RegisterWeapon("TheCauldron", new(2, 0, 14, 0, intelligenceScalingGrade: ScalingGrade.D));
            RegisterWeapon("SparklingEmpress", new(0, 0, 8, 0, intelligenceScalingGrade: ScalingGrade.E));
            RegisterWeapon("WulfrumProsthesis", new());
            #endregion
            #region Hardmode
            // Wands
            RegisterWeapon("AlulaAustralis", new(2, 0, 33, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("ArchAmaryllis", new(2, 0, 40, 0, intelligenceScalingGrade: ScalingGrade.A));
            RegisterWeapon("ArtAttack", new(2, 0, 25, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("AstralStaff", new(2, 0, 38, 0, intelligenceScalingGrade: ScalingGrade.B));
            RegisterWeapon("AstralachneaStaff", new(2, 0, 40, 0, intelligenceScalingGrade: ScalingGrade.B));
            RegisterWeapon("Atlantis", new(2, 0, 31, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("BrimroseStaff", new(2, 0, 22, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("Downpour", new(2, 0, 20, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("GleamingMagnolia", new(2, 0, 26, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("GloriousEnd", new(2, 0, 22, 0, intelligenceScalingGrade: ScalingGrade.B));
            RegisterWeapon("Hematemesis", new(2, 0, 33, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("IcicleTrident", new(2, 0, 19, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("InfernalRift", new(2, 0, 32, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("Keelhaul", new(2, 0, 31, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("Miasma", new(2, 0, 18, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("Photosynthesis", new(2, 0, 27, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("PlagueStaff", new(2, 0, 31, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("ShiftingSands", new(2, 0, 29, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("SnowstormStaff", new(2, 0, 18, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("UndinesRetribution", new(2, 0, 32, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("ValkyrieRay", new(2, 0, 25, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("Vesuvius", new(2, 0, 34, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("WyvernsCall", new(2, 0, 21, 0, intelligenceScalingGrade: ScalingGrade.C));

            // Magic Guns
            RegisterWeapon("Cryophobia", new(2, 0, 25, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("GatlingLaser", new(2, 0, 31, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("GaussPistol", new(2, 0, 22, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("IonBlaster", new(2, 0, 24, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("NanoPurge", new(2, 0, 40, 0, intelligenceScalingGrade: ScalingGrade.B));
            RegisterWeapon("SHPC", new(2, 0, 24, 0, intelligenceScalingGrade: ScalingGrade.A));
            RegisterWeapon("TheSwarmer", new(2, 0, 40, 0, intelligenceScalingGrade: ScalingGrade.B));
            RegisterWeapon("Wingman", new(2, 0, 34, 0, intelligenceScalingGrade: ScalingGrade.C));

            // Spell Tomes
            RegisterWeapon("BurningSea", new(2, 0, 23, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("DeathValleyDuster", new(2, 0, 24, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("EvergladeSpray", new(2, 0, 24, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("ForbiddenSun", new(2, 0, 32, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("FrigidflashBolt", new(2, 0, 19, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("LashesofChaos", new(2, 0, 26, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("Poseidon", new(2, 0, 18, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("PrimordialEarth", new(2, 0, 30, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("RelicofRuin", new(2, 0, 25, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("SeethingDischarge", new(2, 0, 20, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("Serpentine", new(2, 0, 16, 0, intelligenceScalingGrade: ScalingGrade.D));
            RegisterWeapon("ShadecrystalBarrage", new(2, 0, 20, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("SlitheringEels", new(2, 0, 17, 0, intelligenceScalingGrade: ScalingGrade.D));
            RegisterWeapon("StarShower", new(2, 0, 39, 0, intelligenceScalingGrade: ScalingGrade.B));
            RegisterWeapon("TearsofHeaven", new(2, 0, 28, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("TomeofFates", new(2, 0, 40, 0, intelligenceScalingGrade: ScalingGrade.B));
            RegisterWeapon("WintersFury", new(2, 0, 31, 0, intelligenceScalingGrade: ScalingGrade.C));

            // Others
            RegisterWeapon("AnahitasArpeggio", new(2, 0, 30, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("ArcticBearPaw", new(2, 0, 25, 0, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("BelchingSaxophone", new(2, 0, 21, intelligenceScalingGrade: ScalingGrade.C));
            RegisterWeapon("ClothiersWrath", new(2, 0, 17, 0, intelligenceScalingGrade: ScalingGrade.D));
            RegisterWeapon("CosmicRainbow", new(2, 0, 42, 0, intelligenceScalingGrade: ScalingGrade.B));
            RegisterWeapon("HadalUrn", new(2, 0, 33, 0, intelligenceScalingGrade: ScalingGrade.C));
            #endregion
            #region Post-Moon Lord
            // Wands
            RegisterWeapon("ClamorNoctus", new(3, 0, 62, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("DeathhailStaff", new(3, 0, 66, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("EidolonStaff", new(3, 0, 61, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("ElementalRay", new(3, 0, 45, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("FatesReveal", new(3, 0, 63, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("HeliumFlash", new(4, 0, 70, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("IceBarrage", new(3, 0, 69, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("MagneticMeltdown", new(3, 0, 62, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Mistlestorm", new(3, 0, 60, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("NebulousCataclysm", new(4, 0, 66, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("PhantasmalFury", new(3, 0, 59, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("PhoenixFlameBarrage", new(4, 0, 70, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("ThePrince", new(3, 0, 57, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("SanguineFlare", new(3, 0, 60, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("ShadowboltStaff", new(3, 0, 62, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("SoulPiercer", new(3, 0, 67, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("StaffofBlushie", new(3, 0, 77, 0, intelligenceScalingGrade: ScalingGrade.S), true);
            RegisterWeapon("Sylvestaff", new(4, 0, 76, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Teslastaff", new(2, 0, 61, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("ThornBlossom", new(3, 0, 58, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("UltraLiquidator", new(3, 0, 45, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Vehemence", new(4, 0, 73, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("VenusianTrident", new(3, 0, 62, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Viscera", new(3, 0, 62, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("VitriolicViper", new(3, 0, 62, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("VividClarity", new(5, 0, 72, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("VoidVortex", new(4, 0, 70, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("VoltaicClimax", new(3, 0, 67, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("TheWand", new(2, 0, 54, 0, intelligenceScalingGrade: ScalingGrade.S), true);

            // Magic Guns
            RegisterWeapon("AethersWhisper", new(5, 0, 58, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("AetherfluxCannon", new(4, 0, 70, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("ApoctosisArray", new(4, 0, 44, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Effervescence", new(3, 0, 45, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("EidolicWail", new(3, 0, 62, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Genesis", new(4, 0, 46, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Omicron", new(4, 0, 67, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("PlasmaCaster", new(3, 0, 57, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("PlasmaRifle", new(3, 0, 58, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("PurgeGuzzler", new(3, 0, 57, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("RainbowPartyCannon", new(5, 0, 76, 0, intelligenceScalingGrade: ScalingGrade.S), true);
            RegisterWeapon("TeslaCannon", new(4, 0, 69, 0, intelligenceScalingGrade: ScalingGrade.S), true);
            RegisterWeapon("Thunderstorm", new(3, 0, 60, 0, intelligenceScalingGrade: ScalingGrade.A), true);

            // Spell Tomes
            RegisterWeapon("Apotheosis", new(2, 0, 70, 0, intelligenceScalingGrade: ScalingGrade.S), true);
            RegisterWeapon("AuguroftheElements", new(2, 0, 46, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Biofusillade", new(2, 0, 59, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("TheDanceofLight", new(2, 0, 78, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Eternity", new(2, 0, 78, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("EventHorizon", new(2, 0, 70, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Heresy", new(2, 0, 75, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("LightGodsBrilliance", new(2, 0, 70, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("NuclearFury", new(2, 0, 47, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("PrimordialAncient", new(2, 0, 70, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Rancor", new(3, 0, 74, 0, intelligenceScalingGrade: ScalingGrade.S), true);
            RegisterWeapon("RecitationoftheBeast", new(2, 0, 69, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("RougeSlash", new(2, 0, 53, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("SubsumingVortex", new(4, 0, 74, 0, intelligenceScalingGrade: ScalingGrade.A), true);

            // Others
            RegisterWeapon("ChronomancersScythe", new(4, 0, 44, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("DarkSpark", new(3, 0, 63, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("FaceMelter", new(2, 2, 67, 0, dexterityScalingGrade: ScalingGrade.A, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("GhastlyVisage", new(2, 0, 63, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("GruesomeEminence", new(2, 0, 74, 0, intelligenceScalingGrade: ScalingGrade.S), true);
            RegisterWeapon("MadAlchemistsCocktailGlove", new(2, 0, 52, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("TacticiansTrumpCard", new(2, 2, 60, 0, ScalingGrade.C, ScalingGrade.B, ScalingGrade.A), true);
            RegisterWeapon("UnstableCastersGauntlet", new(3, 0, 46, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("YharimsCrystal", new(2, 0, 73, 0, intelligenceScalingGrade: ScalingGrade.A), true);
            #endregion
            #endregion

            #region Summon Weapons
            #region Pre-Hardmode
            // Minion
            RegisterWeapon("BelladonnaSpiritStaff", new(1, 0, 0, 8, faithScalingGrade: ScalingGrade.E));
            RegisterWeapon("BrittleStarStaff", new(1, 0, 0, 5));
            RegisterWeapon("CinderBlossomStaff", new(1, 0, 0, 12, faithScalingGrade: ScalingGrade.D));
            RegisterWeapon("CorroslimeStaff", new(1, 0, 0, 14, faithScalingGrade: ScalingGrade.D));
            RegisterWeapon("CrimslimeStaff", new(1, 0, 0, 14, faithScalingGrade: ScalingGrade.D));
            RegisterWeapon("DankStaff", new(1, 0, 0, 11, faithScalingGrade: ScalingGrade.E));
            RegisterWeapon("DeathstareRod", new(1, 0, 0, 8, faithScalingGrade: ScalingGrade.E));
            RegisterWeapon("EnchantedConch", new(1, 0, 0, 8, faithScalingGrade: ScalingGrade.E));
            RegisterWeapon("EnchantedKnifeStaff", new(1, 0, 0, 5));
            RegisterWeapon("EyeOfNight", new(1, 0, 0, 14, faithScalingGrade: ScalingGrade.D));
            RegisterWeapon("FleshOfInfidelity", new(1, 0, 0, 11, faithScalingGrade: ScalingGrade.E));
            RegisterWeapon("FrostBlossomStaff", new(1, 0, 0, 2, faithScalingGrade: ScalingGrade.E));
            RegisterWeapon("HerringStaff", new(1, 0, 0, 15, faithScalingGrade: ScalingGrade.D));
            RegisterWeapon("PuffShroom", new(1, 0, 0, 11, faithScalingGrade: ScalingGrade.E));
            RegisterWeapon("ScabRipper", new(1, 0, 0, 11, faithScalingGrade: ScalingGrade.E));
            RegisterWeapon("StaffOfNecrosteocytes", new(1, 0, 0, 15, faithScalingGrade: ScalingGrade.D));
            RegisterWeapon("AqueousHunterDrone", new(1, 0, 0, 11, faithScalingGrade: ScalingGrade.D));
            RegisterWeapon("StormjawStaff", new(1, 0, 0, 4));
            RegisterWeapon("SunSpiritStaff", new(1, 0, 0, 4));
            RegisterWeapon("VileFeeder", new(1, 0, 0, 11, faithScalingGrade: ScalingGrade.E));
            RegisterWeapon("WulfrumController", new());

            // Sentry
            RegisterWeapon("CausticCroakerStaff", new(1, 0, 0, 3, faithScalingGrade: ScalingGrade.E));
            RegisterWeapon("HarvestStaff", new(1, 0, 0, 12, faithScalingGrade: ScalingGrade.E));
            RegisterWeapon("PolypLauncher", new(1, 0, 0, 7, faithScalingGrade: ScalingGrade.E));
            RegisterWeapon("RustyBeaconPrototype", new(1, 0, 0, 4, faithScalingGrade: ScalingGrade.E));
            RegisterWeapon("SquirrelSquireStaff", new());

            // Other
            RegisterWeapon("Cnidarian", new(1, 0, 0, 6, faithScalingGrade: ScalingGrade.D));
            RegisterWeapon("SlimePuppetStaff", new(1, 0, 0, 14, faithScalingGrade: ScalingGrade.D));
            #endregion
            #region Hardmode
            // Minion
            RegisterWeapon("AbandonedSlimeStaff", new(2, 0, 0, 32, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("AmphibiansGuitar", new(2, 0, 0, 28, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("AncientIceChunk", new(2, 0, 0, 19, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("BlackHawkRemote", new(2, 0, 0, 17, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("CausticStaff", new(2, 0, 0, 18, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("DaedalusGolemStaff", new(2, 0, 0, 20, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("DeepseaStaff", new(2, 0, 0, 21, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("DormantBrimseeker", new(2, 0, 0, 21, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("EntropysVigil", new(2, 0, 0, 26, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("ForgottenApexWand", new(2, 0, 0, 24, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("FuelCellBundle", new(2, 0, 0, 32, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("GastricBelcherStaff", new(2, 0, 0, 31, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("GlacialEmbrace", new(2, 0, 0, 20, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("HauntedScroll", new(2, 0, 0, 18, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("IgneousExaltation", new(2, 0, 0, 24, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("InfectedRemote", new(2, 0, 0, 33, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("MountedScanner", new(2, 0, 0, 22, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("PlantationStaff", new(2, 0, 0, 28, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("ResurrectionButterfly", new(2, 0, 0, 32, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("SandSharknadoStaff", new(2, 0, 0, 31, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("ShellfishStaff", new(2, 0, 0, 18, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("StarspawnHelixStaff", new(2, 0, 0, 38, faithScalingGrade: ScalingGrade.B));
            RegisterWeapon("TundraFlameBlossomsStaff", new(2, 0, 0, 22, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("VengefulSunStaff", new(2, 0, 0, 25, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("ViralSprout", new(2, 0, 0, 22, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("WitherBlossomsStaff", new(2, 0, 0, 32, faithScalingGrade: ScalingGrade.C));

            // Sentry
            RegisterWeapon("CryogenicStaff", new(2, 0, 0, 20, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("DreadmineStaff", new(2, 0, 0, 31, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("HivePod", new(2, 0, 0, 31, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("OrthoceraShell", new(2, 0, 0, 19, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("PulseTurretRemote", new(2, 0, 0, 32, faithScalingGrade: ScalingGrade.C));
            RegisterWeapon("SpikecragStaff", new(2, 0, 0, 34, faithScalingGrade: ScalingGrade.C));

            // Other
            RegisterWeapon("BorealisBomber", new(2, 0, 0, 31, faithScalingGrade: ScalingGrade.C));
            #endregion
            #region Post-Moon Lord
            // After ML: 45-50
            // Providence: +-60
            // Old Duke + Polterghast: +-65
            // DoG: +-70
            // Yharon: +-75
            // End game: +-80

            // Minion
            RegisterWeapon("AresExoskeleton", new(5, 0, 0, 72, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("CalamarisLament", new(3, 0, 0, 62, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("CorvidHarbringerStaff", new(3, 0, 0, 68, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("CosmicImmaterializer", new(4, 0, 0, 71, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("CosmicViperEngine", new(3, 0, 0, 67, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Cosmilamp", new(3, 0, 0, 60, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("DazzlingStabberStaff", new(3, 0, 0, 57, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("DragonbloodDisgorger", new(3, 0, 0, 62, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("ElementalAxe", new(3, 0, 0, 47, faithScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("EndoHydraStaff", new(3, 0, 0, 68, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Endogenesis", new(4, 0, 0, 76, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("EtherealSubjugator", new(3, 0, 0, 63, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("FlowersOfMortality", new(3, 0, 0, 45, faithScalingGrade: ScalingGrade.B), true);
            RegisterWeapon("GammaHeart", new(3, 0, 0, 62, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("KingofConstellationsTenryu", new(3, 0, 0, 60, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("LiliesOfFinality", new(3, 0, 0, 72, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Metastasis", new(4, 0, 0, 73, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("MidnightSunBeacon", new(3, 0, 0, 72, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("MirrorofKalandra", new(3, 0, 0, 67, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("MutatedTruffle", new(3, 0, 0, 61, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Perdition", new(3, 0, 0, 73, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("PoleWarper", new(3, 0, 0, 69, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("SarosPossession", new(3, 0, 0, 68, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Sirius", new(3, 0, 0, 62, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("SnakeEyes", new(3, 0, 0, 57, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("StaffoftheMechworm", new(3, 0, 0, 67, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("StellarTorusStaff", new(3, 0, 0, 59, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("TacticalPlagueEngine", new(3, 0, 0, 44, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("TemporalUmbrella", new(3, 0, 0, 77, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("Vigilance", new(3, 0, 0, 73, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("ViridVanguard", new(3, 0, 0, 58, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("VoidConcentrationStaff", new(3, 0, 0, 60, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("WarloksMoonFist", new(4, 0, 0, 62, faithScalingGrade: ScalingGrade.S), true);
            RegisterWeapon("YharonsKindleStaff", new(3, 0, 0, 72, faithScalingGrade: ScalingGrade.A), true);

            // Sentry
            RegisterWeapon("AquasScepter", new(3, 0, 0, 58, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("AtlasMunitionsBeacon", new(3, 0, 0, 73, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("CadaverousCarrion", new(3, 0, 0, 62, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("GuidelightofOblivion", new(3, 0, 0, 57, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("SanctifiedSpark", new(3, 0, 0, 48, faithScalingGrade: ScalingGrade.A), true);

            // Other
            RegisterWeapon("CindersOfLament", new(3, 0, 0, 73, faithScalingGrade: ScalingGrade.A), true);
            RegisterWeapon("FlamsteedRing", new(4, 2, 2, 72, faithScalingGrade: ScalingGrade.S), true);
            RegisterWeapon("UniverseSplitter", new(0, 0, 0, 80, faithScalingGrade: ScalingGrade.S, saturation: 1500f));
            #endregion
            #endregion

            #region Rogue Weapons
            #region Pre-Hardmode
            // Bombs
            RegisterWeapon("ContaminatedBile", new(2, 6, 0, 0, dexterityScalingGrade: ScalingGrade.E));
            RegisterWeapon("MeteorFist", new(5, 8, 0, 0, ScalingGrade.E, ScalingGrade.E));
            RegisterWeapon("Pumpkaboom", new());
            RegisterWeapon("SeafoamBomb", new(2, 5, 0, 0));

            // Boomerangs
            RegisterWeapon("EnchantedAxe", new(5, 10, 0, 0, ScalingGrade.E, ScalingGrade.D));
            RegisterWeapon("FishboneBoomerang", new(2, 5, 0, 0));
            RegisterWeapon("Glaive", new(5, 10, 0, 0, ScalingGrade.E, ScalingGrade.D));
            RegisterWeapon("InfestedClawmerang", new(4, 8, 0, 0, ScalingGrade.E, ScalingGrade.E));
            RegisterWeapon("Kylie", new(6, 9, 0, 0, ScalingGrade.E, ScalingGrade.D));
            RegisterWeapon("SandDollar", new(4, 5, 0, 0, ScalingGrade.E, ScalingGrade.E));
            RegisterWeapon("TrackingDisk", new(6, 7, 0, 0, ScalingGrade.E, ScalingGrade.E));

            // Daggers
            RegisterWeapon("AshenStalactite", new(4, 8, 0, 0, ScalingGrade.E, ScalingGrade.E));
            RegisterWeapon("Cinquedea", new(5, 10, 0, 0, ScalingGrade.E, ScalingGrade.D));
            RegisterWeapon("Crystalline", new(2, 3, 0, 0));
            RegisterWeapon("FeatherKnife", new(6, 6, 0, 0, ScalingGrade.E, ScalingGrade.E));
            RegisterWeapon("GelDart", new(5, 10, 0, 0, ScalingGrade.E, ScalingGrade.D));
            RegisterWeapon("GildedDagger", new(2, 2, 0, 0));
            RegisterWeapon("GleamingDagger", new(2, 2, 0, 0));
            RegisterWeapon("InfernalKris", new(4, 9, 0, 0, ScalingGrade.E, ScalingGrade.D));
            RegisterWeapon("Mycoroot", new(4, 7, 0, 0, ScalingGrade.E, ScalingGrade.E));
            RegisterWeapon("ShinobiBlade", new(4, 11, 0, 0, ScalingGrade.E, ScalingGrade.D));
            RegisterWeapon("SporeKnife", new(2, 3, 0, 0));
            RegisterWeapon("WulfrumKnife", new());

            // Javelins
            RegisterWeapon("AntlionSkewer", new());
            RegisterWeapon("ScourgeoftheDesert", new(2, 5, 0, 0));
            RegisterWeapon("Turbulance", new(6, 7, 0, 0, ScalingGrade.E, ScalingGrade.E));

            // Spiky Balls
            RegisterWeapon("MetalMonstrosity", new(4, 10, 0, 0, ScalingGrade.E, ScalingGrade.D));
            RegisterWeapon("NastyCholla", new());
            RegisterWeapon("WebBall", new());

            // Others
            RegisterWeapon("BouncingEyeball", new(2, 4, 0, 0));
            RegisterWeapon("HardenedHoneycomb", new(5, 8, 0, 0, ScalingGrade.E, ScalingGrade.E));
            RegisterWeapon("IronFrancisca", new());
            RegisterWeapon("LeadTomahawk", new());
            RegisterWeapon("Lionfish", new(7, 8, 0, 0, ScalingGrade.E, ScalingGrade.D));
            RegisterWeapon("RotBall", new(4, 7, 0, 0, ScalingGrade.E, ScalingGrade.E));
            RegisterWeapon("SlickCane", new(5, 10, 0, 0, ScalingGrade.E, ScalingGrade.D));
            RegisterWeapon("SludgeSplotch", new(4, 8, 0, 0, ScalingGrade.E, ScalingGrade.D));
            RegisterWeapon("SnapClam", new(4, 5, 0, 0, ScalingGrade.E, ScalingGrade.E));
            RegisterWeapon("ThrowingBrick", new());
            RegisterWeapon("ToothBall", new(4, 7, 0, 0, ScalingGrade.E, ScalingGrade.E));
            RegisterWeapon("UrchinStinger", new(2, 2, 0, 0));
            #endregion
            #region Hardmode
            // Bombs
            RegisterWeapon("BallisticPoisonBomb", new(14, 18, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("BlastBarrel", new(7, 11, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("BrackishFlask", new(13, 18, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("ConsecratedWater", new(10, 15, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("CraniumSmasher", new(15, 21, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("DesecratedWater", new(10, 15, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("DuststormInABottle", new(12, 18, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("Exorcism", new(12, 15, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("Plaguenade", new(12, 18, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("ShockGrenade", new(14, 18, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("SkyfinBombers", new(8, 12, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("SpentFuelContainer", new(8, 12, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("StarofDestruction", new(18, 22, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("TotalityBreakers", new(12, 18, 0, 0, ScalingGrade.D, ScalingGrade.C));

            // Boomerangs
            RegisterWeapon("BlazingStar", new(7, 11, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("Brimblade", new(11, 13, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("DefectiveSphere", new(12, 20, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("EpidemicShredder", new(14, 18, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("Equanimity", new(8, 12, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("FrostcrushValari", new(12, 18, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("Icebreaker", new(7, 12, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("KelvinCatalyst", new(8, 13, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("MangroveChakram", new(12, 16, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("SamsaraSlicer", new(12, 17, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("SubductionSlicer", new(14, 18, 0, 0, ScalingGrade.D, ScalingGrade.C));

            // Daggers
            RegisterWeapon("CobaltKunai", new(7, 11, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("CorpusAvertor", new(14, 22, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("CursedDagger", new(7, 11, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("LeviathanTeeth", new(12, 19, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("Malachite", new(14, 20, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("MythrilKnife", new(9, 14, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("OrichalcumSpikedGemstone", new(9, 14, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("Prismalline", new(7, 11, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("RadiantStar", new(18, 22, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("StellarKnife", new(12, 20, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("StormfrontRazor", new(10, 14, 0, 0, ScalingGrade.D, ScalingGrade.C));

            // Javelins
            RegisterWeapon("CrystalPiercer", new(7, 12, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("FrequencyManipulator", new(9, 14, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("IchorSpear", new(7, 11, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("PalladiumJavelin", new(7, 11, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("PhantasmalRuin", new(12, 18, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("ScourgeoftheSeas", new(8, 12, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("ShardofAntumbra", new(18, 22, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("SpearofDestiny", new(12, 15, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("SpearofPaleolith", new(10, 15, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("WaveSkipper", new(7, 11, 0, 0, ScalingGrade.D, ScalingGrade.D));

            // Spiky Balls
            RegisterWeapon("BurningStrife", new(8, 11, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("SystemBane", new(14, 20, 0, 0, ScalingGrade.C, ScalingGrade.C));

            // Others
            RegisterWeapon("AdamantiteThrowingAxe", new(10, 14, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("Apoctolith", new(15, 18, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("AuroradicalThrow", new(12, 20, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("CrushsawCrasher", new(12, 15, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("DeepWounder", new(12, 17, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("DukesDecapitator", new(15, 19, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("FantasyTalisman", new(12, 18, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("FrostyFlare", new(12, 15, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("GacruxianMollusk", new(7, 11, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("GraveGrimreaver", new(12, 16, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("HeavenfallenStardisk", new(12, 20, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("IceStar", new(7, 12, 0, 0, ScalingGrade.D, ScalingGrade.D));
            RegisterWeapon("LeonidProgenitor", new(12, 20, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("RegulusRiot", new(16, 24, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("TheSyringe", new(14, 20, 0, 0, ScalingGrade.C, ScalingGrade.C));
            RegisterWeapon("TerrorTalons", new(12, 16, 0, 0, ScalingGrade.D, ScalingGrade.C));
            RegisterWeapon("TitaniumShuriken", new(10, 14, 0, 0, ScalingGrade.D, ScalingGrade.C));
            #region Post-Moon Lord
            //
            /*
             * Post ML: 45-50
             * Dragonfolly: 50-60
             * Providence / Rune of Kos Bosses: 60-65
             * Polterghast / Old Duke: 63-67
             * DoG: 68-72
             * Yharon, etc: 75+
             * End Game: 80
            */
            // Bombs
            RegisterWeapon("Penumbra", new(30, 40, 0, 0, ScalingGrade.B, ScalingGrade.A), true);
            RegisterWeapon("PlasmaGrenade", new(28, 40, 2, 0, ScalingGrade.B, ScalingGrade.A), true);
            RegisterWeapon("SealedSingularity", new(25, 37, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("Supernova", new(32, 45, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("WavePounder", new(24, 36, 0, 0, ScalingGrade.B, ScalingGrade.B), true);

            // Boomerangs
            RegisterWeapon("Celestus", new(32, 45, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("DynamicPursuer", new(32, 42, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("ElementalDisk", new(20, 28, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("Eradicator", new(30, 40, 0, 0, ScalingGrade.B, ScalingGrade.A), true);
            RegisterWeapon("GhoulishGouger", new(26, 39, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("MoltenAmputator", new(24, 36, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("NanoblackReaper", new(32, 48, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("ToxicantTwister", new(26, 40, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("Valediction", new(26, 40, 0, 0, ScalingGrade.B, ScalingGrade.B), true);

            // Daggers
            RegisterWeapon("CosmicKunai", new(25, 38, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("JawsOfOblivion", new(26, 39, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("LunarKunai", new(20, 26, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("Sacrifice", new(32, 45, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("Seraphim", new(32, 43, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("ShatteredDawn", new(24, 37, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("TarragonThrowingDart", new(24, 36, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("TimeBolt", new(26, 39, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("TwistingThunder", new(25, 37, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("UtensilPoker", new(18, 25, 0, 0, ScalingGrade.B, ScalingGrade.B), true);

            // Javelins
            RegisterWeapon("TheAtomSplitter", new(32, 44, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("EclipsesFall", new(30, 41, 0, 0, ScalingGrade.B, ScalingGrade.A), true);
            RegisterWeapon("NightsGaze", new(26, 40, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("ProfanedPartisan", new(24, 37, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("RealityRupture", new(25, 39, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("ScarletDevil", new(32, 48, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("Wrathwing", new(32, 43, 0, 0, ScalingGrade.A, ScalingGrade.A), true);

            // Spiky Balls
            RegisterWeapon("GodsParanoia", new(30, 38, 0, 0, ScalingGrade.A, ScalingGrade.A), true);

            // Others
            RegisterWeapon("BloodsoakedCrasher", new(25, 39, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("CelestialReaper", new(18, 25, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("DeepSeaDumbbell", new(26, 40, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("ExecutionersBlade", new(30, 39, 0, 0, ScalingGrade.B, ScalingGrade.A), true);
            RegisterWeapon("TheFinalDawn", new(32, 44, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            RegisterWeapon("Hypothermia", new(39, 40, 0, 0, ScalingGrade.A, ScalingGrade.B), true);
            RegisterWeapon("TheOldReaper", new(25, 40, 0, 0, ScalingGrade.B, ScalingGrade.B), true);
            RegisterWeapon("RefractionRotor", new(33, 44, 0, 0, ScalingGrade.A, ScalingGrade.A), true);
            #endregion
            #endregion
            #endregion

            #region Classless weapons
            #region Pre-Hardmode
            RegisterWeapon("Aestheticus", new());
            RegisterWeapon("Skynamite", new());
            #endregion
            #region Hardmode
            RegisterWeapon("EyeofMagnus", new());
            RegisterWeapon("LunicEye", new());
            RegisterWeapon("StarStruckWater", new());
            #endregion
            #region Post-Moon Lord
            RegisterWeapon("ClaretCannon", new());
            RegisterWeapon("RelicOfDeliverance", new());
            RegisterWeapon("StratusSphere", new());

            #endregion
            #endregion

            #region Multiclass weapons
            #region Post-Moon Lord
            RegisterWeapon("PrismaticBreaker", new(0, 0, 0, 0, ScalingGrade.B, ScalingGrade.B, ScalingGrade.B, ScalingGrade.B), true);
            #endregion
            #endregion
        }
    }
}