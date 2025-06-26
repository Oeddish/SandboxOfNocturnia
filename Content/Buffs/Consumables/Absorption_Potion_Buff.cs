using FragmentsOfNocturnia.Content.Players;
using Terraria;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Buffs.Consumables
{
	public class Absorption_Potion_Buff : ModBuff
	{
		public override void Update(Player player, ref int buffIndex)
		{
			if (player.HasBuff<Absorption_Potion_Buff>())
			{
				player.GetModPlayer<KokoModPlayer>().Absorption_Buff = true;
			}
		}
	}
}