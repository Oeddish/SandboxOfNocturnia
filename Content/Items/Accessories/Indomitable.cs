using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using FragmentsOfNocturnia.Content.Players;
using FragmentsOfNocturnia.Content.Items.Items;

namespace FragmentsOfNocturnia.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    internal class Indomitable : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 7);
            Item.rare = ItemRarityID.Expert;
            Item.damage = 30;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.knockBack = 9f;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 4;
            player.dashType = 2;
            player.noKnockback = true;
            player.fireWalk = true;

            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Stoned] = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AnkhShield, 1);
            recipe.AddIngredient(ItemID.EoCShield, 1);
            recipe.AddIngredient(ModContent.ItemType<NightmareSteelBar>(), 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
