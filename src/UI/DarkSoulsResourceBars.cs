using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent;

using ReLogic.Content;
using ReLogic.Graphics;

namespace DarkSouls.UI
{
    public class CustomResourceBars : ModResourceDisplaySet
    {
        private Asset<Texture2D> emptyBar;

        private int barWidth;
        private int barHeight;
        private int barBorderOffset;

        private int segments;

        public override void Load()
        {
            if (!Main.dedServ)
                emptyBar = ModContent.Request<Texture2D>("DarkSouls/UI/Textures/Bars/EmptyBar");
        }

        public override void SetStaticDefaults()
        {
            barWidth = emptyBar.Width(); // 226
            barHeight = emptyBar.Height(); // 14
            barBorderOffset = 3;
            segments = barWidth - (barBorderOffset * 2); // 220
        }

        public override bool PreHover(out bool hoveringLife)
        {
            hoveringLife = true;
            return false;
        }

        public override void DrawLife(SpriteBatch spriteBatch)
        {
            DrawHealthBar(spriteBatch);
            DrawManaBar(spriteBatch);
            DrawStaminaBar(spriteBatch);
        }

        public override void DrawMana(SpriteBatch spriteBatch) { }

        private float GetDrawPositionX() => Main.screenWidth - 296f; // Upper-left corner of mini map

        private void DrawHealthBar(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            DarkSoulsPlayer dsPlayer = player.GetModPlayer<DarkSoulsPlayer>();

            Vector2 position = new Vector2(GetDrawPositionX(), 20f);
            spriteBatch.Draw(emptyBar.Value, position, Color.White);

            if (player.statLifeMax2 > 0)
            {
                float usedHpWidth = ((float)dsPlayer.usedHP / player.statLifeMax2) * segments;
                float hpWidth = ((float)player.statLife / player.statLifeMax2) * segments;

                float innerX = position.X + barBorderOffset;
                float innerY = position.Y + barBorderOffset;

                Color usedTop = new Color(170, 130, 20); 
                Color usedBottom = new Color(90, 70, 10); 

                if (usedHpWidth > 0f)
                    DrawGradFill(spriteBatch, innerX, innerY, usedHpWidth, usedTop, usedBottom);
                
                Color hpTop = new Color(150, 30, 30); 
                Color hpBottom = new Color(80, 10, 10); 

                if (hpWidth > 0f)
                    DrawGradFill(spriteBatch, innerX, innerY, hpWidth, hpTop, hpBottom);

            }

            spriteBatch.Draw(emptyBar.Value, position, Color.White);
            DrawBarDividers(spriteBatch, position, 4);

            Vector2 textPos = position + new Vector2(barWidth + 5f, -1f);
            spriteBatch.DrawString(FontAssets.MouseText.Value, $"{player.statLife}/{player.statLifeMax2}", textPos, Color.WhiteSmoke, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
        }

        private void DrawManaBar(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            DarkSoulsPlayer dsPlayer = player.GetModPlayer<DarkSoulsPlayer>();

            Vector2 position = new Vector2(GetDrawPositionX(), 20f + barHeight + 3f);
            spriteBatch.Draw(emptyBar.Value, position, Color.White);

            if (player.statManaMax2 > 0)
            {
                float usedManaWidth = ((float)dsPlayer.usedMana / player.statManaMax2) * segments;
                float manaWidth = ((float)player.statMana / player.statManaMax2) * segments;

                float innerX = position.X + barBorderOffset;
                float innerY = position.Y + barBorderOffset;

                Color usedTop = new Color(170, 130, 20); 
                Color usedBottom = new Color(90, 70, 10); 

                if (usedManaWidth > 0f)
                    DrawGradFill(spriteBatch, innerX, innerY, usedManaWidth, usedTop, usedBottom);

                Color manaTop = new Color(50, 90, 150); 
                Color manaBottom = new Color(20, 35, 80); 

                if (manaWidth > 0f)
                    DrawGradFill(spriteBatch, innerX, innerY, manaWidth, manaTop, manaBottom);
            }

            spriteBatch.Draw(emptyBar.Value, position, Color.White);
            DrawBarDividers(spriteBatch, position, 4);

            Vector2 textPos = position + new Vector2(barWidth + 5f, -1f);
            spriteBatch.DrawString(FontAssets.MouseText.Value, $"{player.statMana}/{player.statManaMax2}", textPos, Color.WhiteSmoke, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
        }

        private void DrawStaminaBar(SpriteBatch spriteBatch)
        {
            DarkSoulsPlayer dsPlayer = Main.LocalPlayer.GetModPlayer<DarkSoulsPlayer>();

            Vector2 position = new Vector2(GetDrawPositionX(), 20f + 2f * (barHeight + 3f));
            spriteBatch.Draw(emptyBar.Value, position, Color.White);

            if (dsPlayer.maxStamina > 0)
            {
                float usedStamWidth = (dsPlayer.usedStamina / dsPlayer.maxStamina) * segments;
                float stamWidth = (dsPlayer.currentStamina / dsPlayer.maxStamina) * segments;

                float innerX = position.X + barBorderOffset;
                float innerY = position.Y + barBorderOffset;

                Color usedTop = new Color(170, 130, 20); 
                Color usedBottom = new Color(90, 70, 10); 

                if (usedStamWidth > 0f)
                    DrawGradFill(spriteBatch, innerX, innerY, usedStamWidth, usedTop, usedBottom);

                Color stamTop = new Color(60, 120, 60); 
                Color stamBottom = new Color(25, 60, 25); 

                if (stamWidth > 0f)
                    DrawGradFill(spriteBatch, innerX, innerY, stamWidth, stamTop, stamBottom);
            }

            spriteBatch.Draw(emptyBar.Value, position, Color.White);
            DrawBarDividers(spriteBatch, position, 4);

            Vector2 textPos = position + new Vector2(barWidth + 5f, -1f);
            spriteBatch.DrawString(FontAssets.MouseText.Value, $"{(int)dsPlayer.currentStamina}/{(int)dsPlayer.maxStamina}", textPos, Color.WhiteSmoke, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
        }

        private void DrawGradFill(SpriteBatch spriteBatch, float x, float y, float width, Color topColor, Color bottomColor)
        {
            Texture2D magicPixel = TextureAssets.MagicPixel.Value;

            int totalInnerHeight = barHeight - (barBorderOffset * 2);

            int ix = (int)Math.Floor(x);
            int iy = (int)Math.Floor(y) - 1;
            int iWidth = (int)Math.Ceiling(width);
            int iHeight = totalInnerHeight + 2;

            int steps = 4;
            float heightPerStep = (float)iHeight / steps;

            for (int i = 0; i < steps; i++)
            {
                float amount = (float)i / (steps - 1);
                Color stepColor = Color.Lerp(topColor, bottomColor, amount);

                int startY = iy + (int)Math.Floor(i * heightPerStep);
                int endY = iy + (int)Math.Floor((i + 1) * heightPerStep);
                int stepH = endY - startY;

                spriteBatch.Draw(magicPixel, new Rectangle(ix, startY, iWidth, stepH), stepColor);
            }
        }

        private void DrawBarDividers(SpriteBatch spriteBatch, Vector2 position, int sectionCount)
        {
            if (sectionCount <= 1)
                return;

            float sectionWidth = (float)segments / sectionCount;

            int innerX = (int)position.X + barBorderOffset;
            int innerY = (int)position.Y + barBorderOffset;

            for (int i = 1; i < sectionCount; i++)
            {
                int divX = innerX + (int)Math.Round(sectionWidth * i);

                Rectangle divRect = new Rectangle(divX, innerY, 2, barHeight - (barBorderOffset * 2));
                spriteBatch.Draw(TextureAssets.MagicPixel.Value, divRect, new Color(0, 0, 0, 180));
            }
        }
    }
}