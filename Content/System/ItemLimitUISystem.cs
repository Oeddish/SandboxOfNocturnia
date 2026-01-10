using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;
using FragmentsOfNocturnia.Content.Players;

namespace FragmentsOfNocturnia.Content.System
{
    public class ItemLimitUISystem : ModSystem
    {
        private bool lastVisible = false;
        
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            const string layerName = "FragmentsOfNocturnia: Item Limit HUD";

            if (layers.FindIndex(layer => layer.Name == layerName) != -1)
                return;

            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    layerName,
                    DrawItemLimitHUD,
                    InterfaceScaleType.UI)
                );
            }
        }

        private bool DrawItemLimitHUD()
        {
            if (Main.dedServ)
                return true;

            Player player = Main.LocalPlayer;
            var modPlayer = player.GetModPlayer<ItemLimitPlayer>();

            bool visible = modPlayer.showItemLimitHud;
            if (lastVisible != visible)
            {
                Main.NewText("HUD visibility change: " + visible);
                lastVisible = visible;
            }

            if (!visible)
            {
                return true;
            }

            int maxItemCount = Main.maxItems;
            // hard-coded test value
            // int maxItemCount = 40;

            string text = $"Items: {ItemLimitNotifierSystem.ActiveItemCount}/{maxItemCount}";
            Color color = ItemLimitNotifierSystem.LimitReached ? Color.Yellow : Color.White;

            float scale = 0.9f;

            // Measure the text using the same font as other HUD strings and place it on the right side.
            Vector2 textSize = FontAssets.MouseText.Value.MeasureString(text) * scale;
            Vector2 position = new Vector2(Main.screenWidth - textSize.X - 16f, 16f);

            // Draw with border for readability (same style as other UI lines)
            Utils.DrawBorderString(Main.spriteBatch, text, position, color, scale);

            if (ItemLimitNotifierSystem.LimitReached)
            {
                float effectMultiplier = 2.2f;

                float pulse = 0.6f + (0.4f * (float)global::System.Math.Sin(Main.GlobalTimeWrappedHourly * 6f));
                pulse *= effectMultiplier;

                // Lighting.AddLight(player.Center, 0.6f * pulse, 0.05f * pulse, 0.05f * pulse);
                Vector3 rgb = new(0.6f * pulse, 0.05f * pulse, 0.05f * pulse);

                // Add a subtle light/gloom to make it feel "alerty"
                FragmentsOfNocturnia.Instance.Logger.Info("Player light: pulse = " + pulse + ", rgb = " + rgb);
                Lighting.AddLight(player.Center, rgb);
            }

            return true;
        }
    }
}