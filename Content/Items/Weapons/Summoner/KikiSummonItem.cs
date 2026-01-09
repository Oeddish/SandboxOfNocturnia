using System;
using FragmentsOfNocturnia.Content.Buffs;
using FragmentsOfNocturnia.Content.Players;
using FragmentsOfNocturnia.Content.Projectiles.Summoner;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Summoner
{
    public class KikiSummonItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

            ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; // The default value is 1, but other values are supported. See the docs for more guidance. 
        }

        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.knockBack = 3f;
            Item.mana = 20; // mana cost
            Item.width = 15;
            Item.height = 20;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing; // how the player's arm moves when using the item
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item44; // What sound should play when using the item

            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<KikiSummonBuff>();
            Item.shoot = ModContent.ProjectileType<KikiDroidProj>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var modPlayer = player.GetModPlayer<KokoModPlayer>();

            // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
            player.AddBuff(Item.buffType, 2);

            // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
            int kikiDroid = ModContent.ProjectileType<KikiDroidProj>();
            var minionA = Projectile.NewProjectileDirect(source, position, velocity, kikiDroid, 30, knockback, Main.myPlayer);
            minionA.originalDamage = Item.damage;
            float id = modPlayer.getNextKikiID();
            minionA.ai[0] = id;

            // Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
            .AddIngredient(ItemID.GoldBar)
            .AddRecipeGroup("FragmentsOfNocturnia:SilverOrTungsten", 5)
            .AddIngredient(ItemID.Lens, 10)
            .AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}