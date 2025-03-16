using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FragmentsOfNocturnia.Content.Items.Items;
using FragmentsOfNocturnia.Content.Projectiles.Melee;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Melee
{
    public class BatBasher : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Equals("Bat Basher");
            Tooltip.Equals("A heavy mace that crushes enemies under the weight of eternal night.");
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.useAnimation = 45; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useTime = 45; // The item's use time in ticks (60 ticks == 1 second.)
            Item.knockBack = 5.5f; // The knockback of your flail, this is dynamically adjusted in the projectile code.
            Item.width = 32; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.damage = 40; // The damage of your flail, this is dynamically adjusted in the projectile code.
            Item.noUseGraphic = true; // This makes sure the item does not get shown when the player swings his hand
            Item.shoot = ModContent.ProjectileType<BatBasherProjectile>(); // The flail projectile
            Item.shootSpeed = 12f; // The speed of the projectile measured in pixels per frame.
            Item.UseSound = SoundID.Item1; // The sound that this item makes when used
            Item.rare = ItemRarityID.Green; // The color of the name of your item
            Item.value = Item.sellPrice(gold: 15, silver: 50); // Sells for 1 gold 50 silver
            Item.DamageType = DamageClass.MeleeNoSpeed; // Deals melee damage
            Item.channel = true;
            Item.noMelee = true; // This makes sure the item does not deal damage from the swinging animation
        }

        // Adds a visual lighting effect
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Add a light effect with a purple hue
            Lighting.AddLight(hitbox.Center.ToVector2(), 0.5f, 0.1f, 0.6f);

            // Create a dust effect on swing
            if (Main.rand.NextBool(3)) // 1 in 3 chance to spawn dust
            {
                int dust = Dust.NewDust(hitbox.Location.ToVector2(), hitbox.Width, hitbox.Height, DustID.Shadowflame, 0f, 0f, 150, default, 1.2f);
                Main.dust[dust].velocity *= 0.3f;
                Main.dust[dust].noGravity = true; // Makes the dust float
            }
        }

        // Optional: Add a crafting recipe for the weapon
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FlamingMace, 1);
            recipe.AddIngredient(ItemID.Sunfury, 1);
            recipe.AddIngredient(ItemID.BlueMoon, 1);
            recipe.AddIngredient(ItemID.TheMeatball, 1);
            recipe.AddIngredient(ModContent.ItemType<NightmareSteelBar>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.FlamingMace, 1);
            recipe1.AddIngredient(ItemID.Sunfury, 1);
            recipe1.AddIngredient(ItemID.BlueMoon, 1);
            recipe1.AddIngredient(ItemID.BallOHurt, 1);
            recipe1.AddIngredient(ModContent.ItemType<NightmareSteelBar>(), 10);
            recipe1.AddTile(TileID.Anvils);
            recipe1.Register();
        }
    }
}
