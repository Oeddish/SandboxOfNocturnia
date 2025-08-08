using FragmentsOfNocturnia.Content.Items.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Tools
{
    public class NightmareDiggingClaws : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ShroomiteDiggingClaw;

        public override void SetStaticDefaults()
        {
            // Optional: Set display name and tooltip here or in localization
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ShroomiteDiggingClaw);
            Item.tileBoost = 0;
            // Optionally tweak stats here if you want
            // For example: Item.pick = 200; // Shroomite is 200
        }

        public override bool CanUseItem(Player player)
        {
            // Only allow use if the world is not in Hardmode
            if (Main.hardMode)
            {
                if (Main.myPlayer == player.whoAmI)
                    Main.NewText("The spirits of light and dark have blunted your claws!", 200, 100, 255);
                return false;
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddIngredient(ModContent.ItemType<BatEssence>(), 16);
            recipe.AddTile(TileID.Anvils); // Or whatever you want
            recipe.Register();
        }
    }
}
