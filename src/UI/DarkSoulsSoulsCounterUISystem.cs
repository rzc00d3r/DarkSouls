using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;

namespace DarkSouls.UI
{
    [Autoload(true, Side = ModSide.Client)]
    public class DarkSoulsSoulsCounterUISystem : ModSystem
    {
        private UserInterface soulsCounterInterface;
        private DarkSoulsSoulsCounterUI soulsCounterUI;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                soulsCounterUI = new DarkSoulsSoulsCounterUI();
                soulsCounterInterface = new UserInterface();

                soulsCounterInterface.SetState(soulsCounterUI);
            }
        }

        public override void Unload()
        {
            soulsCounterUI = null;
            soulsCounterInterface = null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (soulsCounterInterface != null)
                soulsCounterInterface.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "Dark Souls: Souls Counter",
                    delegate
                    {
                        if (soulsCounterInterface != null)
                            soulsCounterInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}