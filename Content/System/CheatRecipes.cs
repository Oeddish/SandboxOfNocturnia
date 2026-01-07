using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.System
{
    public class CheatRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.ExtendoGrip);
            recipe.AddIngredient(ItemID.Wood, 20);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 20); // Accepts either Lead or Iron Bars
            recipe.AddIngredient(ModContent.ItemType<Items.Items.BatEssence>(), 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = Recipe.Create(ItemID.MechanicalEye);
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = Recipe.Create(ItemID.MechanicalWorm);
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = Recipe.Create(ItemID.MechanicalSkull);
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = Recipe.Create(ItemID.SuspiciousLookingEye);
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

            recipe = Recipe.Create(ItemID.WormFood);
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

            recipe = Recipe.Create(ItemID.TruffleWorm);
            recipe.AddIngredient(ItemID.LunarCraftingStation, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = Recipe.Create(ItemID.BloodySpine, 1);
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

            recipe = Recipe.Create(ItemID.SlimeCrown , 1);
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
