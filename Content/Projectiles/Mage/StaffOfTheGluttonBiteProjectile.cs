using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Projectiles.Mage
{
    internal class StaffOfTheGluttonBiteProjectile : ModProjectile
    {
        private int ttl = 25;
        private bool canHit = true;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.width = 50; // The width of projectile hitbox
            Projectile.height = 50; // The height of projectile hitbox

            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.DamageType = DamageClass.Magic; // Is the projectile shoot by a ranged weapon?
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
            Projectile.penetrate = -1; // Look at comments ExamplePiercingProjectile

            Projectile.friendly = false;

            Projectile.alpha = 0; // How transparent to draw this projectile. 0 to 255. 255 is completely transparent.
        }

        public override void AI()
        {
            base.AI();

            ttl--;
            if (ttl < 0) { Projectile.Kill(); }
            if (ttl < 10 && canHit) { Projectile.friendly = true; }

            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                // Or more compactly Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 4;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            canHit = false;
            target.AddBuff(BuffID.Bleeding, 600);
            Projectile.friendly = false;         
        }
    }
}
