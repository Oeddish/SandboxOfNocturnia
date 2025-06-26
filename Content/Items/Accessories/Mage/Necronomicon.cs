using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Players;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace FragmentsOfNocturnia.Content.Items.Accessories.Mage
{
    internal class Necronomicon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 4);
            Item.rare = ItemRarityID.Red;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<NocturnePlayer>();
            modPlayer.applyReverberation = true;
            modPlayer.applyWraithed = true;
            player.GetDamage<MagicDamageClass>() += 0.22f;
            player.GetCritChance<MagicDamageClass>() += 0.12f;
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ModContent.ItemType<HolyScripture>() || slot != i && player.armor[i].type == ModContent.ItemType<ReverberationStone>() || slot != i && player.armor[i].type == ModContent.ItemType<TomeOfProvidence>() || slot != i && player.armor[i].type == ModContent.ItemType<CursedPearl>() || slot != i && player.armor[i].type == ModContent.ItemType<TomeOfMalice>() || slot != i && player.armor[i].type == ModContent.ItemType<EyeOfNewt>())
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
            recipe.AddIngredient(ModContent.ItemType<TomeOfMalice>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EyeOfNewt>(), 1);
            recipe.AddIngredient(ItemID.FragmentNebula, 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}