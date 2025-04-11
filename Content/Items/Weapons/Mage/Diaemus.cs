using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Items.Items;
using FragmentsOfNocturnia.Content.Projectiles.Mage;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Mage
{
    internal class Diaemus : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 44;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 80;
            Item.useAnimation = 80;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.damage = 30;
            Item.knockBack = 6;
            Item.crit = 4;
            Item.mana = 45;

            Item.value = Item.sellPrice(gold: 12);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item76;

            Item.shoot = ModContent.ProjectileType<DiaemusProjectile>(); // ID of the projectiles
            Item.shootSpeed = 6f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15f, 0f);
        }
        // This method gets called when firing your weapon.
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //position = Main.MouseWorld;
            //Projectile.NewProjectile(source, position, velocity, type, damage * 2, knockback);

            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BloodVial>(), 15);
            recipe.AddIngredient(ModContent.ItemType<DragonBone>(), 10);
            recipe.AddIngredient(ModContent.ItemType<ViolentCatalyst>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<BloodVial>(), 15);
            recipe1.AddIngredient(ModContent.ItemType<DragonBone>(), 10);
            recipe1.AddIngredient(ModContent.ItemType<RottingCatalyst>(), 1);
            recipe1.AddTile(TileID.MythrilAnvil);
            recipe1.Register();
        }
    }
}
