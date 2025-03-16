using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using FragmentsOfNocturnia.Content.Projectiles.Ranged;
using FragmentsOfNocturnia.Content.Items.Items;
using FragmentsOfNocturnia.Content.Items.Weapons.Melee;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Ranged
{
    internal class AbyssalResonator : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 68;
            Item.height = 36;
            //Item.damage = 0;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 80;
            Item.useAnimation = 80;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1.5f;
            Item.UseSound = SoundID.Item34;
            Item.value = 1;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<AbyssalResonatorScope>();
            Item.shootSpeed = 8f;
            Item.useAmmo = AmmoID.Gel;
            Item.consumeAmmoOnFirstShotOnly = true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity * 0.000001f;
            position = position - new Vector2(30f, 0f);

            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 120f;

            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
                //position = position * new Vector2(1, 1.01f);
                
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrystalShard, 30);
            recipe.AddIngredient(ModContent.ItemType<NocturniumBar>(), 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}