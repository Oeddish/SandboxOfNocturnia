using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Buffs;
using Steamworks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Players
{
    public class NocturnePlayer : ModPlayer
    {
        public bool hasOrbitingProjectile { get; set; }
        public bool noManaPotions = false;
        public bool applyReverberation = false;
        public bool applyWraithed = false;
        public override void ResetEffects()
        {
            // Reset the flag every frame
            applyReverberation = false;
            hasOrbitingProjectile = false;
            noManaPotions = false;
            applyWraithed = false;
        }
        public override bool CanUseItem(Item item)
        {
            if (noManaPotions && item.consumable && item.healMana > 0)
            {
                return false;
            }
            return base.CanUseItem(item);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPCWithProj(proj, target, hit, damageDone);
            if (proj.owner == Player.whoAmI && proj.DamageType == DamageClass.Magic && applyReverberation)
            {
                target.AddBuff(ModContent.BuffType<Reverberation>(), 180);
            }
            if (proj.owner == Player.whoAmI && proj.DamageType == DamageClass.Magic && applyWraithed)
            {
                target.AddBuff(ModContent.BuffType<Wraithed>(), 300);
            }
        }
    }
}