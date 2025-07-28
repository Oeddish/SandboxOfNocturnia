using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Items.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

// HELLO - KRAKEN
// This item has some notes on how armors work, they are both from teh example mod and from me

namespace FragmentsOfNocturnia.Content.Items.Armors.Hardmode.FlippedOffArmor
{
    [AutoloadEquip(EquipType.Head)] // This line basically tells the game to use "itemName_Head" as the spritesheet
    internal class FlippedOffMask : ModItem
    {
        public static readonly int AdditiveMeleeDamageBonus = 10;
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Pink;
            Item.defense = 23;
        }
        // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<FlippedOffBreastplate>() && legs.type == ModContent.ItemType<FlippedOffLegs>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Press UP to activate 'Bat mode'!"; // I prefer to modify the setbonus tip here directly
            player.AddBuff(BuffID.Gravitation, 1, true);
            player.noFallDmg = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += AdditiveMeleeDamageBonus / 100f; // I don't know why it needs to be devided by 100 but it does, not sure where this rule applies and where not, deal with it (I just use it everywhere, we'll find out)
            player.GetAttackSpeed(DamageClass.Melee) += 10 / 100f;
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
