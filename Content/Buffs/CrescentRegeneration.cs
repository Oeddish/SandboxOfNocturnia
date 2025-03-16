using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Projectiles.Buffs;
using rail;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Buffs
{
    internal class CrescentRegeneration : ModBuff
    {
        int timeLeft = 120;
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            timeLeft--;
            if (player.statLife + 1 < player.statLifeMax2) { player.statLife += 1; }
            if (player.statMana + 1 < player.statManaMax2) { player.statMana += 1; }
            if (timeLeft <= 1) { player.AddBuff(ModContent.BuffType<CrescentCharging>(), 60 * 30); SoundEngine.PlaySound(SoundID.Item92); }
        }
    }
}
