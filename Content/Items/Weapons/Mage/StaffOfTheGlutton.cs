using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using FragmentsOfNocturnia.Content.Projectiles.Mage;
using Steamworks;
using FragmentsOfNocturnia.Content.Items.Items;
using FragmentsOfNocturnia.Content.Items.Weapons.Melee;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Mage
{
    internal class StaffOfTheGlutton : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 75;
            Item.height = 75;
            Item.damage = 35;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.mana = 10;
            Item.shootSpeed = 20f;
            
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<StaffOfTheGluttonHeadProjectile>();
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 1, silver: 70);
            
            Item.noMelee = true;
            Item.autoReuse = true;
        }

        //StaffOfTheGlutton said to hold it out like a spear
        public override Vector2? HoldoutOffset()
        {
            Vector2? offset = new Vector2(-10f, 0f);
            return offset;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);

            float randomAngle = Main.rand.NextFloat(0, MathHelper.TwoPi);
            position = Main.MouseWorld + 400f * new Vector2((float)Math.Cos(randomAngle), (float)Math.Sin(randomAngle));

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofNight, 15);
            recipe.AddIngredient(ModContent.ItemType<BatEssence>(), 10);
            recipe.AddIngredient(ItemID.SpiderFang, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}
