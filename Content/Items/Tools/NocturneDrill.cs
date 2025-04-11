using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using FragmentsOfNocturnia.Content.Items.Items;
using FragmentsOfNocturnia.Content.Projectiles.Tools;

namespace FragmentsOfNocturnia.Content.Items.Tools
{
    internal class NocturneDrill : ModItem
    {
        public override void SetStaticDefaults()
        {
            // As mentioned in the documentation, IsDrill and IsChainsaw automatically reduce useTime and useAnimation to 60% of what is set in SetDefaults and decrease tileBoost by 1, but only for vanilla items.
            // We set it here despite it doing nothing because it is likely to be used by other mods to provide special effects to drill or chainsaw items globally.
            ItemID.Sets.IsDrill[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.MeleeNoSpeed; // ignores melee speed bonuses. There's no need for drill animations to play faster, nor drills to dig faster with melee speed.
            Item.width = 56;
            Item.height = 56;
            // IsDrill/IsChainsaw effects must be applied manually, so 60% or 0.6 times the time of the corresponding pickaxe. In this case, 60% of 7 is 4 and 60% of 25 is 15.
            // If you decide to copy values from vanilla drills or chainsaws, you should multiply each one by 0.6 to get the expected behavior.
            Item.useTime = 2;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0.5f;
            Item.value = Item.sellPrice(gold: 40);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item23;
            Item.shoot = ModContent.ProjectileType<NocturneDrillProjectile>(); // Create the drill projectile
            Item.shootSpeed = 32f; // Adjusts how far away from the player to hold the projectile
            Item.noMelee = true; // Turns off damage from the item itself, as we have a projectile
            Item.noUseGraphic = true; // Stops the item from drawing in your hands, for the aforementioned reason
            Item.channel = true; // Important as the projectile checks if the player channels

            // tileBoost changes the range of tiles that the item can reach.
            Item.tileBoost = 1;

            Item.pick = 205; // How strong the drill is, see https://terraria.wiki.gg/wiki/Pickaxe_power for a list of common values
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<NocturniumBar>(), 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}