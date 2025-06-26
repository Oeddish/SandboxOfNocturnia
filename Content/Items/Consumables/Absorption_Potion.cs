using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FragmentsOfNocturnia.Content.Buffs.Consumables;

namespace FragmentsOfNocturnia.Content.Items.Consumables
{
	public class Absorption_Potion : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 20;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 26;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useTurn = true;
			Item.UseSound = SoundID.Item3;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(silver: 5);
			Item.buffType = ModContent.BuffType<Absorption_Potion_Buff>(); // Specify an existing buff to be applied when used.
			Item.buffTime = 12*60*60; // 12min * 60s/min * 60 frames/s.
		}

		public override bool? UseItem(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				player.AddBuff(Item.buffType, Item.buffTime);
			}
			return true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HeartreachPotion)
				.AddIngredient(ItemID.FallenStar, 5)
				.AddIngredient<Bottled_Shimmer>(1)
				.AddTile(TileID.Bottles)
				.AddTile(TileID.AlchemyTable)
				.Register();
        }
	}
}