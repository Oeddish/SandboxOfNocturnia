using FragmentsOfNocturnia.Content.Items.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Thrown
{
    public class NightmareBomb : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.ScarabBomb;
        private static readonly int COUNT_RECIPE_RESULTS = 1;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Bomb);
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.NightmareBombProjectile>();
            Item.value = Item.buyPrice(silver: 50);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(COUNT_RECIPE_RESULTS);
            recipe.AddIngredient(ItemID.Bomb, 4); // Base bomb
            recipe.AddIngredient(ModContent.ItemType<BatEssence>(), 1);
            recipe.AddTile(TileID.Anvils); // Crafting station
            recipe.Register();
        }
    }
}
