using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using FragmentsOfNocturnia.Content.Items.Items;
using FragmentsOfNocturnia.Content.Items.Weapons.Melee;
using System.Runtime.CompilerServices;

namespace FragmentsOfNocturnia.Content.Items.Tools
{
    internal class NightmareSteelPickaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Melee;
            Item.width = 44;
            Item.height = 44;
            Item.useTime = 10;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(gold: 4);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.pick = 190; // https://terraria.wiki.gg/wiki/Pickaxe_power
            Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
            Item.tileBoost = 2;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            /*if (Main.rand.NextBool(10))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<ExampleCustomDrawDust>());
            }*/
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 4);
            recipe.AddIngredient(ModContent.ItemType<NightmareSteelBar>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}