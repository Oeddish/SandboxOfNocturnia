using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Projectiles.Ranged;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FragmentsOfNocturnia.Content.Items.Items;
using FragmentsOfNocturnia.Content.Projectiles.Mage;
using System.Runtime.InteropServices;
using Terraria.DataStructures;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Ranged
{
    internal class Diphylla : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 58;
            Item.damage = 50;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2.5f;
            Item.UseSound = SoundID.Item5;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 4);
            Item.autoReuse = true;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ModContent.ProjectileType<DiphyllaProjectile>();
            Item.consumeAmmoOnFirstShotOnly = true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ModContent.ProjectileType<DiphyllaProjectile>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numberProjectiles = 5;
            float rotation = MathHelper.ToRadians(20); // TOTAL SPREAD

            for (int i = 0; i < numberProjectiles; i++)
            {
                float currentRotation = MathHelper.Lerp(-rotation, rotation, i / (float)(numberProjectiles - 1));
                Vector2 newVelocity = velocity.RotatedBy(currentRotation);

                Projectile.NewProjectile(
                    source,
                    position,
                    newVelocity,
                    ModContent.ProjectileType<DiphyllaProjectile>(),
                    damage,
                    knockback,
                    player.whoAmI);
            }
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(
                    player.position + new Vector2(player.width, player.height) / 2,
                    5, 5,
                    DustID.Blood,
                    Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5),
                    0,
                    Color.Red,
                    1f);
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0f, 0f);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BloodVial>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DragonBone>(), 10);
            recipe.AddIngredient(ModContent.ItemType<RottingCatalyst>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<BloodVial>(), 5);
            recipe1.AddIngredient(ModContent.ItemType<DragonBone>(), 10);
            recipe1.AddIngredient(ModContent.ItemType<ViolentCatalyst>(), 1);
            recipe1.AddTile(TileID.Anvils);
            recipe1.Register();
        }
    }
}
