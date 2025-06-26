using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Consumables
{
    public class Bottled_Shimmer : ModItem
    {
        public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 30;
		}

		public override void SetDefaults()
		{
			Item.width = 10;
			Item.height = 13;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useTurn = true;
			Item.UseSound = SoundID.Item3;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(copper: 10);
			Item.buffType = BuffID.Shimmer; // Specify an existing buff to be applied when used.
			Item.buffTime = 0; // The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
		}

		public override bool? UseItem(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				player.AddBuff(Item.buffType, 30);
			}
			return true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Bottle)
				.AddCondition(Condition.NearShimmer)
				.Register();
        }
    }
}