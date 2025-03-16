using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Items.Items;
using FragmentsOfNocturnia.Content.Players;
using FragmentsOfNocturnia.Content.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Melee;

public class Bratarang : ModItem
{
    //public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.ModdedBoomerangs;

    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 36;
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(gold: 2);

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.useTime = 30;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item1;
        Item.noMelee = true;
        Item.noUseGraphic = true;

        Item.shoot = ModContent.ProjectileType<BratarangProjectile>();
        Item.shootSpeed = 10f;
        Item.damage = 14;
        Item.knockBack = 4.5f;
        Item.DamageType = DamageClass.MeleeNoSpeed;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.EnchantedBoomerang, 1);
        recipe.AddIngredient(ModContent.ItemType<BatEssence>(), 10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();

    }

    public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] < player.GetModPlayer<BoomerangPlayer>().ExtraBoomerangs + 1;
}