using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Buffs.Pets;
using FragmentsOfNocturnia.Content.Projectiles.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Accessories
{
    public class PottedFlower : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shoot = ModContent.ProjectileType<PottedCompanionProjectile>();
            Item.width = 16;
            Item.height = 30;
            Item.UseSound = SoundID.Item2;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.rare = ItemRarityID.Blue;
            Item.noMelee = true;

            Item.value = Item.sellPrice(0, 0, 50, 20);

            Item.buffType = ModContent.BuffType<PottedCompanion>();
            Item.vanity = true;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AbigailsFlower, 1);
            recipe.AddIngredient(ItemID.ClayPot, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600);
            }
        }

        public override void UpdateVanity(Player player)
        {
            if (player.whoAmI == Main.myPlayer && !player.HasBuff<PottedCompanion>())
            {
                player.AddBuff(Item.buffType, 3600);
            }
        }
    }
}