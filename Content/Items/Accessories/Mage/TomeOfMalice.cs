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
    internal class TomeOfMalice : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 4);
            Item.rare = ItemRarityID.Lime;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<NocturnePlayer>();
            modPlayer.applyReverberation = true;
            player.GetDamage<MagicDamageClass>() += 0.25f;
            player.GetCritChance<MagicDamageClass>() += 0.05f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TomeOfProvidence>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CursedPearl>(), 1);
            recipe.AddIngredient(ItemID.AncientCloth, 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}