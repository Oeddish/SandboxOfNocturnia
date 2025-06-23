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
    internal class ReverberationStone : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 40;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 3, 68); //
            Item.rare = ItemRarityID.LightRed;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage<MagicDamageClass>() += 0.05f;
            var modPlayer = Main.LocalPlayer.GetModPlayer<NocturnePlayer>();
            modPlayer.applyReverberation = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Diamond, 10);
            recipe.AddIngredient(ModContent.ItemType<BatEssence>(), 5);
            recipe.AddIngredient(ItemID.LightShard, 2);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}

