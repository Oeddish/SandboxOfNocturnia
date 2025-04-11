using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Projectiles.Ranged
{
    internal class DiphyllaProjectile : ModProjectile
    {
        private int ttl = 600;
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 26;

            Projectile.DamageType = DamageClass.Ranged;
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
            Projectile.alpha++;

            if (ttl <= 0 || Projectile.alpha == 250)
            {
                Projectile.Kill();
            }

            Projectile.velocity.Y += 0.15f;
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;

            // Cap downward velocity
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(
                    target.position + new Vector2(target.width, target.height) / 2,
                    1, 1,
                    DustID.Blood,
                    Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5),
                    0,
                    Color.Red,
                    1f);
            }
            Player player = Main.player[Projectile.owner];
            player.Heal(damageDone / 50);
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}
