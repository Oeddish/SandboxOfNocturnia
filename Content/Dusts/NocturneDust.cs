using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Dusts
{
    internal class NocturneDust : ModDust
    {
        public override void SetStaticDefaults()
        {
            UpdateType = 110;
        }
    }
}