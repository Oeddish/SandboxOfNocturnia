using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Ranged
{
    internal class DaiyaRevolver : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 13;
            Item.damage = 28;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1.5f;
            Item.UseSound = SoundID.Item41;
            Item.value = 1;
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = false;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 8f;
            Item.useAmmo = AmmoID.Bullet;
        }

        public override Vector2? HoldoutOffset() {
			return new Vector2(-2f, -2f);
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 23f;

            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
                position.Y -= 8f;
			}
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Revolver);
            recipe.AddRecipeGroup("FragmentsOfNocturnia:SilverOrTungsten", 5);
            recipe.AddIngredient(ItemID.GoldCoin, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}