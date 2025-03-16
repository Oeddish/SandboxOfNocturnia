using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Items.Items;
using FragmentsOfNocturnia.Content.Projectiles.Summoner.Whips;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Summoner.Whips
{
    internal class FishWhip : ModItem
    {
        public override void SetDefaults()
        {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.DefaultToWhip(ModContent.ProjectileType<FishWhipProjectile>(), 12, 2, 5);
            Item.value = Item.sellPrice(silver: 6, copper: 60);
            Item.rare = ItemRarityID.White;
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix()
        {
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.WoodFishingPole, 1);
            recipe.AddIngredient(ItemID.WhiteString, 1);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
