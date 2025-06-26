using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Items.Items;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace FragmentsOfNocturnia.Content.Items.Accessories.Mage
{
    internal class EyeOfNewt : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 36;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 5, 73);
            Item.rare = ItemRarityID.Yellow;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance<MagicDamageClass>() += 0.1f;
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ModContent.ItemType<Necronomicon>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.EyeoftheGolem, 1);
            recipe.AddIngredient(ModContent.ItemType<ViolentCatalyst>(), 1);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
