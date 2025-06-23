using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using FragmentsOfNocturnia.Content.Projectiles.Melee;
using FragmentsOfNocturnia.Content.Projectiles.Buffs;

namespace FragmentsOfNocturnia.Content.System
{
    internal class DebuffGlobalNPC : GlobalNPC
    {
        private int timer = 0;

        public override bool InstancePerEntity => true;

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff(ModContent.BuffType<Content.Buffs.Reverberation>()))
            {
                timer++;

                if (timer >= 60) //  every second
                {
                    npc.lifeRegen = -2400; // Damage appkied over 2 seconds. Meaning the real damage will be 2400 : 120 = 20
                    timer = 0;
                }
            }
        }

        public override void OnKill(NPC npc)
        {
            if (npc.HasBuff(ModContent.BuffType<Content.Buffs.Wraithed>()))
            {
                Projectile wraith = Projectile.NewProjectileDirect(npc.GetSource_FromAI(), npc.Center, new Microsoft.Xna.Framework.Vector2(0, 0), ModContent.ProjectileType<WraithedProjectile>(), npc.damage / 2, 1);
            }
                base.OnKill(npc);
        }
    }
}