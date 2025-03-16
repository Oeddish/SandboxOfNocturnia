using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using FragmentsOfNocturnia.Content.Tiles.Furniture;
using Terraria.ID;
using FragmentsOfNocturnia.Content.Items.Items;
using Terraria;

namespace FragmentsOfNocturnia.Content.Items.Placeable.Furniture;

internal class NightmareSteelThrone : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<NightmareSteelThroneTile>());
        Item.width = 21;
        Item.height = 27;
        Item.value = Item.sellPrice(0, 15, 40);
        Item.rare = ItemRarityID.Pink;
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Silk, 20);
        recipe.AddIngredient(ModContent.ItemType<NightmareSteelBar>(), 30);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}