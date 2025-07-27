using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Items.Items;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Armors.Hardmode.FlippedOffArmor
{
    [AutoloadEquip(EquipType.Head)]
    internal class FlippedOffHeadgear : ModItem
    {
        public static readonly int AdditiveMagicDamageBonus = 14;
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Pink;
            Item.defense = 4;
        }
        // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<FlippedOffBreastplate>() && legs.type == ModContent.ItemType<FlippedOffLegs>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Press UP to activate 'Bat mode'!"; // This is the setbonus tooltip
            player.AddBuff(BuffID.Gravitation, 1, true);
            player.noFallDmg = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += AdditiveMagicDamageBonus / 100f;
            player.GetCritChance(DamageClass.Magic) += 14 / 100f;
            player.statManaMax2 += 120;
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
