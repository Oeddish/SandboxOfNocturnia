using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FragmentsOfNocturnia.Content.Items.Items;
using FragmentsOfNocturnia.Content.Projectiles.Melee;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Melee
{
    public class NocturneKnocker : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Equals("Nocturne Knocker");
            Tooltip.Equals("These bat's got stuck to the charged core. Sometimes they fly off when it hits something.");
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.knockBack = 10.5f;
            Item.width = 48;
            Item.height = 48;
            Item.damage = 150;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<NocturneKnockerProjectile>();
            Item.shootSpeed = 14f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(gold: 43, silver: 50);
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.channel = true;
            Item.noMelee = true;
            Item.autoReuse = true;
        }

        // Adds a visual lighting effect
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Add a light effect with a purple hue
            Lighting.AddLight(hitbox.Center.ToVector2(), 0.5f, 0.1f, 0.6f);

            // Create a dust effect on swing
            if (Main.rand.NextBool(3))
            {
                int dust = Dust.NewDust(hitbox.Location.ToVector2(), hitbox.Width, hitbox.Height, DustID.Shadowflame, 0f, 0f, 150, default, 1.2f);
                Main.dust[dust].velocity *= 0.3f;
                Main.dust[dust].noGravity = true; // Makes the dust float
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddIngredient(ModContent.ItemType<BatBasher>(), 1);
            recipe.AddIngredient(ModContent.ItemType<NocturniumBar>(), 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

        }
    }
}
