using FragmentsOfNocturnia.Content.Items.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Placeable.Furniture;

internal class NightmareChest : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.NightmareChestTile>());
        Item.width = 26;
        Item.height = 22;
        Item.value = 500;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Wood, 8);
        recipe.AddIngredient(ItemID.IronBar, 2);
        // It's all about the bats!
        recipe.AddIngredient(ModContent.ItemType<BatEssence>(), 2);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}