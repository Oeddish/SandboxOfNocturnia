using FragmentsOfNocturnia.Content.Items.Items;
using FragmentsOfNocturnia.Content.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Tools
{
    public class NightmareDiggingClaws : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ShroomiteDiggingClaw;
        private const string MessageKey = "NightmareDiggingClaws";
        private const int CooldownTicks = 60; // 1 second

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ShroomiteDiggingClaw);
            Item.tileBoost = 0;
        }

        public override bool CanUseItem(Player player)
        {
            // Only allow use if the world is not in Hardmode
            if (Main.hardMode)
            {
                if (Main.myPlayer == player.whoAmI)
                {
                    var cooldownPlayer = player.GetModPlayer<MessageCooldownPlayer>();
                    // If not on cooldown, show message and set cooldown 
                    cooldownPlayer.TryTriggerMessage(
                        MessageKey,
                        CooldownTicks,
                        "The spirits of light and dark have blunted your claws!"
                    );
                }

                return false;
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 9);
            recipe.AddIngredient(ModContent.ItemType<BatEssence>(), 16);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
