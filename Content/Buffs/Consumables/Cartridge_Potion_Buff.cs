using Terraria;
using Terraria.ModLoader;
using FragmentsOfNocturnia.Content.Players;

namespace FragmentsOfNocturnia.Content.Buffs.Consumables
{
	public class Cartridge_Potion_Buff : ModBuff
	{
		public override void Update(Player player, ref int buffIndex)
		{
			if (player.HasBuff<Cartridge_Potion_Buff>())
			{
				player.GetModPlayer<KokoModPlayer>().Cartridge_Buff = true;
			}
		}
	}
}