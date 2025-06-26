using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Ranged
{
    internal class DaiyaTommy : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 35;
            Item.height = 15;
            Item.damage = 0;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.crit = 6;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1.5f;
            Item.UseSound = SoundID.Item41;
            Item.value = 1;
            Item.rare = ItemRarityID.Cyan;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 8f;
            Item.useAmmo = AmmoID.Coin;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int coinDamage = damage;
            if (type == ProjectileID.PlatinumCoin)
            {
                coinDamage = 180;
            }
            else if (type == ProjectileID.GoldCoin)
            {
                coinDamage = 80;
            }
            else if (type == ProjectileID.SilverCoin)
            {
                coinDamage = 35;
            }
            else if (type == ProjectileID.CopperCoin)
            {
                coinDamage = 7;
            }

            Projectile.NewProjectile(source, position, velocity, type, coinDamage, knockback, player.whoAmI);
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15f, 2f);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 40f;

            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
                position.Y -= 5f;
			}
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DaiyaRevolver>();
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(ItemID.PlatinumCoin);
            recipe.AddIngredient(ItemID.SoulofMight,5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}