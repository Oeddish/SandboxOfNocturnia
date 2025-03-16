using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Players;
using Terraria;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Buffs
{
    internal class CrescentCharging : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.statMana = 0;
            player.manaRegenDelay = 5;
            var modPlayer = player.GetModPlayer<NocturnePlayer>();
            modPlayer.noManaPotions = true;

            //Main.NewText("Mana potions disabled: " + modPlayer.noManaPotions, Microsoft.Xna.Framework.Color.Red); // Debugging
        }
    }
}
