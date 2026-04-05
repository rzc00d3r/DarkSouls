using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.UI;
using Terraria.Audio;
using Terraria.ModLoader;

using ReLogic.Graphics;
using DarkSouls.Config;

namespace DarkSouls.UI
{
    public class DarkSoulsSoulsCounterUI : UIState
    {
        private long visualSouls = 0;
        private long targetSouls = 0;
        private long expectedRealSouls = 0;

        private long lastAddedSouls = 0;
        private float textAlpha = 0f;
        private int alphaState = 0; // 0 - hidden, 1 - appears, 2 - visible, 3 - fades
        private float displayTimer = 0f;

        private long bufferedSouls = 0;
        private float bufferTimer = 0f;

        private Texture2D bgTexture;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            DarkSoulsPlayer dsPlayer = Main.LocalPlayer.GetModPlayer<DarkSoulsPlayer>();
            long currentRealSouls = dsPlayer.dsSouls;

            if (!ClientConfig.Instance.EnableSoulsCounterUI || dsPlayer.instantSoulsCounterUpdate)
            {
                visualSouls = currentRealSouls;
                targetSouls = currentRealSouls;
                expectedRealSouls = currentRealSouls;
                bufferedSouls = 0;
                bufferTimer = 0f;
                lastAddedSouls = 0;
                textAlpha = 0f;
                alphaState = 0;
                dsPlayer.instantSoulsCounterUpdate = false;
                return;
            }

            if (currentRealSouls != expectedRealSouls)
            {
                long diff = currentRealSouls - expectedRealSouls;

                if (diff > 0)
                {
                    if (alphaState > 0)
                    {
                        targetSouls += diff;
                        lastAddedSouls += diff;
                        displayTimer = 0f;

                        if (alphaState == 3)
                            alphaState = 1;
                    }
                    else
                    {
                        if (bufferedSouls == 0)
                            bufferTimer = 1.5f;

                        bufferedSouls += diff;
                    }
                }
                else
                {
                    visualSouls = currentRealSouls;
                    targetSouls = currentRealSouls;
                    bufferedSouls = 0;
                    bufferTimer = 0f;
                    lastAddedSouls = 0;
                    textAlpha = 0f;
                    alphaState = 0;
                }

                expectedRealSouls = currentRealSouls;
            }

            if (bufferedSouls > 0)
            {
                bufferTimer -= 1f / 60f;
                if (bufferTimer <= 0f)
                {
                    targetSouls += bufferedSouls;
                    lastAddedSouls += bufferedSouls;
                    bufferedSouls = 0;

                    SoundEngine.PlaySound(DarkSouls.dsSoulSuck, Main.LocalPlayer.position);

                    if (alphaState == 0 || alphaState == 3)
                        alphaState = 1;

                    displayTimer = 0f;
                }
            }

            if (alphaState == 1)
            {
                textAlpha += 0.05f;
                if (textAlpha >= 1f)
                {
                    textAlpha = 1f;
                    alphaState = 2;
                }
            }
            else if (alphaState == 3)
            {
                textAlpha -= 0.05f;
                if (textAlpha <= 0f)
                {
                    textAlpha = 0f;
                    alphaState = 0;
                    lastAddedSouls = 0;
                }
            }

            if (visualSouls < targetSouls)
            {
                long diff = targetSouls - visualSouls;

                long speed = (long)(diff * 0.05);
                if (speed < 1)
                    speed = 1;

                visualSouls += speed;

                if (visualSouls >= targetSouls)
                    visualSouls = targetSouls;

                if (alphaState == 2)
                    displayTimer = 0f;
            }

            if (visualSouls >= targetSouls && alphaState == 2 && bufferedSouls == 0)
            {
                displayTimer += 1f / 60f;
                if (displayTimer >= 1.5f) 
                {
                    alphaState = 3;
                }
            }

            if (visualSouls == targetSouls && alphaState == 2 && bufferedSouls == 0)
            {
                displayTimer += 1f / 60f;
                if (displayTimer >= 0.85f)
                    alphaState = 3;
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (bgTexture == null)
                bgTexture = ModContent.Request<Texture2D>("DarkSouls/UI/Textures/SoulsCounterBackground", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

            float percentX = ClientConfig.Instance.SoulsCounterXPercent / 100f;
            float percentY = ClientConfig.Instance.SoulsCounterYPercent / 100f;

            float availableWidth = Main.screenWidth - bgTexture.Width;
            float availableHeight = Main.screenHeight - bgTexture.Height;

            Vector2 position = new Vector2(availableWidth * percentX, availableHeight * percentY);
            spriteBatch.Draw(bgTexture, position, Color.White);

            DynamicSpriteFont font = DarkSouls.OptimusPrincepsFont;
            float textScale = 0.2f;

            string soulsText = visualSouls.ToString();
            Vector2 textSize = font.MeasureString(soulsText) * textScale;
            Vector2 textPos = position + new Vector2(bgTexture.Width - textSize.X - 12f, bgTexture.Height / 2f - textSize.Y / 2f);

            Terraria.UI.Chat.ChatManager.DrawColorCodedStringWithShadow(
                spriteBatch, font, soulsText, textPos, Color.White, 0f, Vector2.Zero, new Vector2(textScale));

            if (textAlpha > 0f && lastAddedSouls > 0)
            {
                string addedText = "+" + lastAddedSouls.ToString();
                Vector2 addedTextSize = font.MeasureString(addedText) * textScale;

                float addedX = position.X - addedTextSize.X - 15f;
                float addedY = position.Y + (bgTexture.Height / 2f) - (addedTextSize.Y / 2f);

                Vector2 addedPos = new Vector2(addedX, addedY);
                Color addedColor = Color.White * textAlpha;

                Terraria.UI.Chat.ChatManager.DrawColorCodedStringWithShadow(
                    spriteBatch, font, addedText, addedPos, addedColor, 0f, Vector2.Zero, new Vector2(textScale));
            }
        }
    }
}