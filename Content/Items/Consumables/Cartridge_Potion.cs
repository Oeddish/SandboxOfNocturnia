using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FragmentsOfNocturnia.Content.Buffs.Consumables;

namespace FragmentsOfNocturnia.Content.Items.Consumables
{
	public class Cartridge_Potion : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 20;

			// Dust that will appear in these colors when the item with ItemUseStyleID.DrinkLiquid is used
			ItemID.Sets.DrinkParticleColors[Type] = [
				new Color(240, 240, 240),
				new Color(200, 200, 200),
				new Color(140, 140, 140)
			];
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
			Item.buffType = ModContent.BuffType<Cartridge_Potion_Buff>(); // Specify an existing buff to be applied when used.
			Item.buffTime = 28800; // The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
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
				.AddIngredient(ItemID.AmmoReservationPotion)
				.AddIngredient(ItemID.MusketBall, 20)
				.AddIngredient<Bottled_Shimmer>(1)
				.AddTile(TileID.Bottles)
				.AddTile(TileID.AlchemyTable)
				.Register();
        }
	}
}