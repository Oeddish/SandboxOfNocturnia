using FragmentsOfNocturnia.Content.Buffs.Pets;
using FragmentsOfNocturnia.Content.Items.Items;
using FragmentsOfNocturnia.Content.Projectiles.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Pets
{
    public class KokoSlime : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToVanitypet(ModContent.ProjectileType<KokoSlimeProj>(), ModContent.BuffType<KokoSlimeBuff>());

            Item.shoot = ModContent.ProjectileType<KokoSlimeProj>();
            Item.buffType = ModContent.BuffType<KokoSlimeBuff>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BatEssence>(), 10);
            recipe.AddIngredient(ItemID.Gel, 99);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                player.AddBuff(Item.buffType, 3600);
            }
            return true;
        }
    }
}