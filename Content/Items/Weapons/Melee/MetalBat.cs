using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using FragmentsOfNocturnia.Content.Items.Items;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Melee
{
    public class MetalBat : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 25; // The item texture's width.
            Item.height = 25; // The item texture's height.

            Item.useStyle = ItemUseStyleID.Swing; // The useStyle of the Item.
            Item.useTime = 30; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
            Item.useAnimation = 20; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
            Item.autoReuse = false; // Whether the weapon can be used more than once automatically by holding the use button.
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;

            Item.DamageType = DamageClass.Melee; // Whether your item is part of the melee class.
            Item.damage = 10; // The damage your item deals.
            Item.knockBack = 10; // The force of knockback of the weapon. Maximum is 20
            Item.crit = 3; // The critical strike chance the weapon has. The player, by default, has a 4% critical strike chance.

            Item.value = Item.buyPrice(gold: 1); // The value of the weapon in copper coins.

            Item.shoot = ProjectileID.WandOfFrostingFrost;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 temp = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitX) * 4f;
                Vector2 newVelocity = temp.RotatedByRandom(2 * 0.174533f);
                Vector2 spawnPosition = player.Center;

                Projectile.NewProjectile(
                    player.GetSource_ItemUse(Item),
                    spawnPosition,
                    newVelocity,
                    ProjectileID.WandOfFrostingFrost,
                    damage,
                    knockback,
                    player.whoAmI
                );
            }
            return false;
            //return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Inflict the debuff onto any NPC/Monster that this hits.
            // 60 frames = 1 second
            target.AddBuff(BuffID.Frostburn, 60 * 5);
        }
        
        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.WandofFrosting)
				.AddIngredient(ItemID.SilverBar, 10)
				.AddIngredient(ModContent.ItemType<HeavyRing>())
				.AddTile(TileID.Anvils)
				.Register();
        }

	}
}