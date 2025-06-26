using FragmentsOfNocturnia.Content.Players;
using FragmentsOfNocturnia.Content.Projectiles.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Buffs.Pets
{
    public class KokoSlimeBuff : ModBuff
	{
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            bool unused = false;
            player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<KokoSlimeProj>());
            if(player.HasBuff<KokoSlimeBuff>()){
                player.GetModPlayer<KokoModPlayer>().slimePet = true;
            }
        }
    }
}

