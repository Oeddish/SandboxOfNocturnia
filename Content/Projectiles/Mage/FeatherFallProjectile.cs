using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Projectiles.Mage
{
    internal class FeatherFallProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20; // The width of projectile hitbox
            Projectile.height = 20; // The height of projectile hitbox

            Projectile.DamageType = DamageClass.Magic; // What type of damage does this projectile affect?
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
            Projectile.light = 0.2f; // How much light emit around the projectile
            Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.tileCollide = false;

            Projectile.ai[0] = 0;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
            Projectile.ai[0]++;
            // Creates a flurry of particles
            if (Projectile.ai[0] % 10 == 0)
            {
                float scale = 0.3f + Main.rand.NextFloat(0.6f);
                int idx = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SpookyWood);
                Main.dust[idx].noGravity = false;
                Main.dust[idx].scale = scale;
                Main.dust[idx].velocity *= 1.2f;
                Main.dust[idx].velocity += Projectile.velocity * 0.3f;
            }
            if (Projectile.position.Y >= Main.MouseWorld.Y)
            {
                Projectile.tileCollide = true;
            }
        }
        public override void OnKill(int timeLeft)
        {
            // Creates a flurry of particles
            int numDust = 5;
            for (int i = 0; i < numDust; ++i)
            {
                //int dustType = Main.rand.NextBool(3) ? 246 : 244;
                float scale = 0.3f + Main.rand.NextFloat(0.6f);
                int idx = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Wraith);
                Main.dust[idx].noGravity = false;
                Main.dust[idx].scale = scale;
                Main.dust[idx].velocity *= 1.2f;
                Main.dust[idx].velocity += Projectile.velocity * 0.3f;
            }
            base.OnKill(timeLeft);
        }
    }
}
