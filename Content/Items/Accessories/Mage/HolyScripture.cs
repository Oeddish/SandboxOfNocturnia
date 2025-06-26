using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Items.Items;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Accessories.Mage
{
    internal class HolyScripture : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 4);
            Item.rare = ItemRarityID.LightRed;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage<MagicDamageClass>() += 0.2f;
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ModContent.ItemType<Necronomicon>() || slot != i && player.armor[i].type == ModContent.ItemType<TomeOfProvidence>() || slot != i && player.armor[i].type == ModContent.ItemType<TomeOfMalice>())
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
            recipe.AddIngredient(ItemID.SpellTome, 1);
            recipe.AddIngredient(ModContent.ItemType<HolyCatalyst>(), 1);
            recipe.AddIngredient(ItemID.SorcererEmblem, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
