using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace FragmentsOfNocturnia.Content.Items.Items
{
    internal class BatEssence : ModItem
    {

        public override void SetStaticDefaults()
        {
            ItemID.Sets.AnimatesAsSoul[Type] = true;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(6, 4));
        }


        public override void SetDefaults()
        {
            Item.width = 24; // The item texture's width
            Item.height = 24; // The item texture's height

            Item.maxStack = Item.CommonMaxStack; // The item's max stack value
            Item.value = Item.sellPrice(silver: 10); // The value of the item in copper coins. Item.buyPrice & Item.sellPrice are helper methods that returns costs in copper coins based on platinum/gold/silver/copper arguments provided to it. 
            Item.rare = ItemRarityID.White;
        }

        public override void PostUpdate()
        {
            Player closestPlayer = Main.player[Player.FindClosest(Item.Center, Item.width, Item.height)];
            float attractionRange = 120f; // Range within which the item floats towards the player

            if (Vector2.Distance(Item.Center, closestPlayer.Center) < attractionRange)
            {
                // Move towards the player
                float speed = 2f; // How fast the item moves toward the player
                Vector2 direction = closestPlayer.Center - Item.Center;
                direction.Normalize();
                Item.velocity = direction * speed;
            }
            else
            {
                // Apply a floating effect
                float time = Main.GlobalTimeWrappedHourly * 2f; // Global time affects the floatiness
                float amplitude = 0.5f; // The strength of the wave
                Item.velocity.Y = amplitude * (float)Math.Sin(time); // Sinusoidal wave for floating
                Item.velocity.X *= 0.98f; // Gradually slow horizontal movement
            }

            // Optionally, add a light glow (like souls)
            Lighting.AddLight(Item.Center, 0.8f, 0.2f, 0.8f); // Add a soft blue light
        }
    }
}
