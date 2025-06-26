using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Items.Items;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using FragmentsOfNocturnia.Content.Players;

namespace FragmentsOfNocturnia.Content.Items.Accessories.Mage
{
    internal class TomeOfProvidence : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 4);
            Item.rare = ItemRarityID.Pink;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<NocturnePlayer>();
            modPlayer.applyReverberation = true;
            player.GetDamage<MagicDamageClass>() += 0.25f;
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ModContent.ItemType<HolyScripture>() || slot != i && player.armor[i].type == ModContent.ItemType<ReverberationStone>() || slot != i && player.armor[i].type == ModContent.ItemType<Necronomicon>() || slot != i && player.armor[i].type == ModContent.ItemType<TomeOfMalice>())
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
            recipe.AddIngredient(ModContent.ItemType<HolyScripture>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ReverberationStone>(), 1);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
