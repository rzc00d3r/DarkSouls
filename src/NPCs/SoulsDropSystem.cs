using DarkSouls.Systems;
using DarkSouls.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarkSouls.NPCs
{
    public class SoulsDropSystem : GlobalNPC
    {
        public static HashSet<int> NPCIDBlackList = new()
        {
            #region Pre-Hardmode Bosses
            NPCID.KingSlime,
            NPCID.EyeofCthulhu,
            
            NPCID.EaterofWorldsBody,
            NPCID.EaterofWorldsTail,
            NPCID.EaterofWorldsHead,
            
            NPCID.BrainofCthulhu,
            NPCID.QueenBee,
            NPCID.Deerclops,
            
            NPCID.SkeletronHand,
            NPCID.SkeletronHead,
            
            NPCID.WallofFlesh,
            NPCID.WallofFleshEye,
            #endregion

            #region Hardmode Bosses
            NPCID.QueenSlimeBoss,
            
            NPCID.Retinazer,
            NPCID.Spazmatism,
            
            NPCID.TheDestroyer,
            NPCID.TheDestroyerBody,
            NPCID.TheDestroyerTail,
           
            NPCID.SkeletronPrime,
            NPCID.PrimeCannon,
            NPCID.PrimeLaser,
            NPCID.PrimeSaw,
            NPCID.PrimeVice,
            
            NPCID.Plantera,
            NPCID.Golem,
            NPCID.GolemFistLeft,
            NPCID.GolemFistRight,
            NPCID.GolemHead,
            NPCID.GolemHeadFree,
            
            NPCID.DukeFishron,
            NPCID.HallowBoss,
            NPCID.CultistBoss,
            
            NPCID.LunarTowerSolar,
            NPCID.SolarCrawltipedeBody,
            NPCID.SolarCrawltipedeBody,
            NPCID.SolarCrawltipedeTail,
            NPCID.LunarTowerVortex,
            NPCID.LunarTowerNebula,
            NPCID.LunarTowerStardust,
            NPCID.StardustCellSmall,

            NPCID.MoonLordCore,
            NPCID.MoonLordFreeEye,
            NPCID.MoonLordHand,
            NPCID.MoonLordHead,
            NPCID.MoonLordLeechBlob,
            #endregion

            #region Bosses Servants
            NPCID.CultistBossClone,
            NPCID.AncientDoom,
            NPCID.AncientCultistSquidhead,
            NPCID.QueenSlimeMinionPink,
            NPCID.QueenSlimeMinionBlue,
            NPCID.QueenSlimeMinionPurple,
            NPCID.Creeper,
            NPCID.LeechHead,
            NPCID.LeechBody,
            NPCID.LeechTail,
            NPCID.CultistDragonHead,
            NPCID.CultistDragonBody1,
            NPCID.CultistDragonBody2,
            NPCID.CultistDragonBody3,
            NPCID.CultistDragonBody4,
            NPCID.CultistDragonTail,
            NPCID.PlanterasTentacle,
            NPCID.Probe,
            NPCID.TheHungry,
            NPCID.TheHungryII,
            NPCID.MoonLordFreeEye,
            NPCID.ServantofCthulhu,
            NPCID.SlimeSpiked,
            NPCID.Sharkron,
            NPCID.Sharkron2,
            #endregion

            #region Old One's Army
            NPCID.DD2DrakinT2,
            NPCID.DD2DrakinT3,
            NPCID.DD2GoblinT1,
            NPCID.DD2GoblinT2,
            NPCID.DD2GoblinT3,
            NPCID.DD2GoblinBomberT1,
            NPCID.DD2GoblinBomberT2,
            NPCID.DD2GoblinBomberT3,
            NPCID.DD2JavelinstT1,
            NPCID.DD2JavelinstT2,
            NPCID.DD2JavelinstT3,
            NPCID.DD2LightningBugT3,
            NPCID.DD2WyvernT1,
            NPCID.DD2WyvernT2,
            NPCID.DD2WyvernT3,
            NPCID.DD2KoboldWalkerT2,
            NPCID.DD2KoboldWalkerT3,
            NPCID.DD2KoboldFlyerT2,
            NPCID.DD2KoboldFlyerT3,
            NPCID.DD2WitherBeastT2,
            NPCID.DD2WitherBeastT3,
            NPCID.DD2OgreT2,
            NPCID.DD2OgreT3,
            NPCID.DD2DarkMageT1,
            NPCID.DD2DarkMageT3,
            NPCID.DD2SkeletonT1,
            NPCID.DD2SkeletonT3,
            #endregion

            NPCID.Bee,
            NPCID.BeeSmall,
            NPCID.CursedSkull,
            NPCID.Crab,
            NPCID.FungiBulb,
            NPCID.MeteorHead,
            NPCID.Gnome
        };

        private static Dictionary<int, double> AbyssNPCSoulsMultipliers;

        // This flag prevents double reward drops for defeating the Exo Mechs, which happens because of Apollo and Artemis
        private static bool ExoMechsSoulsReward = false;

        public override bool PreKill(NPC npc)
        {
            // downed Calamity boss flags are set before this OnKill function (OnKill function will get always true)
            // PreKill fixed it
            if (npc.ModNPC?.Mod.Name == "CalamityMod")
                CalamityDownedBossSystem.RefreshDownedInfo(refreshExoMechs: true);
            return true;
        }

        public override void OnKill(NPC npc)
        {
            int playerIndex = npc.lastInteraction;
            if (playerIndex == 255)
                return;

            int npcID = npc.type;
            if (!npc.boss && npcID != NPCID.LunarTowerNebula && npcID != NPCID.LunarTowerSolar && npcID != NPCID.LunarTowerStardust && npcID != NPCID.LunarTowerVortex)
            {
                // NPC hasn't been damaged by any Player + Souls farming with Statues and friendly NPCs and Boss parts disabled :)
                if (npc.SpawnedFromStatue || npc.friendly || npc.townNPC || npc.lifeMax <= 5 || (npc.aiStyle == 0 && npc.damage == 0))
                    return;
            }

            bool boss;
            DarkSoulsPlayer dsPlayer = Main.player[playerIndex].GetModPlayer<DarkSoulsPlayer>();
            int souls = GetSoulsByNPC(npc, out boss);

            if (souls <= 0)
                return;

            float soulsMultiplier = 1f;

            if (!boss && !Config.ServerConfig.Instance.DisableCrowdControlMultiplier)
                soulsMultiplier = GetCrowdControlMultiplier();

            souls = (int)(souls * soulsMultiplier);

            if (Main.dedServ)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)DarkSouls.NetMessageTypes.GetSouls);
                packet.Write(souls);
                if (boss) // server sends souls to all clients (if NPC is downed boss)
                    packet.Send();
                else // 
                    packet.Send(playerIndex); // if the client (specific player) kills someone other than boss
            }
            else // single player
                dsPlayer.AddSouls(souls);
        }

        private float GetCrowdControlMultiplier()
        {
            int activeHostileNPCs = 0;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.active && !n.friendly && !n.townNPC && n.lifeMax > 5 && n.damage > 0)
                    activeHostileNPCs++;
            }

            // 15-20 - ванилка
            // 40 - водяной свеча, боевое зелье
            float standardCap = 40f;

            if (activeHostileNPCs <= standardCap)
                return 1.0f;

            // Боевое зелье (40 мобов) -> множитель 1. (заработок x2)
            // Зелье Зергов (90+ мобов) -> множитель 0.44 (заработок +- как и в первом случае)
            return standardCap / (float)activeHostileNPCs;
        }

        public static bool AddNPCIDToBlacklist(int npcID)
        {
            if (NPCIDBlackList.Contains(npcID))
            {
                NPC npc = new(); npc.SetDefaults(npcID);
                LoggingUtils.Error("SoulsDropSystem", $"Same key has already been added ({npcID}, {npc.TypeName})");
                return false;
            }
            NPCIDBlackList.Add(npcID);
            return true;
        }

        public static void EnableCalamityModSupport()
        {
            if (DarkSouls.CalamityModIsEnabled)
            {
                LoggingUtils.Info("SoulsDropSystem", "Initialize for CalamityMod");

                AbyssNPCSoulsMultipliers = new()
                {
                    { CalamityNPCID.Oarfish, 0.15 },       // 600
                    { CalamityNPCID.MirageJelly, 0.12 },   // 720
                    { CalamityNPCID.ColossalSquid, 0.02 }, // 2600
                    { CalamityNPCID.Eidolist, 0.1 },       // 500
                    { CalamityNPCID.GulperEel, 1d/48d },   // 1000
                    { CalamityNPCID.Bloatfish, 0.1 },      // 720
                    { CalamityNPCID.BobbitWorm, 0.1 },     // 600
                    { CalamityNPCID.EidolonWyrm, 0.02 },   // 3200
                    { CalamityNPCID.ReaperShark, 0.02 }    // 2000
                };

                AddNPCIDToBlacklist(CalamityDownedBossSystem.DesertScourge.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("DesertScourgeBody"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("DesertScourgeTail"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("DesertNuisanceHead"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("DesertNuisanceBody"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("DesertNuisanceTail"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("DesertNuisanceHeadYoung"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.Crabulon.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("CrabShroom"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.TheHiveMind.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("DankCreeper"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("DarkHeart"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("HiveBlob"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.ThePerforators.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("PerforatorHeadSmall"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("PerforatorBodySmall"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("PerforatorTailSmall"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("PerforatorHeadMedium"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("PerforatorBodyMedium"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("PerforatorTailMedium"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("PerforatorHeadLarge"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("PerforatorBodyLarge"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("PerforatorTailLarge"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.TheSlimeGod.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("CorruptSlimeSpawn"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("CorruptSlimeSpawn2"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("CrimsonSlimeSpawn"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("CrimsonSlimeSpawn2"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("CrimulanPaladin"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("EbonianPaladin"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("SplitCrimulanPaladin"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("SplitEbonianPaladin"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.Cryogen.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("CryogenShield"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.AquaticScourge.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("AquaticScourgeBody"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("AquaticScourgeBodyAlt"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("AquaticScourgeTail"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.BrimstoneElemental.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("Brimling"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.CalamitasClone.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("Cataclysm"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("Catastrophe"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("SoulSeeker"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.LeviathanAndAnahita.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("AnahitasIceShield"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("AquaticAberration"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("Leviathan"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("LeviathanStart"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.AstrumAureus.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("AureusSpawn"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.ThePlaguebringerGoliath.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("PlagueHomingMissile"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("PlagueMine"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.Ravager.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("FlamePillar"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("RavagerClawLeft"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("RavagerClawRight"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("RavagerHead"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("RavagerHead2"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("RavagerLegLeft"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("RavagerLegRight"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("RockPillar"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.AstrumDeus.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("AstrumDeusBody"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("AstrumDeusTail"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.ProfanedGuardians.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("ProfanedGuardianDefender"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("ProfanedGuardianHealer"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("ProfanedRocks"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.Dragonfolly.ID);

                AddNPCIDToBlacklist(CalamityDownedBossSystem.ProvidenceTheProfanedGoddess.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("ProvSpawnDefense"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("ProvSpawnHealer"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("ProvSpawnOffense"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.StormWeaver.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("StormWeaverBody"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("StormWeaverTail"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.CeaselessVoid.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("DarkEnergy"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.SignusEnvoyOfTheDevourer.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("CosmicLantern"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("CosmicMine"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.Polterghast.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("PhantomFuckYou"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("PolterghastHook"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("PolterPhantom"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.TheDevourerOfGods.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("DevourerofGodsBody"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("DevourerofGodsTail"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.YharonDragonOfRebirth.ID);
                
                AddNPCIDToBlacklist(CalamityDownedBossSystem.SupremeWitchCalamitas.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("BrimstoneHeart"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("SepulcherHead"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("SepulcherBody"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("SepulcherBodyEnergyBall"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("SepulcherArm"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("SepulcherTail"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("SoulSeekerSupreme"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("SupremeCataclysm"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("SupremeCatastrophe"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.Apollo.ID);
                AddNPCIDToBlacklist(CalamityDownedBossSystem.Artemis.ID);
                AddNPCIDToBlacklist(CalamityDownedBossSystem.Ares.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("AresGaussNuke"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("AresLaserCannon"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("AresPlasmaFlamethrower"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("AresTeslaCannon"));
                AddNPCIDToBlacklist(CalamityDownedBossSystem.Thanatos.ID);
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("ThanatosBody1"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("ThanatosBody2"));
                AddNPCIDToBlacklist(CalamityNPCID.GetCalamityNPCIDByName("ThanatosTail"));

                AddNPCIDToBlacklist(CalamityDownedBossSystem.GiantClam.ID);
                AddNPCIDToBlacklist(CalamityDownedBossSystem.PrimordialWyrm.ID);
                AddNPCIDToBlacklist(CalamityDownedBossSystem.CragmawMire.ID);
            }
        }

        public int GetSoulsByNPC(NPC npc, out bool boss)
        {
            boss = false;
            int npcID = npc.type;
            int souls = 0;

            // Bosses
            if (npc.ModNPC == null) // Vanilla Terraria
            {
                #region Pre-Hardmode
                if (npcID == NPCID.KingSlime) // King Slime
                {
                    NPC kingSlime = new(); kingSlime.SetDefaults(npcID);
                    souls = kingSlime.lifeMax; // 2000
                    if (NPC.downedSlimeKing) // 300
                        souls = (int)(kingSlime.lifeMax * (2d / 30d));
                    boss = true;
                    return souls;
                }
                else if (npcID == NPCID.EyeofCthulhu) // Eye of Cthulhu
                {
                    NPC eyeOfCthulhu = new(); eyeOfCthulhu.SetDefaults(npcID);
                    souls = eyeOfCthulhu.lifeMax; // Calamity: 3000, Vanilla: 2800
                    if (NPC.downedBoss1) // 400
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(eyeOfCthulhu.lifeMax * (2d / 15d));
                        else
                            souls = (int)(eyeOfCthulhu.lifeMax * (1d / 7d));
                    }
                    boss = true;
                    return souls;
                }
                else if (npcID == NPCID.BrainofCthulhu || npcID == NPCID.EaterofWorldsHead)
                {
                    NPC eaterOfWorlds = new(); eaterOfWorlds.SetDefaults(NPCID.EaterofWorldsHead);
                    souls = eaterOfWorlds.lifeMax; // Calamity: 11725, Vanilla: 10050
                    if (NPC.downedBoss2) // 560
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(eaterOfWorlds.lifeMax * (16d / 335d));
                        else
                            souls = (int)(eaterOfWorlds.lifeMax * (56d / 1005d));
                    }
                    else // 4700
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(eaterOfWorlds.lifeMax * (188d / 469d));
                        else
                            souls = (int)(eaterOfWorlds.lifeMax * (94d / 201d));
                    }
                    boss = true;
                    return souls;
                }
                else if (npcID == NPCID.QueenBee) // Queen Bee
                {
                    NPC queenBee = new(); queenBee.SetDefaults(npcID);
                    souls = queenBee.lifeMax;
                    if (NPC.downedQueenBee) // 610
                        souls = (int)(souls * (61d / 340d));
                    else // 5400
                        souls = (int)(souls * (27d / 17d));
                    boss = true;
                    return souls;
                }
                else if (npcID == NPCID.Deerclops) // Deerclops
                {
                    NPC deerclops = new(); deerclops.SetDefaults(npcID);
                    souls = deerclops.lifeMax; // Calamity: 10000, Vanilla: 7000
                    if (NPC.downedDeerclops) // 700
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (7d / 100d));
                        else
                            souls = (int)(souls * (1d / 10d));
                    }
                    else // 6200
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (31d / 50d));
                        else
                            souls = (int)(souls * (31d / 35d));
                    }

                    boss = true;
                    return souls;
                }
                else if (npcID == NPCID.SkeletronHead) // Skeletron
                {
                    NPC skeletron = new(); skeletron.SetDefaults(npcID);
                    NPC skeletronHand = new(); skeletron.SetDefaults(NPCID.SkeletronHand);
                    souls = skeletron.lifeMax + 2 * skeletronHand.lifeMax;
                    if (NPC.downedBoss3) // 750
                        souls = (int)(souls * (15d / 88d));
                    else // 6600
                        souls = (int)(souls * 1.5f);
                    boss = true;
                    return souls;
                }
                else if (npcID == NPCID.WallofFleshEye || npcID == NPCID.WallofFlesh) // Wall of Flesh
                {
                    NPC wallOfFlesh = new(); wallOfFlesh.SetDefaults(NPCID.WallofFlesh);
                    souls = wallOfFlesh.lifeMax; // Calamity: 12800, Vanilla: 8000
                    if (Main.hardMode) // 840
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (21d / 320d));
                        else
                            souls = (int)(souls * (21d / 200d));
                    }
                    else // 8000
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (5d / 8d));
                    }
                    boss = true;
                    return souls;
                }
                #endregion
                #region Hardmode

                else if (npcID == NPCID.QueenSlimeBoss) // Queen Slime
                {
                    NPC queenSlime = new(); queenSlime.SetDefaults(npcID);
                    souls = queenSlime.lifeMax; // Calamity: 27000, Vanilla: 18000
                    if (NPC.downedQueenSlime) // 900
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (1d / 30d));
                        else
                            souls = (int)(souls * (1d / 20d));
                    }
                    else // 8800
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (44d / 135d));
                        else
                            souls = (int)(souls * (22d / 45d));
                    }
                    boss = true;
                    return souls;
                }
                else if (Main.zenithWorld && (npcID == NPCID.Retinazer || npcID == NPCID.Spazmatism || npcID == NPCID.SkeletronPrime || npcID == NPCID.TheDestroyer)) // Mechdusa
                {
                    if (AreMechBossesAlive(npc))
                        return 0;
                    NPC retinazer = new(); retinazer.SetDefaults(NPCID.Retinazer);
                    NPC spazmatism = new(); spazmatism.SetDefaults(NPCID.Spazmatism);
                    NPC destroyer = new(); destroyer.SetDefaults(NPCID.TheDestroyer);
                    NPC skeletronPrime = new(); skeletronPrime.SetDefaults(NPCID.SkeletronPrime);
                    souls = retinazer.lifeMax + spazmatism.lifeMax + destroyer.lifeMax + skeletronPrime.lifeMax; // Calamity: 211200, Vanilla: 205200
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) // 3000
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (5d/352d));
                        else
                            souls = (int)(souls * (5d/342d));
                    }
                    else // 42000
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (35d/176d));
                        else
                            souls = (int)(souls * (35d/171d));
                    }
                    boss = true;
                    return souls;

                }
                else if (npcID == NPCID.Retinazer || npcID == NPCID.Spazmatism) // Twins
                {
                    if (!NPC.AnyNPCs(npcID == NPCID.Retinazer ? NPCID.Spazmatism : NPCID.Retinazer)) // Second is not alive
                    {
                        NPC retinazer = new(); retinazer.SetDefaults(NPCID.Retinazer);
                        NPC spazmatism = new(); spazmatism.SetDefaults(NPCID.Spazmatism);
                        souls = retinazer.lifeMax + spazmatism.lifeMax; // Calamity: 48000, Vanilla: 43000
                        if (NPC.downedMechBoss2) // 1100
                        {
                            if (DarkSouls.CalamityModIsEnabled)
                                souls = (int)(souls * (11d / 480d));
                            else
                                souls = (int)(souls * (11d / 430d));
                        }
                        else // 11200
                        {
                            if (DarkSouls.CalamityModIsEnabled)
                                souls = (int)(souls * (7d / 30d));
                            else
                                souls = (int)(souls * (56d / 215d));
                        }
                        boss = true;
                    }
                    return souls;
                }
                else if (npcID == NPCID.TheDestroyer) // The Destroyer
                {
                    NPC destroyer = new(); destroyer.SetDefaults(npcID);
                    souls = destroyer.lifeMax;
                    if (NPC.downedMechBoss1) // 1150
                        souls = (int)(souls * (23d / 1600d));
                    else // 13500
                        souls = (int)(souls * (27d / 160d));
                    boss = true;
                    return souls;
                }
                else if (npcID == NPCID.SkeletronPrime) // Skeletron Prime
                {
                    NPC skeletronPrime = new(); skeletronPrime.SetDefaults(npcID);
                    souls = skeletronPrime.lifeMax;
                    if (NPC.downedMechBoss3) // 1350
                        souls = (int)(souls * (27d / 560d));
                    else // 17000
                        souls = (int)(souls * (17d / 28d));
                    boss = true;
                    return souls;
                }

                else if (npcID == NPCID.Plantera) // Plantera
                {
                    NPC plantera = new(); plantera.SetDefaults(npcID);
                    souls = plantera.lifeMax; // Calamity: 75000, Vanilla: 30000
                    if (NPC.downedPlantBoss) // 1750
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (7d / 300d));
                        else
                            souls = (int)(souls * (7d / 120d));
                    }
                    else // 22500
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (3d / 10d));
                        else
                            souls = (int)(souls * (3d / 4d));
                    }
                    boss = true;
                    return souls;
                }
                else if (npcID == NPCID.Golem) // Golem
                {
                    NPC golem = new(); golem.SetDefaults(npcID);
                    NPC golemFist = new(); golemFist.SetDefaults(NPCID.GolemFistLeft);
                    NPC golemHead = new(); golemHead.SetDefaults(NPCID.GolemHead);
                    souls = golem.lifeMax + 2 * golemFist.lifeMax + golemHead.lifeMax; // Calamity: 64000, Vanilla: 60000
                    if (NPC.downedGolemBoss) // 2500
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (5d / 128d));
                        else
                            souls = (int)(souls * (1d / 24d));
                    }
                    else // 31000
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (31d / 64d));
                        else
                            souls = (int)(souls * (31d / 60d));
                    }
                    boss = true;
                    return souls;
                }
                else if (npcID == NPCID.DukeFishron) // Duke Fishron
                {
                    NPC dukeFishron = new(); dukeFishron.SetDefaults(npcID);
                    souls = dukeFishron.lifeMax; // Calamity: 100000, Vanilla: 60000
                    if (NPC.downedFishron) // 2800
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (7d / 250d));
                        else
                            souls = (int)(souls * (7d / 150d));
                    }
                    else // 34500
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (69d / 200d));
                        else
                            souls = (int)(souls * (23d / 40d));
                    }
                    boss = true;
                    return souls;
                }
                else if (npcID == NPCID.HallowBoss) // Empress of Light
                {
                    NPC empressOfLight = new(); empressOfLight.SetDefaults(npcID);
                    souls = empressOfLight.lifeMax; // Calamity: 100000, Vanilla: 70000
                    if (NPC.downedEmpressOfLight) // 3250
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (13d / 400d));
                        else
                            souls = (int)(souls * (13d / 280d));
                    }
                    else // 38500
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (77d / 200d));
                        else
                            souls = (int)(souls * (11d / 20d));
                    }
                    boss = true;
                    return souls;
                }
                else if (npcID == NPCID.CultistBoss) // Cultist
                {
                    NPC cultist = new(); cultist.SetDefaults(npcID);
                    souls = cultist.lifeMax; // Calamity: 80000, Vanilla: 32000
                    if (NPC.downedAncientCultist) // 3800
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (19d / 400d));
                        else
                            souls = (int)(souls * (19d / 160d));
                    }
                    else // 41500
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (83d / 160d));
                        else
                            souls = (int)(souls * (83d / 64d));
                    }
                    boss = true;
                    return souls;
                }
                else if (npcID == NPCID.LunarTowerNebula || npcID == NPCID.LunarTowerSolar ||
                    npcID == NPCID.LunarTowerStardust || npcID == NPCID.LunarTowerVortex) // Lunar Towers
                {
                    NPC tower = new(); tower.SetDefaults(npcID);
                    souls = tower.lifeMax; // 20000
                    if ((NPC.downedTowerSolar && npcID == NPCID.LunarTowerSolar) || (NPC.downedTowerNebula && npcID == NPCID.LunarTowerNebula) ||
                        (NPC.downedTowerVortex && npcID == NPCID.LunarTowerVortex) || (NPC.downedTowerStardust && npcID == NPCID.LunarTowerStardust)
                    ) // 2000
                        souls = (int)(souls * (1d / 10d));
                    boss = true;
                    return souls;
                }
                else if (npcID == NPCID.MoonLordCore) // Moon Lord
                {
                    NPC moonLordCore = new(); moonLordCore.SetDefaults(npcID);
                    NPC moonLordHead = new(); moonLordHead.SetDefaults(NPCID.MoonLordHead);
                    NPC moonLordHand = new(); moonLordHand.SetDefaults(NPCID.MoonLordHand);
                    souls = moonLordCore.lifeMax + moonLordHead.lifeMax + 2 * moonLordHand.lifeMax; // Calamity: 187000, Vanilla: 145000
                    if (NPC.downedMoonlord) // 4500
                    {
                        if (DarkSouls.CalamityModIsEnabled)
                            souls = (int)(souls * (9d / 374d));
                        else
                            souls = (int)(souls * (9d / 290d));
                    }
                    else // 50000
                        if (DarkSouls.CalamityModIsEnabled)
                        souls = (int)(souls * (50d / 187d));
                    else
                        souls = (int)(souls * (10d / 29d));
                    boss = true;
                    return souls;
                }
                #endregion
            }
            else if (npc.ModNPC?.Mod.Name == "CalamityMod") // Calamity Mod
            {
                #region Pre-Hardmode
                if (npcID == CalamityDownedBossSystem.DesertScourge.ID)
                {
                    boss = true;
                    NPC desertScourge = new(); desertScourge.SetDefaults(npcID);
                    souls = desertScourge.lifeMax;
                    if (CalamityDownedBossSystem.DesertScourge.Downed) // 350
                        souls = (int)(souls * (1d / 12d));
                    else // 2500
                        souls = (int)(souls * (25d / 42d));
                    return souls;
                }
                if (npcID == CalamityDownedBossSystem.Crabulon.ID)
                {
                    boss = true;
                    NPC crabulon = new(); crabulon.SetDefaults(npcID);
                    souls = crabulon.lifeMax; // 3700
                    if (CalamityDownedBossSystem.Crabulon.Downed) // 500
                        souls = (int)(souls * (5d / 37d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.ThePerforators.ID || npcID == CalamityDownedBossSystem.TheHiveMind.ID)
                {
                    boss = true;
                    NPC theHiveMind = new(); theHiveMind.SetDefaults(CalamityDownedBossSystem.TheHiveMind.ID);
                    souls = theHiveMind.lifeMax;
                    if (CalamityDownedBossSystem.TheHiveMind.Downed) // 600
                        souls = (int)(souls * (6d / 77d));
                    else // 5100
                        souls = (int)(souls * (51d / 77d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.TheSlimeGod.ID)
                {
                    boss = true;
                    NPC slimeGodCore = new(); slimeGodCore.SetDefaults(npcID);
                    NPC ebonianPaladin = new(); ebonianPaladin.SetDefaults(CalamityNPCID.GetCalamityNPCIDByName("EbonianPaladin"));
                    NPC crimulanPaladin = new(); crimulanPaladin.SetDefaults(CalamityNPCID.GetCalamityNPCIDByName("CrimulanPaladin"));
                    int theSlimeGodTotalHP = slimeGodCore.lifeMax + ebonianPaladin.lifeMax + crimulanPaladin.lifeMax;
                    if (CalamityDownedBossSystem.TheSlimeGod.Downed) // 790
                        souls = (int)(theSlimeGodTotalHP * (79d / 1592d));
                    else // 7160
                        souls = (int)(theSlimeGodTotalHP * (179d / 398d));
                    return souls;
                }
                #endregion
                #region Hardmode
                else if (npcID == CalamityDownedBossSystem.Cryogen.ID)
                {
                    boss = true;
                    NPC cryogen = new(); cryogen.SetDefaults(npcID);
                    souls = cryogen.lifeMax;
                    if (CalamityDownedBossSystem.Cryogen.Downed) // 1000
                        souls = (int)(souls * (1d / 40d));
                    else // 10000
                        souls = (int)(souls * (1d / 4d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.AquaticScourge.ID)
                {
                    boss = true;
                    NPC aquaticScourge = new(); aquaticScourge.SetDefaults(npcID);
                    souls = aquaticScourge.lifeMax;
                    if (CalamityDownedBossSystem.AquaticScourge.Downed) // 1150
                        souls = (int)(souls * (23d / 1600d));
                    else // 12500
                        souls = (int)(souls * (5d / 32d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.BrimstoneElemental.ID)
                {
                    boss = true;
                    NPC brimstoneElemental = new(); brimstoneElemental.SetDefaults(npcID);
                    souls = brimstoneElemental.lifeMax;
                    if (CalamityDownedBossSystem.BrimstoneElemental.Downed) // 1250
                        souls = (int)(souls * (5d / 164d));
                    else // 15000
                        souls = (int)(souls * (15d / 41d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.CalamitasClone.ID)
                {
                    boss = true;
                    NPC cataclysm = new(); cataclysm.SetDefaults(CalamityNPCID.GetCalamityNPCIDByName("Cataclysm"));
                    NPC catastrophe = new(); catastrophe.SetDefaults(CalamityNPCID.GetCalamityNPCIDByName("Catastrophe"));
                    souls = cataclysm.lifeMax + catastrophe.lifeMax;
                    if (CalamityDownedBossSystem.CalamitasClone.Downed) // 1500
                        souls = (int)(souls * (15d / 202d));
                    else // 19000
                        souls = (int)(souls * (95d / 101d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.LeviathanAndAnahita.ID)
                {
                    boss = true;
                    NPC leviathan = new(); leviathan.SetDefaults(CalamityNPCID.GetCalamityNPCIDByName("Leviathan"));
                    NPC anahita = new(); anahita.SetDefaults(CalamityNPCID.GetCalamityNPCIDByName("Anahita"));
                    souls = leviathan.lifeMax + anahita.lifeMax;
                    if (CalamityDownedBossSystem.LeviathanAndAnahita.Downed) // 2000
                        souls = (int)(souls * (2d / 95d));
                    else // 25750
                        souls = (int)(souls * (103d / 380d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.AstrumAureus.ID)
                {
                    boss = true;
                    NPC astrumAureus = new(); astrumAureus.SetDefaults(npcID);
                    souls = astrumAureus.lifeMax;
                    if (CalamityDownedBossSystem.AstrumAureus.Downed) // 2250
                        souls = (int)(souls * (9d / 400d));
                    else // 28000
                        souls = (int)(souls * (7d / 25d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.ThePlaguebringerGoliath.ID)
                {
                    boss = true;
                    NPC thePlaguebringerGoliath = new(); thePlaguebringerGoliath.SetDefaults(npcID);
                    souls = thePlaguebringerGoliath.lifeMax;
                    if (CalamityDownedBossSystem.ThePlaguebringerGoliath.Downed) // 3000
                        souls = (int)(souls * (6d / 175d));
                    else // 36000
                        souls = (int)(souls * (72d / 175d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.Ravager.ID)
                {
                    boss = true;
                    NPC ravager = new(); ravager.SetDefaults(npcID);
                    souls = ravager.lifeMax;
                    if (CalamityDownedBossSystem.Ravager.Downed) // 3600
                        souls = (int)(souls * (2d / 25d));
                    else // 40000
                        souls = (int)(souls * (8d / 9d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.AstrumDeus.ID)
                {
                    boss = true;
                    NPC astrumDeus = new(); astrumDeus.SetDefaults(npcID);
                    souls = astrumDeus.lifeMax;
                    if (CalamityDownedBossSystem.AstrumDeus.Downed) // 4000
                        souls = (int)(souls * (1d / 150d));
                    else // 45000
                        souls = (int)(souls * (3d / 40d));
                    return souls;
                }
                #endregion
                #region Post-Moon Lord
                else if (npcID == CalamityDownedBossSystem.ProfanedGuardians.ID)
                {
                    boss = true;
                    NPC guardianCommander = new(); guardianCommander.SetDefaults(npcID);
                    souls = guardianCommander.lifeMax;
                    if (CalamityDownedBossSystem.ProfanedGuardians.Downed) // 5000
                        souls = (int)(souls * (1d / 20d));
                    else // 52000
                        souls = (int)(souls * (13d / 25d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.Dragonfolly.ID)
                {
                    if (Main.zenithWorld && (NPC.AnyNPCs(CalamityDownedBossSystem.YharonDragonOfRebirth.ID) || NPC.AnyNPCs(CalamityDownedBossSystem.SupremeWitchCalamitas.ID))) // Yharon and Permafrost in GFB spawn many Dragonfolly
                        return 0;
                    boss = true;
                    NPC dragonfolly = new(); dragonfolly.SetDefaults(npcID);
                    souls = dragonfolly.lifeMax;
                    if (CalamityDownedBossSystem.Dragonfolly.Downed) // 5250
                        souls = (int)(souls * (7d / 250d));
                    else // 54500
                        souls = (int)(souls * (109d / 375d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.ProvidenceTheProfanedGoddess.ID)
                {
                    boss = true;
                    NPC providence = new(); providence.SetDefaults(npcID);
                    souls = providence.lifeMax;
                    if (CalamityDownedBossSystem.ProvidenceTheProfanedGoddess.Downed) // 5500
                        souls = (int)(souls * (11d / 625d));
                    else // 57000
                        souls = (int)(souls * (114d / 625d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.StormWeaver.ID)
                {
                    boss = true;
                    NPC stormWeaver = new(); stormWeaver.SetDefaults(npcID);
                    souls = stormWeaver.lifeMax;
                    if (CalamityDownedBossSystem.StormWeaver.Downed) // 5700
                        souls = (int)(souls * (19d / 2750d));
                    else // 59000
                        souls = (int)(souls * (59d / 825d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.CeaselessVoid.ID)
                {
                    boss = true;
                    NPC ceaselessVoid = new(); ceaselessVoid.SetDefaults(npcID);
                    souls = ceaselessVoid.lifeMax;
                    if (CalamityDownedBossSystem.CeaselessVoid.Downed) // 5750
                        souls = (int)(souls * (23d / 260d));
                    else // 60000
                        souls = (int)(souls * (12d / 13d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.SignusEnvoyOfTheDevourer.ID)
                {
                    boss = true;
                    NPC signus = new(); signus.SetDefaults(npcID);
                    souls = signus.lifeMax;
                    if (CalamityDownedBossSystem.SignusEnvoyOfTheDevourer.Downed) // 5800
                        souls = (int)(souls * (29d / 1500d));
                    else // 61000
                        souls = (int)(souls * (61d / 300d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.Polterghast.ID)
                {
                    boss = true;
                    NPC polterghast = new(); polterghast.SetDefaults(npcID);
                    souls = polterghast.lifeMax;
                    if (CalamityDownedBossSystem.Polterghast.Downed) // 6000
                        souls = (int)(souls * (3d / 175d));
                    else // 62500
                        souls = (int)(souls * (5d / 28d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.TheOldDuke.ID)
                {
                    boss = true;
                    NPC theOldDuke = new(); theOldDuke.SetDefaults(npcID);
                    souls = theOldDuke.lifeMax;
                    if (CalamityDownedBossSystem.TheOldDuke.Downed) // 6300
                        souls = (int)(souls * (63d / 5000d));
                    else // 65000
                        souls = (int)(souls * (13d / 100d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.TheDevourerOfGods.ID)
                {
                    boss = true;
                    NPC theDevourerOfGods = new(); theDevourerOfGods.SetDefaults(npcID);
                    souls = theDevourerOfGods.lifeMax;
                    if (CalamityDownedBossSystem.TheDevourerOfGods.Downed) // 6600
                        souls = (int)(souls * (66d / 8875d));
                    else // 70000
                        souls = (int)(souls * (28d / 355d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.YharonDragonOfRebirth.ID)
                {
                    boss = true;
                    NPC yharonDragonOfRebirth = new(); yharonDragonOfRebirth.SetDefaults(npcID);
                    souls = yharonDragonOfRebirth.lifeMax;
                    if (CalamityDownedBossSystem.YharonDragonOfRebirth.Downed) // 7000
                        souls = (int)(souls * (7d / 1300d));
                    else // 80000
                        souls = (int)(souls * (4d / 65d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.SupremeWitchCalamitas.ID)
                {
                    boss = true;
                    NPC supremeWitchCalamitas = new(); supremeWitchCalamitas.SetDefaults(npcID);
                    souls = supremeWitchCalamitas.lifeMax;
                    if (CalamityDownedBossSystem.SupremeWitchCalamitas.Downed) // 8500
                        souls = (int)(souls * (17d / 1920d));
                    else // 100000
                        souls = (int)(souls * (5d / 48d));
                    return souls;
                }
                else if (CalamityDownedBossSystem.IsExoMech(npc))
                {
                    if (ExoMechsSoulsReward && npcID == CalamityDownedBossSystem.Ares.ID) // Reset the reward flag when any of the Exo Mechs is killed, in this case I chose Ares
                        ExoMechsSoulsReward = false;

                    if (!CalamityDownedBossSystem.AreExoMechsAlive(npc) && !ExoMechsSoulsReward)
                    {
                        boss = true;
                        ExoMechsSoulsReward = true;
                        NPC apollo = new(); apollo.SetDefaults(CalamityDownedBossSystem.Apollo.ID);
                        NPC ares = new(); ares.SetDefaults(CalamityDownedBossSystem.Ares.ID);
                        NPC thanatos = new(); thanatos.SetDefaults(CalamityDownedBossSystem.Thanatos.ID);
                        souls = apollo.lifeMax + ares.lifeMax + thanatos.lifeMax;
                        if (CalamityDownedBossSystem.downedExoMechs) // 8500
                            return (int)(souls * (17d / 6920d));
                        else // 100000
                            return (int)(souls * (5d / 173d));
                    }
                    return 0;
                }
                else if (npcID == CalamityNPCID.GetCalamityNPCIDByName("Draedon"))
                    return 666; // lmao

                #endregion
                #region Hidden / Mini bosses
                else if (npcID == CalamityDownedBossSystem.GiantClam.ID)
                {
                    boss = true;
                    NPC giantClam = new(); giantClam.SetDefaults(npcID);
                    souls = giantClam.lifeMax; // Hardmode: 7500, Pre-Hardmode: 1250
                    if (CalamityDownedBossSystem.GiantClam.Downed)
                    {
                        if (Main.hardMode) // 875
                            souls = (int)(souls * (7d / 60d));
                        else // 375
                            souls = (int)(souls * (3d / 10d));
                    }
                    else
                    {
                        if (Main.hardMode) // 8400
                            souls = (int)(souls * (28d / 25d));
                        else // 3000
                            souls = (int)(souls * (12d / 5d));
                    }
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.GreatSandShark.ID)
                {
                    boss = true;
                    NPC greatSandShark = new(); greatSandShark.SetDefaults(npcID);
                    souls = greatSandShark.lifeMax;
                    if (CalamityDownedBossSystem.GreatSandShark.Downed) // 1000
                        souls = (int)(souls * (5d / 46d));
                    else // 12000
                        souls = (int)(souls * (30d / 32d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.CragmawMire.ID)
                {
                    boss = true;
                    NPC cragmawMire = new(); cragmawMire.SetDefaults(npcID);
                    souls = cragmawMire.lifeMax; // T2: 4000, T3: 80630
                    if (CalamityDownedBossSystem.CragmawMire.Downed)
                    {
                        if (CalamityDownedBossSystem.Polterghast.Downed) // 2250, T3
                            souls = (int)(souls * (225d / 8063d));
                        else // 800, T2
                            souls = (int)(souls * (1d / 5d));
                    }
                    else // first kill
                    {
                        if (CalamityDownedBossSystem.Polterghast.Downed) // 20000, T3
                            souls = (int)(souls * (2000d / 8063d));
                        else // 6000, T2
                            souls = (int)(souls * (3d / 2d));
                    }
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.Mauler.ID || npcID == CalamityDownedBossSystem.NuclearTerror.ID)
                {
                    boss = true;
                    NPC mauler = new(); mauler.SetDefaults(CalamityDownedBossSystem.Mauler.ID);
                    souls = mauler.lifeMax;
                    if ((CalamityDownedBossSystem.Mauler.Downed && npcID == CalamityDownedBossSystem.Mauler.ID) ||
                        (CalamityDownedBossSystem.NuclearTerror.Downed && npcID == CalamityDownedBossSystem.NuclearTerror.ID)
                    ) // 2400
                        souls = (int)(souls * (2d / 75d));
                    else // 22000
                        souls = (int)(souls * (11d / 45d));
                    return souls;
                }
                else if (npcID == CalamityDownedBossSystem.PrimordialWyrm.ID)
                {
                    boss = true;
                    NPC primordialWyrm = new(); primordialWyrm.SetDefaults(npcID);
                    souls = primordialWyrm.lifeMax;
                    if (CalamityDownedBossSystem.PrimordialWyrm.Downed) // 10000
                        souls = (int)(souls * (1d / 250d));
                    else // 150000
                        souls = (int)(souls * (3d / 50d));
                    return souls;
                }
                else if (npcID == CalamityNPCID.THELORDE)
                {
                    boss = true;
                    return 222222;
                }
                #endregion
                #region Abyss Enemies
                else if (AbyssNPCSoulsMultipliers.TryGetValue(npcID, out var multiplier))
                {
                    NPC enemy = new();
                    enemy.SetDefaults(npcID);
                    return (int)(enemy.lifeMax * multiplier);
                }
                #endregion
            }
            else // bosses that have not been manually handled
                boss = npc.boss;

            // Blacklist
            if (NPCIDBlackList.Contains(npcID))
                return 0;

            // Other NPCs
            int statLife, maxStatLife;
            npc.GetLifeStats(out statLife, out maxStatLife);
            return maxStatLife;
        }

        private static bool AreMechBossesAlive(NPC killedMech)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (killedMech != null && npc.whoAmI == killedMech.whoAmI)
                    continue;

                if (npc.type == NPCID.Spazmatism && npc.life > 0 && npc.active)
                    return true;
                else if (npc.type == NPCID.Retinazer && npc.life > 0 && npc.active)
                    return true;
                else if (npc.type == NPCID.SkeletronPrime && npc.life > 0 && npc.active)
                    return true;
                else if (npc.type == NPCID.TheDestroyer && npc.life > 0 && npc.active)
                    return true;
            }
            return false;
        }
    }
}