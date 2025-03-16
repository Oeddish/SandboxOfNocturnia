using FragmentsOfNocturnia.Content.Projectiles.Summoner.Whips;
using FragmentsOfNocturnia.Content.Items.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Summoner.Whips
{
    internal class Dermatidae : ModItem
    {
        public override void SetDefaults()
        {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.DefaultToWhip(ModContent.ProjectileType<DermatidaeProjectile>(), 50, 2, 4);
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(gold: 11);
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix()
        {
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BloodVial>(), 20);
            recipe.AddIngredient(ModContent.ItemType<DragonBone>(), 30);
            recipe.AddIngredient(ModContent.ItemType<ViolentCatalyst>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}