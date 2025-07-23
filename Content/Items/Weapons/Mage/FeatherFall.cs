using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using FragmentsOfNocturnia.Content.Projectiles.Mage;
using FragmentsOfNocturnia.Content.Players;
using FragmentsOfNocturnia.Content.Items.Items;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Mage
{
    internal class FeatherFall : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 52;
            Item.height = 52;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.damage = 35;
            Item.knockBack = 6;
            Item.crit = 4;
            Item.mana = 5;

            Item.value = Item.sellPrice(gold: 12);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item76;

            Item.shoot = ModContent.ProjectileType<FeatherFallProjectile>(); // ID of the projectiles the sword will shoot
            Item.shootSpeed = 16f; // Speed of the projectiles the sword will shoot
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15f, 0f);
        }
        // This method gets called when firing your weapon.
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit > player.Center.Y - 200f)
            {
                ceilingLimit = player.Center.Y - 200f;
            }
            // Loop these functions 3 times.
            for (int i = 0; i < 3; i++)
            {
                position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                position.Y -= 100 * i;
                Vector2 heading = target - position;

                if (heading.Y < 0f)
                {
                    heading.Y *= -1f;
                }

                if (heading.Y < 20f)
                {
                    heading.Y = 20f;
                }

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(source, position, heading, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
            }

            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RavenFeather>(), 10);
            recipe.AddIngredient(ItemID.Ectoplasm, 15);
            recipe.AddIngredient(ItemID.SpookyTwig, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
