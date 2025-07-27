using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using FragmentsOfNocturnia.Content.Items.Items;

namespace FragmentsOfNocturnia.Content.Items.Armors.Hardmode.FlippedOffArmor
{
    [AutoloadEquip(EquipType.Legs)]
    internal class FlippedOffLegs : ModItem
    {
        public static readonly int MoveSpeedBonus = 8;
        public static readonly int damageBonus = 8;

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Pink;
            Item.defense = 11;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += MoveSpeedBonus / 100f;
            player.GetDamage(DamageClass.Generic) += damageBonus / 100f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<NightmareSteelBar>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
