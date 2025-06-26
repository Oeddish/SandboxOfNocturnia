using FragmentsOfNocturnia.Content.Projectiles.Summoner;
using Terraria;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Buffs
{

	public class KikiSummonBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
		}

		public override void Update(Player player, ref int buffIndex) {
			// If the minions exist reset the buff time, otherwise remove the buff from the player
			if (player.ownedProjectileCounts[ModContent.ProjectileType<KikiDroidProj>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<KikiBunProj>()] > 0) {
				player.buffTime[buffIndex] = 18000;
			}
			else {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}