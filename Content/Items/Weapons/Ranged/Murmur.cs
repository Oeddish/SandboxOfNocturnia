using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using FragmentsOfNocturnia.Content.Items.Items;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Ranged
{
    internal class Murmur : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 58;
            Item.damage = 20;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.knockBack = 6.5f;
            Item.shootSpeed = 8f;
            
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(gold: 3, silver: 62);
            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            
            Item.noMelee = true;
            Item.autoReuse = false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrimtaneBar, 8);
            recipe.AddIngredient(ModContent.ItemType<BatEssence>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void HoldItem(Player player)
        {
            player.AddBuff(BuffID.Hunter, 5, true);
        }
    }
}
