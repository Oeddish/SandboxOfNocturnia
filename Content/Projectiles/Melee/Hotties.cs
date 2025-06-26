using Terraria;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Projectiles.Melee
{
    public class Hotties : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 15;
            Projectile.height = 17;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 360; // The live time for the projectile (60 = 1 second, so 3600 is 1 minute)
            Projectile.light = 0.5f; // How much light emit around the projectile
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1; //Can penetrate infinite enemies making it so only blocks destroy the projectile

        }

        public override void AI()
		{
            if (Projectile.ai[0] != 1)
            {
                Projectile.velocity.Y += 0.1f;
                if (Projectile.velocity.Y > 10f)
                {
                    Projectile.velocity.Y = 10f;
                }
            }
		}
    }
}