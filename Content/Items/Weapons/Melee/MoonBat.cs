using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FragmentsOfNocturnia.Content.Projectiles.Melee;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Melee
{
	public class MoonBat : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 25; // The item texture's width.
			Item.height = 25; // The item texture's height.

			//Will have to create a custom use style unless normal swing is fine

			Item.useStyle = ItemUseStyleID.Swing; // The useStyle of the Item.
			Item.useTime = 20; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 20; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
			Item.autoReuse = false; // Whether the weapon can be used more than once automatically by holding the use button.
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;

			Item.DamageType = DamageClass.Melee; // Whether your item is part of the melee class.
			Item.damage = 60; // The damage your item deals.
			Item.knockBack = 10; // The force of knockback of the weapon. Maximum is 20
			Item.crit = 6; // The critical strike chance the weapon has. The player, by default, has a 4% critical strike chance.

			Item.value = Item.buyPrice(gold: 1); // The value of the weapon in copper coins.
			Item.UseSound = SoundID.Item1; // The sound when the weapon is being used.
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{

			Vector2 toMouse = Main.MouseWorld - player.Center; //Makes the character look in the direction of the swing/throw
			if (toMouse.X != 0) // Avoid zero division
			{
			    player.direction = toMouse.X > 0 ? 1 : -1;
			}

			if (player.altFunctionUse == 2) //Only use value 2 for right-click functionality
			{
				//Item.useStyle = ItemUseStyleID.HoldUp;
				Item.noMelee = true;
				Item.autoReuse = false;

				Item.useTime = 35;
				Item.useAnimation = 35;
			}
			else
			{
				Item.noMelee = false;
				//Item.useStyle = ItemUseStyleID.Swing;

				Item.useTime = 20;
				Item.useAnimation = 20;
			}


			return base.CanUseItem(player);
        }

        public override bool? UseItem(Player player)
        {
			if (player.altFunctionUse == 2) // Right-click
			{

				/*
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					Projectile proj = Main.projectile[i];
					if (proj != null && proj.active && proj.owner == player.whoAmI && proj.ModProjectile is Hotties)
					{
						//If the player already spawned a hottie then kill it to spawn another one.
						//IF we want the player to be able to spawn more than one then remove this entire for loop.
						proj.Kill();
						break;
					}
				}
				*/

				Vector2 spawnPosition = player.Center;
				Vector2 velocity = new Vector2(0.3f * player.direction, -5.5f); //This replicates throwing the hottie up and slightly forwards in the direction of the mouse.
																			  //I say mouse since the player will turn towards the mouse when swinging the weapon

				Projectile.NewProjectile(
					player.GetSource_ItemUse(Item),
					spawnPosition,
					velocity,
					ModContent.ProjectileType<Hotties>(),
					20, // damage
					0f,// knockback
					player.whoAmI
				);

				return true;
			}
		
    		return false; // Let the left-click handle the normal swing
        }

        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
			//Can change the hitbox of this melee weapon when it is used. Called on the local client only.

			//I assume it is here that we have to check if the bat collides with the Hottie and then 
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj != null && proj.active && proj.owner == player.whoAmI && proj.ModProjectile is Hotties)
				{
					Rectangle hottieHitbox = proj.Hitbox;
					if (hitbox.Intersects(hottieHitbox))
					{
						//This code should only run when the weapon hitbox and the hottie hitbox collide
						proj.velocity = (Main.MouseWorld - proj.Center).SafeNormalize(Vector2.UnitX) * 12f;
						proj.damage = 80;
						proj.knockBack = 6;
						proj.ai[0] = 1;
						proj.netUpdate = true;
					}
				}
			}

			//base.UseItemHitbox(player, ref hitbox, ref noHitbox);
        }
	}
}