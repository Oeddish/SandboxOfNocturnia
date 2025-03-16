using FragmentsOfNocturnia.Content.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Projectiles.Melee
{



    public class BratarangProjectile : Boomerang
    {
        private bool canShoot = true;
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 36;
            Projectile.aiStyle = -1;

            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;

            Projectile.tileCollide = true;

            ReturnSpeed = 16f;
            HomingOnOwnerStrength = 1.5f;
            TravelOutFrames = 30;
            DoTurn = true;
        }



        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0] = 1f;
            if (canShoot)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 velocity = Projectile.velocity;
                    velocity = velocity.RotatedByRandom(MathHelper.ToRadians(30f));
                    Projectile shard = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, velocity, ModContent.ProjectileType<BratarangHitProjectile>(), Projectile.damage / 2, Projectile.knockBack / 3f, Projectile.owner, target.whoAmI);
                    shard.frame = Main.rand.Next(0, 4);
                }
                canShoot = false;
            }

            //Projectile.Kill();


        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
            Projectile.ai[0] = 1f;
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            // Dust
            int numDust = Main.rand.Next(8, 12);
            for (int i = 0; i < numDust; i++)
            {
                Vector2 velocity = Projectile.velocity;
                velocity *= 0.2f;
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, velocity.X, velocity.Y, 0, default, Main.rand.NextFloat(0.8f, 1.2f));
            }
        }
    }
}