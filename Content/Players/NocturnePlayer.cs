using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Players
{
    public class NocturnePlayer : ModPlayer
    {
        public bool hasOrbitingProjectile { get; set; }
        public bool noManaPotions = false;
        public override void ResetEffects()
        {
            // Reset the flag every frame
            hasOrbitingProjectile = false;
            noManaPotions = false;
        }
        public override bool CanUseItem(Item item)
        {
            if (noManaPotions && item.consumable && item.healMana > 0)
            {
                return false;
            }
            return base.CanUseItem(item);
        }
    }
}