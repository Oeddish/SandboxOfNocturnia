using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Input;

namespace FragmentsOfNocturnia.Content.Items.Items
{
    internal class SliceOfEasyCake : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            // Check if it's the vanilla Slice of Cake
            if (item.type == ItemID.SliceOfCake)
            {
                // If Alt is pressed, allow the vanilla item use (placement)
                if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) || Keyboard.GetState().IsKeyDown(Keys.RightAlt))
                {
                    return base.CanUseItem(item, player);
                }

                // Add the buff instead of placing item
                player.AddBuff(BuffID.SugarRush, 120 * 60); // 2 minutes in ticks

                // Play a sound effect
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item3, player.position); 

                // Return false to prevent the vanilla item use (placing it)
                return false;
            }

            // For all other items, allow vanilla behavior
            return base.CanUseItem(item, player);
        }
    }
}
