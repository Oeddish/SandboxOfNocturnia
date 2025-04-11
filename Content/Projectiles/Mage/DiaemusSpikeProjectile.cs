using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Projectiles.Mage
{
    internal class DiaemusSpikeProjectile : ModProjectile
    {
        private int ttl = 120;
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 26;

            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = false;
            Projectile.light = 0.2f;
            Projectile.timeLeft = 255;
            Projectile.tileCollide = true;
        }
        public override void AI()
        {
            ttl--;

            if (ttl <= 0)
            {
                Projectile.Kill();
            }

            Projectile.velocity.Y += 0.2f;
            Projectile.alpha++;

            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;

            // Cap downward velocity
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
        }
    }
}
