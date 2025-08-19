using DarkSouls.Utils;
using System.Collections.Generic;
using System.Security.Cryptography;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarkSouls.Systems
{
    sealed class CalamityDownedBossSystem : ModSystem
    {
        public enum CalamityBoss
        {
            // Pre-Hardmode
            DesertScourge,
            Crabulon,
            TheHiveMind,
            ThePerforators,
            TheSlimeGod,

            // Hardmode
            Cryogen,
            AquaticScourge,
            BrimstoneElemental,
            CalamitasClone,
            LeviathanAndAnahita,
            AstrumAureus,
            ThePlaguebringerGoliath,
            Ravager,
            AstrumDeus,

            // Post-Moon Lord
            ProfanedGuardians,
            Dragonfolly,
            ProvidenceTheProfanedGoddess,
            StormWeaver,
            CeaselessVoid,
            SignusEnvoyOfTheDevourer,
            Polterghast,
            TheOldDuke,
            TheDevourerOfGods,
            YharonDragonOfRebirth,
            SupremeWitchCalamitas,

            // ExoMechs (special case)
            Apollo,
            Artemis,
            Ares,
            Thanatos,

            // Hidden Bosses
            PrimordialWyrm,

            // Mini-bosses
            GiantClam,
            GreatSandShark,
            Mauler,
            NuclearTerror,
            CragmawMire
        }

        public readonly struct BossData
        {
            public readonly string InternalCalamityName;
            public readonly int ID;
            public readonly bool Downed;

            public BossData(string internalName, int id, bool downed)
            {
                InternalCalamityName = internalName;
                ID = id;
                Downed = downed;
            }
        }

        private static readonly Dictionary<CalamityBoss, BossData> _bosses = new();

        // Properties for Pre-Hardmode
        public static BossData DesertScourge => GetBoss(CalamityBoss.DesertScourge);
        public static BossData Crabulon => GetBoss(CalamityBoss.Crabulon);
        public static BossData TheHiveMind => GetBoss(CalamityBoss.TheHiveMind);
        public static BossData ThePerforators => GetBoss(CalamityBoss.ThePerforators);
        public static BossData TheSlimeGod => GetBoss(CalamityBoss.TheSlimeGod);

        // Hardmode
        public static BossData Cryogen => GetBoss(CalamityBoss.Cryogen);
        public static BossData AquaticScourge => GetBoss(CalamityBoss.AquaticScourge);
        public static BossData BrimstoneElemental => GetBoss(CalamityBoss.BrimstoneElemental);
        public static BossData CalamitasClone => GetBoss(CalamityBoss.CalamitasClone);
        public static BossData LeviathanAndAnahita => GetBoss(CalamityBoss.LeviathanAndAnahita);
        public static BossData AstrumAureus => GetBoss(CalamityBoss.AstrumAureus);
        public static BossData ThePlaguebringerGoliath => GetBoss(CalamityBoss.ThePlaguebringerGoliath);
        public static BossData Ravager => GetBoss(CalamityBoss.Ravager);
        public static BossData AstrumDeus => GetBoss(CalamityBoss.AstrumDeus);

        // Post-Moon Lord
        public static BossData ProfanedGuardians => GetBoss(CalamityBoss.ProfanedGuardians);
        public static BossData Dragonfolly => GetBoss(CalamityBoss.Dragonfolly);
        public static BossData ProvidenceTheProfanedGoddess => GetBoss(CalamityBoss.ProvidenceTheProfanedGoddess);
        public static BossData StormWeaver => GetBoss(CalamityBoss.StormWeaver);
        public static BossData CeaselessVoid => GetBoss(CalamityBoss.CeaselessVoid);
        public static BossData SignusEnvoyOfTheDevourer => GetBoss(CalamityBoss.SignusEnvoyOfTheDevourer);
        public static BossData Polterghast => GetBoss(CalamityBoss.Polterghast);
        public static BossData TheOldDuke => GetBoss(CalamityBoss.TheOldDuke);
        public static BossData TheDevourerOfGods => GetBoss(CalamityBoss.TheDevourerOfGods);
        public static BossData YharonDragonOfRebirth => GetBoss(CalamityBoss.YharonDragonOfRebirth);
        public static BossData SupremeWitchCalamitas => GetBoss(CalamityBoss.SupremeWitchCalamitas);

        // Exo Mechs (special case)
        public static BossData Apollo => GetBoss(CalamityBoss.Apollo);
        public static BossData Artemis => GetBoss(CalamityBoss.Artemis);
        public static BossData Ares => GetBoss(CalamityBoss.Ares);
        public static BossData Thanatos => GetBoss(CalamityBoss.Thanatos);
        public static bool downedExoMechs { get; private set; }

        // Hidden / Mini bosses
        public static BossData GiantClam => GetBoss(CalamityBoss.GiantClam);
        public static BossData GreatSandShark => GetBoss(CalamityBoss.GreatSandShark);
        public static BossData Mauler => GetBoss(CalamityBoss.Mauler);
        public static BossData NuclearTerror => GetBoss(CalamityBoss.NuclearTerror);
        public static BossData CragmawMire => GetBoss(CalamityBoss.CragmawMire);
        public static BossData PrimordialWyrm => GetBoss(CalamityBoss.PrimordialWyrm);

        public static void Initialize()
        {
            if (!DarkSouls.CalamityModIsEnabled)
                return;

            // Pre-Hardmode
            _bosses[CalamityBoss.DesertScourge] = new BossData("desert scourge", CalamityNPCID.GetCalamityNPCIDByName("DesertScourgeHead"), false);
            _bosses[CalamityBoss.Crabulon] = new BossData("crabulon", CalamityNPCID.GetCalamityNPCIDByName("Crabulon"), false);
            _bosses[CalamityBoss.TheHiveMind] = new BossData("the hive mind", CalamityNPCID.GetCalamityNPCIDByName("HiveMind"), false);
            _bosses[CalamityBoss.ThePerforators] = new BossData("the perforators", CalamityNPCID.GetCalamityNPCIDByName("PerforatorHive"), false);
            _bosses[CalamityBoss.TheSlimeGod] = new BossData("the slime god", CalamityNPCID.GetCalamityNPCIDByName("SlimeGodCore"), false);

            // Hardmode
            _bosses[CalamityBoss.Cryogen] = new BossData("cryogen", CalamityNPCID.GetCalamityNPCIDByName("Cryogen"), false);
            _bosses[CalamityBoss.AquaticScourge] = new BossData("aquatic scourge", CalamityNPCID.GetCalamityNPCIDByName("AquaticScourgeHead"), false);
            _bosses[CalamityBoss.BrimstoneElemental] = new BossData("brimstone elemental", CalamityNPCID.GetCalamityNPCIDByName("BrimstoneElemental"), false);
            _bosses[CalamityBoss.CalamitasClone] = new BossData("calamitas clone", CalamityNPCID.GetCalamityNPCIDByName("CalamitasClone"), false);
            _bosses[CalamityBoss.LeviathanAndAnahita] = new BossData("anahita and leviathan", CalamityNPCID.GetCalamityNPCIDByName("Anahita"), false);
            _bosses[CalamityBoss.AstrumAureus] = new BossData("astrum aureus", CalamityNPCID.GetCalamityNPCIDByName("AstrumAureus"), false);
            _bosses[CalamityBoss.ThePlaguebringerGoliath] = new BossData("plaguebringer goliath", CalamityNPCID.GetCalamityNPCIDByName("PlaguebringerGoliath"), false);
            _bosses[CalamityBoss.Ravager] = new BossData("ravager", CalamityNPCID.GetCalamityNPCIDByName("RavagerBody"), false);
            _bosses[CalamityBoss.AstrumDeus] = new BossData("astrum deus", CalamityNPCID.GetCalamityNPCIDByName("AstrumDeusHead"), false);

            // Post-Moon Lord
            _bosses[CalamityBoss.ProfanedGuardians] = new BossData("profaned guardians", CalamityNPCID.GetCalamityNPCIDByName("ProfanedGuardianCommander"), false);
            _bosses[CalamityBoss.Dragonfolly] = new BossData("dragonfolly", CalamityNPCID.GetCalamityNPCIDByName("Bumblefuck"), false);
            _bosses[CalamityBoss.ProvidenceTheProfanedGoddess] = new BossData("providence", CalamityNPCID.GetCalamityNPCIDByName("Providence"), false);
            _bosses[CalamityBoss.StormWeaver] = new BossData("storm weaver", CalamityNPCID.GetCalamityNPCIDByName("StormWeaverHead"), false);
            _bosses[CalamityBoss.CeaselessVoid] = new BossData("ceaseless void", CalamityNPCID.GetCalamityNPCIDByName("CeaselessVoid"), false);
            _bosses[CalamityBoss.SignusEnvoyOfTheDevourer] = new BossData("signus", CalamityNPCID.GetCalamityNPCIDByName("Signus"), false);
            _bosses[CalamityBoss.Polterghast] = new BossData("polterghast", CalamityNPCID.GetCalamityNPCIDByName("Polterghast"), false);
            _bosses[CalamityBoss.TheOldDuke] = new BossData("the old duke", CalamityNPCID.GetCalamityNPCIDByName("OldDuke"), false);
            _bosses[CalamityBoss.TheDevourerOfGods] = new BossData("devourer of gods", CalamityNPCID.GetCalamityNPCIDByName("DevourerofGodsHead"), false);
            _bosses[CalamityBoss.YharonDragonOfRebirth] = new BossData("yharon", CalamityNPCID.GetCalamityNPCIDByName("Yharon"), false);
            _bosses[CalamityBoss.SupremeWitchCalamitas] = new BossData("supreme witch calamitas", CalamityNPCID.GetCalamityNPCIDByName("SupremeCalamitas"), false);

            // Exo Mechs (special case)
            _bosses[CalamityBoss.Apollo] = new BossData("exo twins", CalamityNPCID.GetCalamityNPCIDByName("Apollo"), false);
            _bosses[CalamityBoss.Artemis] = new BossData("exo twins", CalamityNPCID.GetCalamityNPCIDByName("Artemis"), false);
            _bosses[CalamityBoss.Ares] = new BossData("ares", CalamityNPCID.GetCalamityNPCIDByName("AresBody"), false);
            _bosses[CalamityBoss.Thanatos] = new BossData("thanatos", CalamityNPCID.GetCalamityNPCIDByName("ThanatosHead"), false);

            // Hidden / Mini bosses
            _bosses[CalamityBoss.GiantClam] = new BossData("giant clam", CalamityNPCID.GetCalamityNPCIDByName("GiantClam"), false);
            _bosses[CalamityBoss.GreatSandShark] = new BossData("great sand shark", CalamityNPCID.GetCalamityNPCIDByName("GreatSandShark"), false);
            _bosses[CalamityBoss.Mauler] = new BossData("mauler", CalamityNPCID.GetCalamityNPCIDByName("Mauler"), false);
            _bosses[CalamityBoss.NuclearTerror] = new BossData("nuclear terror", CalamityNPCID.GetCalamityNPCIDByName("NuclearTerror"), false);
            _bosses[CalamityBoss.CragmawMire] = new BossData("cragmaw mire", CalamityNPCID.GetCalamityNPCIDByName("CragmawMire"), false);
            _bosses[CalamityBoss.PrimordialWyrm] = new BossData("primordial wyrm", CalamityNPCID.GetCalamityNPCIDByName("PrimordialWyrmHead"), false); // Downed always false (Calamity bug?)
        }

        private static BossData GetBoss(CalamityBoss boss) => _bosses.TryGetValue(boss, out var data) ? data : new BossData("", NPCID.None, false);

        internal static void RefreshDownedInfo(NPC npc = null, bool refreshExoMechs = false)
        {
            if (refreshExoMechs)
                downedExoMechs = ModSupportUtils.GetCalamityBossDowned("the exo mechs");
            foreach (var boss in _bosses.Keys)
            {
                var old = _bosses[boss];
                if (npc == null || npc.type == old.ID)
                {
                    bool downed = ModSupportUtils.GetCalamityBossDowned(old.InternalCalamityName);
                    _bosses[boss] = new BossData(old.InternalCalamityName, old.ID, downed);

                    if (npc != null)
                        return;
                }
            }
        }

        internal static bool IsExoMech(NPC npc) => npc.type == Apollo.ID || npc.type == Artemis.ID || npc.type == Ares.ID || npc.type == Thanatos.ID;

        internal static bool AreExoMechsAlive(NPC killedMech = null)
        {
            bool apolloAlive = false;
            bool artemisAlive = false;
            bool aresAlive = false;
            bool thanatosAlive = false;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                
                NPC npc = Main.npc[i];
                if (killedMech != null && npc.whoAmI == killedMech.whoAmI)
                    continue;

                if (npc.type == Apollo.ID && npc.life > 0 && npc.active)
                    apolloAlive = true;
                else if (npc.type == Artemis.ID && npc.life > 0 && npc.active)
                    artemisAlive = true;
                else if (npc.type == Ares.ID && npc.life > 0 && npc.active)
                    aresAlive = true;
                else if (npc.type == Thanatos.ID && npc.life > 0 && npc.active)
                    thanatosAlive = true;
            }

            if (!apolloAlive || !artemisAlive)
            {
                apolloAlive = false;
                artemisAlive = false;
            }

            return apolloAlive || artemisAlive || aresAlive || thanatosAlive;
        }
    }

    sealed class DownedInfoUpdater : ModPlayer
    {
        public override void OnEnterWorld()
        {
            if(!DarkSouls.CalamityModIsEnabled)
                return;

            CalamityDownedBossSystem.RefreshDownedInfo(refreshExoMechs: true);
        }
    }
}