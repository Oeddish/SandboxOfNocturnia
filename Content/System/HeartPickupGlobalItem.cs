using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using FragmentsOfNocturnia.Content.Players;

namespace FragmentsOfNocturnia.Content.Systems
{
    public class HeartPickupGlobalItem : GlobalItem
    {
        public override bool CanPickup(Item item, Player player)
        {
            if (item.type == ItemID.Heart)
            {
                var modPlayer = player.GetModPlayer<KokoModPlayer>();

                // Check for active buff AND missing health
                if (modPlayer.Absorption_Buff && player.statLife < player.statLifeMax2)
                {
                    float pickupRadius = 275f; // Range in pixels (~18-19 tiles)
                    float distance = Vector2.Distance(item.Center, player.Center);

                    if (distance < pickupRadius)
                    {
                        // Gently move the heart toward the player
                        item.position = Vector2.Lerp(item.position, player.Center, 0.15f);
                    }
                }
            }

            return base.CanPickup(item, player);
        }
    }
}
