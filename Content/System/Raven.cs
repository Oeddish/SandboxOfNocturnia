using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Players;
using Terraria;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.System
{
    internal class Raven : ModProjectile
    {
        public int state { get; set; } = 0;
        // 0 = inactive
        // 1 = fading in
        // 2 = flying
        // 3 = sitting
        // 4 = homing
        // 5 = attacking
        // 6 = fading out
        public int ravenCooldown { get; set; } = 120;
    }
}
