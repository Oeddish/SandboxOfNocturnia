using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Items.Items;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Armors.Vanity.Bossymoon
{
    [AutoloadEquip(EquipType.Head)]
    internal class BossymoonVanityHead : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 14;
            Item.rare = ItemRarityID.Blue;
            Item.vanity = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<UmbralThread>(), 5);
            recipe.AddIngredient(ModContent.ItemType<BatEssence>(), 5);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
    }
}
