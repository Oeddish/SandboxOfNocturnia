using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using FragmentsOfNocturnia.Content.Items.Items;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Melee
{
    internal class CrimsonZweihander : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 8, 30);

            Item.useAnimation = 35;
            Item.useTime = 35;
            Item.damage = 34;
            Item.knockBack = 6.5f;

            Item.noMelee = false;
            Item.autoReuse = false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrimtaneBar, 20);
            recipe.AddIngredient(ModContent.ItemType<BatEssence>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void HoldItem(Player player)
        {
            //player.AddBuff(BuffID.Hunter, 5, true);
            player.statDefense += 5;
            base.HoldItem(player);
        }
    }
}
