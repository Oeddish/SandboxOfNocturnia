using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Tiles.Furniture;
using Terraria.ModLoader;
using Terraria;
using FragmentsOfNocturnia.Content.Items.Items;
using Terraria.ID;

namespace FragmentsOfNocturnia.Content.Items.Placeable.Furniture
{
    internal class NocturniumThrone : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<NocturniumThroneTile>());
            Item.width = 21;
            Item.height = 27;
            Item.value = 150;
            Item.value = Item.sellPrice(gold: 60, silver: 40);
            Item.rare = ItemRarityID.Yellow;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Silk, 20);
            recipe.AddIngredient(ModContent.ItemType<NocturniumBar>(), 30);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}