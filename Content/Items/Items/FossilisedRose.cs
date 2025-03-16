using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using FragmentsOfNocturnia.Content.Systems;
using Microsoft.Xna.Framework;

namespace FragmentsOfNocturnia.Content.Items.Items
{
    internal class FossilisedRose : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24; // The item texture's width
            Item.height = 38; // The item texture's height

            Item.maxStack = 1; // The item's max stack value
            Item.value = Item.buyPrice(gold: 0);
            Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Bone, 30);
            recipe.AddIngredient(ItemID.ObsidianRose, 1);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
        }
    }
}
