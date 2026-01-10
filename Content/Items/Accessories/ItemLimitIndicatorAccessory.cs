using FragmentsOfNocturnia.Content.Items.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FragmentsOfNocturnia.Content.Players;

namespace FragmentsOfNocturnia.Content.Items.Accessories
{
    public class ItemLimitIndicatorAccessory : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 75);
        }

        // Works when equipped
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ItemLimitPlayer>().showItemLimitHud = true;
        }

        public override void UpdateVanity(Player player)
        {
            player.GetModPlayer<ItemLimitPlayer>().showItemLimitHud = true;
        }

        // Works when the item is simply in the inventory (vanilla informational items use this pattern)
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<ItemLimitPlayer>().showItemLimitHud = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}