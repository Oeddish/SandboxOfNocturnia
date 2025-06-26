using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using FragmentsOfNocturnia.Content.Players;

namespace FragmentsOfNocturnia.Content.Items.Accessories
{
    internal class RoyalFlush : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 17;
            Item.height = 16;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 2, silver: 45);
            Item.rare = ItemRarityID.Blue;
        }

        // This is where the effect is applied when the item is equipped
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<KokoModPlayer>().gunCritBonus += 15;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Silk,5);
            recipe.AddIngredient(ItemID.GoldCoin,10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}