using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Players
{
    internal class BoomerangPlayer : ModPlayer
    {
        public int ExtraBoomerangs { get; set; }
        public float BoomerangReturnSpeedMult { get; set; }
        public float BoomerangKnockbackMult { get; set; }
        public bool BoomerangGlowAndDust { get; set; }
        public bool BoomerangSpectralGlaives { get; set; }

        public override void ResetEffects()
        {
            ExtraBoomerangs = 0;
            BoomerangReturnSpeedMult = 0f;
            BoomerangKnockbackMult = 1f;
            BoomerangGlowAndDust = false;
            BoomerangSpectralGlaives = false;
        }
    }
}
