using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Items
{
    internal class SafeUnsafe : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ItemID.Safe)
            {
                // If Alt is pressed, allow the vanilla item use (placement)
                if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) || Keyboard.GetState().IsKeyDown(Keys.RightAlt))
                {
                    return base.CanUseItem(item, player);
                }

                // Open or close inventory instead of placing the safe
                if (player.chest == -3)
                {
                    player.chest = -1; // Close if aldeady open
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.MenuClose, player.position);
                }
                else
                {
                    // Open the Safe UI for the player (same as right-clicking a placed Safe)
                    player.chest = -3;
                    Main.playerInventory = true;
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.MenuOpen, player.position);
                }

                // Return false to prevent the vanilla item use (placing it)
                return false;
            }

            // For all other items, allow vanilla behavior
            return base.CanUseItem(item, player);
        }
    }
}
