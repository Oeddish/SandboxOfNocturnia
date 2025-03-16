using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Projectiles.Melee;
using FragmentsOfNocturnia.Content.Players;
using FragmentsOfNocturnia.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FragmentsOfNocturnia.Content.Items.Items;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Melee
{
    internal class SokidosRavenclaw : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.knockBack = 1f;
            Item.useStyle = ItemUseStyleID.Rapier; // Makes the player do the proper arm motion
            Item.useAnimation = 4;
            Item.useTime = 4;
            Item.width = 74;
            Item.height = 74;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.autoReuse = false;
            Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
            Item.noMelee = true; // The projectile will do the damage and not the item

            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 11);

            Item.shoot = ModContent.ProjectileType<SokidosRavenclawProjectile>(); // The projectile is what makes a shortsword work
            Item.shootSpeed = 2.1f; // This value bleeds into the behavior of the projectile as velocity, keep that in mind when tweaking values
            Item.autoReuse = true;
        }

        public override void HoldItem(Player player)
        {
            // Define a projectile type (replace with your actual projectile)
            int projType = ModContent.ProjectileType<SokidoRavenProjectile>();

            // Check if the projectile is already spawned
            bool alreadySpawned = false;

            foreach (Projectile proj in Main.projectile)
            {
                if (proj.active && proj.owner == player.whoAmI && proj.type == projType)
                {
                    alreadySpawned = true;
                    break;
                }
            }

            // If not spawned, create it
            if (!alreadySpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_ItemUse(Item),
                                         player.Center,
                                         Vector2.Zero,
                                         projType,
                                         Item.damage,
                                         Item.knockBack,
                                         player.whoAmI);
            }
        }

        public override void UpdateInventory(Player player)
        {
            // When not holding the sword, kill the projectile
            if (player.HeldItem.type != Type)
            {
                foreach (Projectile proj in Main.projectile)
                {
                    if (proj.active && proj.owner == player.whoAmI && proj.type == ModContent.ProjectileType<SokidoRavenProjectile>())
                    {
                        proj.Kill();
                    }
                }
            }
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
            var modPlayer = Main.LocalPlayer.GetModPlayer<SokidoPlayer>();

            if (player.altFunctionUse == 2 && !modPlayer.attacking)
            {
                //Main.NewText("/Tried to attack", color: Color.Gray);
                modPlayer.attacking = true;
                modPlayer.playerDirection = player.direction;
                modPlayer.mousePosition = Main.MouseWorld;
            }
            else
            {
                if (Main.mouseRight && !modPlayer.attacking)
                {
                    modPlayer.attacking = true;
                    modPlayer.playerDirection = player.direction;
                    modPlayer.mousePosition = Main.MouseWorld;
                }
                Item.shoot = ModContent.ProjectileType<SokidosRavenclawProjectile>();
                base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpookyTwig, 1);
            recipe.AddIngredient(RecipeGroupID.IronBar, 10);
            recipe.AddIngredient(ItemID.Ectoplasm, 15);
            //recipe.AddIngredient(ModContent.ItemType<BatBasher>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}
