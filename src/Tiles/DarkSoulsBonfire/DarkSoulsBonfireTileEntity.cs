using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarkSouls.Tiles.DarkSoulsBonfire
{
    public class DarkSoulsBonfireTileEntity : ModTileEntity
    {
        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.HasTile && tile.TileType == ModContent.TileType<DarkSoulsBonfire>();
        }

        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
        {
            int topLeftX = i - 1;
            int topLeftY = j - 2;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendTileSquare(Main.myPlayer, topLeftX, topLeftY, 3, 3);
                NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, topLeftX, topLeftY, Type);
                return -1;
            }

            return Place(topLeftX, topLeftY);
        }
    }
}