using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Items.Items;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Melee
{
    internal class Desmodus : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 4);

            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.damage = 80;
            Item.knockBack = 2f;
            Item.scale = 1.5f;

            Item.noMelee = false;
            Item.autoReuse = true;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(
                    target.position + new Vector2(target.width, target.height) / 2,
                    3, 3,
                    DustID.Blood,
                    Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5),
                    0,
                    Color.Red,
                    1f);
            }
            if (target.type != NPCID.TargetDummy)
            {
                player.Heal(damageDone / 50);
            }
            base.OnHitNPC(player, target, hit, damageDone);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BloodVial>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DragonBone>(), 10);
            recipe.AddIngredient(ModContent.ItemType<ViolentCatalyst>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<BloodVial>(), 5);
            recipe1.AddIngredient(ModContent.ItemType<DragonBone>(), 10);
            recipe1.AddIngredient(ModContent.ItemType<RottingCatalyst>(), 1);
            recipe1.AddTile(TileID.MythrilAnvil);
            recipe1.Register();
        }
    }
}
