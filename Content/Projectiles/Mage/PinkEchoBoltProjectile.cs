using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace FragmentsOfNocturnia.Content.Projectiles.Mage
{
    internal class PinkEchoBoltProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16; // The width of projectile hitbox
            Projectile.height = 16; // The height of projectile hitbox

            Projectile.DamageType = DamageClass.Magic; // What type of damage does this projectile affect?
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.light = 0.2f; // How much light emit around the projectile
            Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.tileCollide = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int num = Main.rand.Next(9);
            switch (num)
            {
                case 0:
                    target.AddBuff(BuffID.Poisoned, 240);
                    break;
                case 1:
                    target.AddBuff(BuffID.CursedInferno, 120);
                    break;
                case 2:
                    target.AddBuff(BuffID.ShadowFlame, 180);
                    break;
                default:
                    break;
            }
        }
    }
}
