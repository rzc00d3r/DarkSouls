using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;

using ReLogic.Content;

using DarkSouls.Config;

namespace DarkSouls.Tiles.DarkSoulsBonfire
{
    public class DarkSoulsBonfireMapLayer : ModMapLayer
    {
        private Asset<Texture2D> bonfireIcon;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                bonfireIcon = ModContent.Request<Texture2D>("DarkSouls/Tiles/DarkSoulsBonfire/DarkSoulsBonfireMapIcon");
            }
        }

        public override void Unload()
        {
            bonfireIcon = null;
        }

        public override void Draw(ref MapOverlayDrawContext context, ref string text)
        {
            Player player = Main.LocalPlayer;
            DarkSoulsPlayer dsPlayer = player.GetModPlayer<DarkSoulsPlayer>();

            if (!Main.hardMode)
                return;

            foreach (var tile in TileEntity.ByID.Values.OfType<DarkSoulsBonfireTileEntity>())
            {
                Vector2 position = new Vector2(tile.Position.X + 1.5f, tile.Position.Y + 1.5f);

                if (context.Draw(bonfireIcon.Value, position, Color.White, new SpriteFrame(1, 1, 0, 0), 0.45f, 0.65f, Alignment.Center).IsMouseOver)
                {
                    text = Language.GetTextValue("Mods.DarkSouls.Tiles.DarkSoulsBonfire.DisplayName");

                    int tpCost = ServerConfig.Instance.BonfireTeleportHumanityCost;
                    if (!NPC.downedMoonlord && tpCost > 0)
                        text += '\n' + Language.GetTextValue("Mods.DarkSouls.Tiles.DarkSoulsBonfire.RequiresHumanity", tpCost);

                    if (Main.mouseLeft && Main.mouseLeftRelease && CanTeleportToBonfire())
                    {
                        Main.mapFullscreen = false;

                        float сenterX = tile.Position.X * 16f + 24f;
                        float bottomY = (tile.Position.Y + 3) * 16f;

                        Vector2 tpLocation = new Vector2(
                            сenterX - (player.width / 2f),
                            bottomY - player.height
                        );

                        player.Teleport(tpLocation, 1);
                        dsPlayer.dsHumanity -= tpCost;
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                            dsPlayer.SyncHumanity();
                    }
                }
            }
        }

        private bool IsNearBonfire(float bonfireRange = 120f)
        {
            foreach (var tile in TileEntity.ByID.Values.OfType<DarkSoulsBonfireTileEntity>())
            {
                Vector2 position = new Vector2(tile.Position.X + 1.5f, tile.Position.Y + 1.5f) * 16f;

                if (Vector2.Distance(Main.LocalPlayer.Center, position) <= bonfireRange)
                    return true;
            }

            return false;
        }

        private bool CanTeleportToBonfire()
        {
            Player player = Main.LocalPlayer;
            DarkSoulsPlayer dsPlayer = player.GetModPlayer<DarkSoulsPlayer>();

            if (!IsNearBonfire())
            {
                Main.NewText(Language.GetTextValue("Mods.DarkSouls.Tiles.DarkSoulsBonfire.NotNearBonfire"), Color.Orange);
                return false;
            }

            if (!NPC.downedMoonlord && ServerConfig.Instance.BonfireTeleportHumanityCost > dsPlayer.dsHumanity)
            {
                Main.NewText(Language.GetTextValue("Mods.DarkSouls.Tiles.DarkSoulsBonfire.NotEnoughHumanity"), Color.DarkRed);
                return false;

            }

            return true;
        }
    }
}